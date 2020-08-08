using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CubePlayer : MonoBehaviour
{
    [SerializeField] Rigidbody rig;
    [SerializeField] float maxSpeed;
    [SerializeField] float speedInc;
    [SerializeField] float speedX;
    [SerializeField] float speedY;
    [SerializeField] GameObject shield;

    bool hasShield;
    bool isGrounded;
    float tempSpeed;
    float combinedSpeed;
    float timer;

    private void Start()
    {
        hasShield = false;
        tempSpeed = 0;
    }


    private void Update()
    {
        if (Mathf.Abs(combinedSpeed) < Mathf.Abs(maxSpeed))
        {
            timer += Time.deltaTime;
            tempSpeed = (timer / speedInc);
        }
     
        combinedSpeed = speedY - tempSpeed;
        if (isGrounded)
        {
            rig.velocity = new Vector3(0, combinedSpeed, 0);
        }
 
        else
            rig.velocity = new Vector3(speedX, combinedSpeed, 0);
 

        if (Input.GetMouseButtonDown(0))
        {
            speedX *= 1;
            Vector3 temp = GetComponent<BoxCollider>().center;
            temp.x *= -1;
            GetComponent<BoxCollider>().center = temp;
            isGrounded = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
        //else if (collision.gameObject.tag == "Enemy")
        //{

        //    // SceneManager.LoadScene(0);
        //}
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Debug.Log(collision.gameObject.name);
            isGrounded = false;
           
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Collectibles")
        {
            other.gameObject.SetActive(false);
            GameManager.Instance.InGameScore();
        }

        else if (other.tag == "PowerUP")
        {
            combinedSpeed *= 0.7f;
            timer *= 0.7f;
            other.gameObject.SetActive(false);
        }
        else if (other.tag == "Enemy")
        {
            if (hasShield)
            {
                shield.SetActive(false);
                hasShield = false;
            }
            else
            {
                GameManager.Instance.Lives();
            }
            other.gameObject.SetActive(false);
        }
        else if (other.tag == "Shield")
        {
            shield.SetActive(true);
            hasShield = true;
            other.gameObject.SetActive(false);
        }
    }
}

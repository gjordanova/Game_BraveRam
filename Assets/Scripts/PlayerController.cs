using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float Speed = 5f;
    [SerializeField] Rigidbody rig;
    [SerializeField] float SpeedX;
    [SerializeField] float SpeedY;
    [SerializeField] float SpeedInc;
    [SerializeField] GameObject shield;
    [SerializeField] float MaxSpeed;
    [SerializeField] PlayerPresets playerPreset;

    float tempSpeed;
    [SerializeField] float combinedSpeed;
    float timer;
    bool hasShield;

    bool isGrounded;

    private void Start()
    {
        playerPreset = GameManager.Instance.activePreset;
        if(playerPreset != null)
        {
            SpeedX = playerPreset.speedX;
            SpeedY = playerPreset.speedY;
            SpeedInc = playerPreset.speedInc;
            MaxSpeed = playerPreset.maxSpeed;
        }
        hasShield = false;
    }

    private void Update()
    {
        if (Mathf.Abs(combinedSpeed) < Mathf.Abs(MaxSpeed))
        {
            timer += Time.deltaTime;
            tempSpeed = (timer / SpeedInc);
        }
        combinedSpeed = SpeedY + tempSpeed;

        if (isGrounded)
            rig.velocity = new Vector3(0, SpeedY, 0);
        else
            rig.velocity = new Vector3(SpeedX, SpeedY, 0);
        if (Input.GetMouseButtonDown(0))
        {
            SpeedX *= -1;
            //Vector3 temp = transform.localScale;
            //temp.y *= 1;
            //transform.localScale = temp;
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
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }

        if (other.gameObject.tag == "Collectibles")
        {
            Debug.Log("Collected a point");
            other.gameObject.SetActive(false);
            GameManager.Instance.InGameScore();
        }

        else if (other.gameObject.tag == "PowerUP")
        {
            combinedSpeed *= 0.7f;
            timer *= 0.7f;
            other.gameObject.SetActive(false);
        }
        else if (other.gameObject.tag == "Enemy")
        {
            if (hasShield)
            {
                shield.SetActive(false);
                hasShield = false;
            }
            else
            {
                GameManager.Instance.Lives();
                GameManager.Instance.SetHighScore();
            }
            other.gameObject.SetActive(false);
        }
        else if (other.gameObject.tag == "Shield")
        {
            shield.SetActive(true);
            //shield.transform.Rotate(0, 0, Speed);
            hasShield = true;
            other.gameObject.SetActive(false);
            
        }
    }
}

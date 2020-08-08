using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{

    [SerializeField] GameObject[] obstaclesLeft;
    [SerializeField] GameObject[] obstaclesRight;
    [SerializeField] GameObject[] CollectiblesLeft;
    [SerializeField] GameObject[] CollectiblesRight;
    [SerializeField] GameObject[] ConstantCollectibles;

    [SerializeField] string[] tags = new string[] { "PowerUP", "Shield", "Collectibles" };
    [SerializeField] Material[] mats;
    [SerializeField] float timer;
    [SerializeField] float timeInc;
    [SerializeField] int maxObstacles;
    int numObstacles;

    Vector3[] startingPositionsObsLeft;
    Vector3[] startingPositionsObsRight;
    Vector3[] startingPositionsColLeft;
    Vector3[] startingPositionsColRight;

    private void Start()
    {
        startingPositionsObsLeft = new Vector3[obstaclesLeft.Length];
        startingPositionsObsRight = new Vector3[obstaclesRight.Length];
        startingPositionsColLeft = new Vector3[CollectiblesLeft.Length];
        startingPositionsColRight = new Vector3[CollectiblesRight.Length];
        int counter = 0;
        foreach (var item in obstaclesLeft)
        {
            startingPositionsObsLeft[counter] = item.transform.localPosition;
            counter++;
        }
        counter = 0;
        foreach (var item in obstaclesRight)
        {
            startingPositionsObsRight[counter] = item.transform.localPosition;
            counter++;
        }
        counter = 0;
        foreach (var item in CollectiblesLeft)
        {
            startingPositionsColLeft[counter] = item.transform.localPosition;
            counter++;
        }
        counter = 0;
        foreach (var item in CollectiblesRight)
        {
            startingPositionsColRight[counter] = item.transform.localPosition;
            counter++;
        }
        numObstacles = 1;
    }


    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeInc && numObstacles < maxObstacles)
        {
            numObstacles++;
            timer = 0;
        }
    }

    public void SetObstacles()
    {
        CleanBlock();
        for (int i = 0; i < numObstacles; i++)
        {
            int side = Random.Range(0, 3);
            if (side == 1)
            {
                int randomIndex = Random.Range(0, obstaclesLeft.Length);
                obstaclesLeft[randomIndex].SetActive(true);
            }
            else
            {
                int randomIndex = Random.Range(0, obstaclesRight.Length);
                obstaclesRight[randomIndex].SetActive(true);

            }
        }
    }

    public void SetCollectibles()
    {
        foreach(var item in ConstantCollectibles)
        {
            item.SetActive(true);
        }
        CleanCollectibles();
        int index = Random.Range(0, 2);
        if (index == 1)
        {
            int randomIndex = Random.Range(0, CollectiblesLeft.Length);
            CollectiblesLeft[randomIndex].SetActive(true);
            int randomTag = Random.Range(0, 10);
            if (randomTag > 2)
                randomTag = 2;
            Debug.Log("RandomTag: " + randomTag);
            //CollectiblesLeft[randomIndex].tag = tags[randomTag];
            //CollectiblesLeft[randomIndex].GetComponent<Renderer>().material = mats[randomTag];
        }
        else
        {
            int randomIndex = Random.Range(0, CollectiblesRight.Length);
            CollectiblesRight[randomIndex].SetActive(true);
            int randomTag = Random.Range(0, 10);
            if (randomTag > 2)
                randomTag = 2;
            Debug.Log("RandomTag: " + randomTag);
            //CollectiblesRight[randomIndex].tag = tags[randomTag];
            //CollectiblesRight[randomIndex].GetComponent<Renderer>().material = mats[randomTag];
        }
    }


    void CleanBlock()
    {
        foreach (GameObject item in obstaclesRight)
        {
            item.SetActive(false);
        }
        foreach (GameObject item in obstaclesLeft)
        {
            item.SetActive(false);
        }
        int counter = 0;
        foreach (var item in obstaclesLeft)
        {
            item.transform.localPosition = startingPositionsObsLeft[counter];
            counter++;
        }
        counter = 0;
        foreach (var item in obstaclesRight)
        {
            item.transform.localPosition = startingPositionsObsRight[counter];
            counter++;
        }
        counter = 0;
        foreach (var item in CollectiblesLeft)
        {
            item.transform.localPosition = startingPositionsColLeft[counter];
            counter++;
        }
        counter = 0;
        foreach (var item in CollectiblesRight)
        {
            item.transform.localPosition = startingPositionsColRight[counter];
            counter++;
        }
    }

    void CleanCollectibles()
    {
        foreach (var item in CollectiblesLeft)
        {
            item.SetActive(false);
        }

        foreach (var item in CollectiblesRight)
        {
            item.SetActive(false);
        }
    }


}

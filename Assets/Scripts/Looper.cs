using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Looper : MonoBehaviour {

    [SerializeField] float yDiff;
    [SerializeField] int numberOfBlocks;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Block")
        {
            Vector3 temp = other.transform.position;
            temp.y -= yDiff * numberOfBlocks;
            other.transform.position = temp;
            other.GetComponent<ObstacleController>().SetObstacles();
            other.GetComponent<ObstacleController>().SetCollectibles();
        }
    }
}

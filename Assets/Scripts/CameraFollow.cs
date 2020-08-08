using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    [SerializeField] GameObject objToFollow;
    [SerializeField] float yDiff;

    private void Update()
    {
        Vector3 temp = transform.position;
        temp.y = objToFollow.transform.position.y + yDiff;
        transform.position = temp;
    }
}

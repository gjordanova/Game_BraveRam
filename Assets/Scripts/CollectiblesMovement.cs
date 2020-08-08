using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblesMovement : MonoBehaviour {

    [SerializeField] float speed;

    private void Update()
    {
        Vector3 temp = transform.position;
        temp.y += speed;
        transform.position = temp;
    }

}

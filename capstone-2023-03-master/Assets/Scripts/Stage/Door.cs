using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Vector3 Destination { get; set; }

    //문과 접촉 시 목적지로 이동
    private void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Player")
        {
            collider.transform.position = Destination;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Vector3 Destination { get; set; }

    //���� ���� �� �������� �̵�
    private void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Player")
        {
            collider.transform.position = Destination;
        }
    }
}

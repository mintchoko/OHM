using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private Vector3 armLength = new Vector3(3, 5, -2);
    private Quaternion camRotation = Quaternion.Euler(45, -45, 0);
    private GameObject player;

    private void Awake()
    {
        gameObject.transform.rotation = camRotation;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    //플레이어 위치 따라가는 카메라
    private void LateUpdate()
    {
        gameObject.transform.position = player.transform.position + armLength;
    }
}

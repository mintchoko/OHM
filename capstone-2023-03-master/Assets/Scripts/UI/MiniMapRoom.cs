using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMapRoom : MonoBehaviour
{
/*    //Ŭ������ �̵��� ��
    public Room connectedRoom;

    //���콺 �ø��� ���� �ٲ� �׵θ� �̹���
    [SerializeField]
    public Image roomOutline;*/

    //�� Ÿ�Կ� ���� ���� �ٲ� ��ü �̹���
    [SerializeField]
    public Image roomBody;

    //���� ���⿡ ���� ǥ��/��ǥ�õ� ��
    [SerializeField]
    public GameObject[] Doors = new GameObject[4];

    //ĳ���� ������
    [SerializeField]
    public GameObject player;

}

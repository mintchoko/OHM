using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //BackGroundUI ���� �� Canvas�� Order in Layer�� 0���� ����
        Canvas canvas = GetComponent<Canvas>();
        canvas.sortingOrder = 0;

        //Canvas�� ī�޶� BattleCamera�� ����, �׷� ī�޶� ���ٸ� ���� ī�޶�� ����
        GameObject battleCameraParent = GameObject.Find("BattleCameraParent");
        Camera mainCamera = Camera.main;
        if (battleCameraParent != null)
        {
            canvas.worldCamera = battleCameraParent.transform.GetChild(0).GetComponent<Camera>();
            mainCamera.gameObject.SetActive(false);
            battleCameraParent.transform.GetChild(0).GetComponent<Camera>().gameObject.SetActive(true);
        }
        else
        {
            canvas.worldCamera = mainCamera;
        }
    }

    // Update is called once per frame
}

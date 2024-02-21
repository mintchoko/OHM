using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //BackGroundUI 실행 시 Canvas의 Order in Layer를 0으로 설정
        Canvas canvas = GetComponent<Canvas>();
        canvas.sortingOrder = 0;

        //Canvas의 카메라를 BattleCamera로 설정, 그런 카메라가 없다면 메인 카메라로 설정
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

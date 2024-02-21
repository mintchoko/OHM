using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class terrainCollision : MonoBehaviour
{
    private Animator zombieAnimator; // 좀비의 Animator 컴포넌트
    private bool hasCollided = false; // 한 번의 충돌만 처리하기 위한 플래그

    private Transform cameraTransform; // 움직일 때  카메라가 함께 움직이기 위한 변수
    // Start is called before the first frame update

    
    private StatusText statusText;

    void Start()
    {
        zombieAnimator = GetComponent<Animator>();
        statusText = GameManager.StatusText.GetComponent<StatusText>();
    }

    void CameraChange()
    {


        if(GameManager.Turn){
            if(GameManager.ZombieCamera1 != null && GameManager.ZombieCamera2 != null )
            {
                GameManager.ZombieCamera2.SetActive(false);
                GameManager.ZombieCamera1.SetActive(true);
                // 큐브 위치 돌려놓기
                GameObject box2 = GameObject.FindGameObjectWithTag("box2");
                box2.transform.position = GameManager.Cube2Position;

                box2.GetComponent<Rigidbody>().isKinematic = true;
                statusText.PrintTurn("Player 1");
            }
        }
        else{
            if(GameManager.ZombieCamera1 != null && GameManager.ZombieCamera2 != null )
            {
                GameManager.ZombieCamera1.SetActive(false);
                GameManager.ZombieCamera2.SetActive(true);
                // 큐브 위치 돌려놓기
                GameObject box1 = GameObject.FindGameObjectWithTag("box1");
                box1.transform.position = GameManager.Cube1Position;
                box1.GetComponent<Rigidbody>().isKinematic = true;
                statusText.PrintTurn("Player 2");
            }
        }
        GameManager.IsCollision = false;
        GameManager.ChangeTurn();
        Debug.Log("camera Change (terrain Collision)");
    }

    IEnumerator DelayedFunctionCoroutine() {
        yield return new WaitForSeconds(5f);
        CameraChange();
        GameManager.IsFunctionRunning = false;
    }

    
    void OnCollisionEnter(Collision collision)
    {
        if(GameManager.IsCollision) return;
        GameManager.IsCollision = true;
        // player1의 박스에 충돌했을 경우
        if(collision.gameObject.CompareTag("box2"))
        {
            Debug.Log("충돌1");
            StartCoroutine(DelayedFunctionCoroutine());
        }
        else if(collision.gameObject.CompareTag("box1"))
        {
            Debug.Log("충돌2");
             // 카메라 이동을 3초 후에 실행
            StartCoroutine(DelayedFunctionCoroutine());
        }      
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}

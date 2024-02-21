using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    private Animator zombieAnimator; // 좀비의 Animator 컴포넌트
    private bool hasCollided = false; // 한 번의 충돌만 처리하기 위한 플래그

    private Transform cameraTransform; // 움직일 때  카메라가 함께 움직이기 위한 변수
    // Start is called before the first frame update

    
    private HealthSystemForDummies healthSystem;
    private StatusText statusText;

    public GameObject explosionFactory; // 충돌 했을때 effect 사용

    public AudioSource audioPlayer; // 맞았을때 오디오 생성
    public AudioSource hitaudioPlayer;

    void Start()
    {
        healthSystem = GetComponentInChildren<HealthSystemForDummies>();
        statusText = GameManager.StatusText.GetComponent<StatusText>();
        zombieAnimator = GetComponent<Animator>();
        zombieAnimator.SetBool("_back",true);
    }


    void CameraChange()
    {
        if(GameManager.Turn){
            if(GameManager.ZombieCamera1 != null && GameManager.ZombieCamera2 != null )
            {
                GameManager.ZombieCamera2.SetActive(false);
                GameManager.ZombieCamera1.SetActive(true);
                zombieAnimator.SetBool("_back",true);
                // 큐브 위치 돌려놓기
                GameObject box2 = GameObject.FindGameObjectWithTag("box2");
                box2.transform.position = GameManager.Cube2Position;
                box2.GetComponent<Rigidbody>().isKinematic = true; // 큐브 고정
                box2.GetComponent<ThrowBox2>().isFlying = false;
                box2.GetComponent<ThrowBox2>().isMoving = false;
                statusText.PrintTurn("Player 1");
            }
        }
        else{
            if(GameManager.ZombieCamera1 != null && GameManager.ZombieCamera2 != null )
            {
                GameManager.ZombieCamera1.SetActive(false);
                GameManager.ZombieCamera2.SetActive(true);
                zombieAnimator.SetBool("_back",true);
                // 큐브 위치 돌려놓기
                GameObject box1 = GameObject.FindGameObjectWithTag("box1");
                box1.transform.position = GameManager.Cube1Position;
                box1.GetComponent<Rigidbody>().isKinematic = true; // 큐브 고정
                box1.GetComponent<ThrowBox1>().isFlying = false;
                box1.GetComponent<ThrowBox1>().isMoving = false;
                statusText.PrintTurn("Player 2");
            }
        }
        GameManager.IsCollision = false;
        GameManager.IsFunctionRunning = false;
        GameManager.ChangeTurn();
        Debug.Log("camera Change (Object Collision)");
    }

    IEnumerator DelayedFunctionCoroutine() {
        yield return new WaitForSeconds(5f);
        CameraChange();
    }

    IEnumerator DelayedGameOverCoroutine() {
        statusText.PrintGameResult(GameManager.Turn);
        yield return new WaitForSeconds(5f);
        SceneLoader.Instance.LoadScene("TitleScene");
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if(GameManager.IsCollision) return;
        GameManager.IsCollision = true;

        healthSystem.AddToCurrentHealth(GameManager.power);
        if (!healthSystem.IsAlive) {
            StartCoroutine(DelayedGameOverCoroutine());
        }

        // player1의 박스에 충돌했을 경우
        if(collision.gameObject.CompareTag("box2"))
        {
            audioPlayer.Play();
            hitaudioPlayer.Play();
            GameObject explosion = Instantiate(explosionFactory);
            explosion.transform.position = transform.position;
            Debug.Log("충돌1");
            zombieAnimator.SetBool("_back",false);
            StartCoroutine(DelayedFunctionCoroutine());
        }
        else if(collision.gameObject.CompareTag("box1"))
        {
            audioPlayer.Play();
            hitaudioPlayer.Play();
            GameObject explosion = Instantiate(explosionFactory);
            explosion.transform.position = transform.position;
            Debug.Log("충돌2");
            zombieAnimator.SetBool("_back",false);
             // 카메라 이동을 3초 후에 실행
            StartCoroutine(DelayedFunctionCoroutine());
        }      
        
    }

    // Update is called once per frame
    void Update()
    { 
    }
}
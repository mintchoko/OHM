using UnityEngine;

public class ThrowBox1 : MonoBehaviour
{
    public Transform cameraTransform;
    public Vector3 weaponPositionOffset;
    public Vector3 weaponRotationOffset;
    public float throwForceMultiplier = 100f;
    public float maxThrowForce = 2900f;
    private Rigidbody rb;
    private float throwStartTime;
    private bool isHolding = false;
    public float speed = 5.0f;
    public float sensitivity = 1.0f;
    public bool isMoving = false;
    private bool saveTurn;
    public bool isFlying = false;
    public float threshold = 0.01f; // 임계값 설정
    private HealthSystemForDummies hs;

    void Start()
    {
        
        GameObject box1 = GameObject.FindGameObjectWithTag("box1");
        rb = box1.GetComponent<Rigidbody>();
        rb.isKinematic = true; // 큐브 고정
    }

    void Update()
    {   
        if (!isFlying && !isMoving) {
            transform.position = cameraTransform.position + cameraTransform.TransformDirection(weaponPositionOffset);
            transform.rotation = cameraTransform.rotation * Quaternion.Euler(weaponRotationOffset);
        }
        // 턴 이동 시작이면 입출력 막기 
        if(GameManager.Turn) return;
        if(GameManager.IsFunctionRunning) return;
        if(GameManager.IsCollision) return;
        
        // player 1 차례
        if(!GameManager.Turn){
            
            if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼을 누르기 시작했을 때
            {
                isHolding = true;
                throwStartTime = Time.time; // 현재 시간을 저장
            }

            if (Input.GetMouseButtonUp(0) && isHolding) // 마우스 왼쪽 버튼을 땠을 때
            {

                isHolding = false;
                Throw();
                isMoving = true;
                isFlying = true;
                saveTurn = GameManager.Turn;
            }
        }
        // 던지는 중이면 계속 업데이트
        if(isHolding){
            if((Time.time - throwStartTime) * throwForceMultiplier <= maxThrowForce) {
                updatePowerBar(((Time.time - throwStartTime) * throwForceMultiplier) / 10);
            }
        }
    }

    void updatePowerBar(float powers)
    {
        GameObject[] power = GameObject.FindGameObjectsWithTag("power");
        foreach (GameObject p in power){
            RectTransform rc = p.GetComponent<RectTransform>();
            if(rc != null) {
                rc.sizeDelta = new Vector2(rc.sizeDelta.x, powers);
            }
        }
    }
    void Throw()
    {
        rb.isKinematic = false; // 큐브 고정 풀기
        float holdTime = Time.time - throwStartTime; // 버튼을 누른 시간 계산
        float throwForce = Mathf.Clamp(holdTime * throwForceMultiplier, 0, maxThrowForce); // 힘의 크기 계산 및 제한
        
        // 좀비 카메라 시점
        GameObject zc = GameManager.ZombieCamera1;
        Vector3 throwDirection = zc.transform.forward;
        throwDirection.Normalize();
        
        rb.AddForce(throwDirection * throwForce); // 물체에 힘 적용
        if(GameManager.SelectedItem != 0){ // 아이템 사용 여부
            if(GameManager.SelectedItem == 1){// 데미지 3배
                GameManager.power *= 3;
            }
            else if(GameManager.SelectedItem == 2){ // 크기 증가
                transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            }
            else if(GameManager.SelectedItem == 3){// 체력 회복
                hs = GameManager.Health1.GetComponent<HealthSystemForDummies>();
                hs.AddToCurrentHealth(200.0f);
            }
            GameManager.Using(GameManager.SelectedItem);
        }
        Debug.Log(GameManager.power);
    }
}
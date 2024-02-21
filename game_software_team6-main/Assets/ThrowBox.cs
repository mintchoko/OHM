using UnityEngine;

public class ThrowObject : MonoBehaviour
{
    public float throwForceMultiplier = 300f;
    public float maxThrowForce = 2000f;
    private Rigidbody rb;
    private float throwStartTime;
    private bool isHolding = false;
    public float speed = 5.0f;
    public float sensitivity = 1.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false; // 시작할 때 중력 비활성화
    }

    void Update()
    {
        
        

        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼을 누르기 시작했을 때
        {
            isHolding = true;
            throwStartTime = Time.time; // 현재 시간을 저장
        }

        if (Input.GetMouseButtonUp(0) && isHolding) // 마우스 왼쪽 버튼을 땠을 때
        {
            isHolding = false;
            Throw();
        }
    }

    void Throw()
    {
        rb.useGravity = true; // 던질 때 중력 활성화
        
        float holdTime = Time.time - throwStartTime; // 버튼을 누른 시간 계산
        float throwForce = Mathf.Clamp(holdTime * throwForceMultiplier, 0, maxThrowForce); // 힘의 크기 계산 및 제한
        
        // 좀비 카메라 시점
        GameObject zc = GameObject.FindGameObjectWithTag("zombieCamera1");
        Vector3 throwDirection = zc.transform.forward;
        throwDirection.Normalize();
        
        rb.AddForce(throwDirection * throwForce); // 물체에 힘 적용
    }
}
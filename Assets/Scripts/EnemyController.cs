
using UnityEngine;


public class EnemyController : MonoBehaviour
{
    public float gravity = -20f;


    CharacterController controller;
    float yVelocity;


    void Start()
    {
        controller = GetComponent<CharacterController>();


        // ép rớt ngay khi bắt đầu
        yVelocity = -1f;
    }


    void Update()
    {
        // nếu đang đứng đất thì giữ nhẹ
        if (controller.isGrounded)
        {
            if (yVelocity < 0)
                yVelocity = -2f;
        }
        else
        {
            // chỉ rơi khi không chạm đất
            yVelocity += gravity * Time.deltaTime;
        }


        // CHỈ MOVE THEO TRỤC Y
        controller.Move(Vector3.up * yVelocity * Time.deltaTime);
    }
}
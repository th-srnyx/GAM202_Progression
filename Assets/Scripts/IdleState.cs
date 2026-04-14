
using UnityEngine;


public class IdeState : StateMachineBehaviour
{
    Transform player; // Biến lưu vị trí (Transform) của Player


    public float chaseRange = 10f; // Khoảng cách để enemy bắt đầu chuyển sang trạng thái đuổi (chase)


    // Hàm này được gọi 1 lần khi enemy VỪA chuyển vào state Idle
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Tìm GameObject có tag "Player"
        // Lấy Transform của Player để tính khoảng cách sau này
        player = GameObject.FindGameObjectWithTag("Player").transform;


    }


    // Hàm này được gọi LIÊN TỤC mỗi frame khi enemy đang ở state Idle
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Tính khoảng cách giữa Player và Enemy
        // player.position  → vị trí người chơi
        // animator.transform.position → vị trí enemy
        float distance = Vector3.Distance(player.position, animator.transform.position);


        // Nếu Player đứng trong phạm vi chaseRange
        if (distance < chaseRange)
        {
            // Bật biến isChasing trong Animator
            // → Animator sẽ chuyển từ Idle sang Chase
            animator.SetBool("isChasing", true);


        }
    }
}


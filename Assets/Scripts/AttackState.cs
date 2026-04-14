using UnityEngine;

public class AttackState : StateMachineBehaviour
{
    // Lưu Transform của Player để kiểm tra khoảng cách tấn công
    Transform player;
    // Khoảng cách tối đa để enemy có thể tấn công
    public float attackDistance = 2.5f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Tìm Player theo tag và lấy Transform
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Tính khoảng cách giữa Player và Enemy
        float distance = Vector3.Distance(player.position, animator.transform.position);

        // Nếu Player ra khỏi tầm đánh
        if (distance > attackDistance)
        {
            // Tắt biến isAttacking để rời trạng thái Attack
            animator.SetBool("isAttacking", false);
        }
    }
}

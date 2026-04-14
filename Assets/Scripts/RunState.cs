using UnityEngine;
using UnityEngine.AI;


public class RunState : StateMachineBehaviour
{
    // NavMeshAgent dùng để điều khiển enemy đuổi theo Player
    NavMeshAgent agent;


    // Lưu Transform của Player để xác định vị trí và khoảng cách
    Transform player;


    // Khoảng cách để enemy ngừng đuổi và quay về trạng thái khác
    public float stopChaseDistance = 15f;


    // Khoảng cách đủ gần để enemy chuyển sang trạng thái tấn công
    public float attackDistance = 2.5f;


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Lấy NavMeshAgent từ GameObject cha của Animator
        agent = animator.transform.parent.GetComponent<NavMeshAgent>();


        // Tìm Player theo tag và lấy Transform
        player = GameObject.FindGameObjectWithTag("Player").transform;


        // Thiết lập tốc độ chạy cho enemy khi vào trạng thái Run
        agent.speed = 5f;
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Tính khoảng cách giữa Player và Enemy
        float distance = Vector3.Distance(player.position, animator.transform.position);


        // Cập nhật điểm đến để enemy luôn chạy theo Player
        agent.SetDestination(player.position);


        // Nếu Player ra quá xa, enemy ngừng đuổi
        if (distance > stopChaseDistance)
        {
            // Tắt biến isChasing để rời trạng thái Run
            animator.SetBool("isChasing", false);
        }


        // Nếu Player đủ gần để tấn công
        if (distance < attackDistance)
        {
            // Bật biến isAttacking để chuyển sang state Attack
            animator.SetBool("isAttacking", true);
        }
    }
}

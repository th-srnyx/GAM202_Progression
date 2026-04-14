using UnityEngine;
using UnityEngine.AI;


public class WalkState : StateMachineBehaviour
{
    NavMeshAgent agent; // NavMeshAgent dùng để di chuyển enemy


    Transform[] waypoints; // Mảng lưu các waypoint để enemy đi tuần


    int currentIndex; // Chỉ số waypoint hiện tại
    Transform player; // Lưu Transform của Player để kiểm tra khoảng cách
    public float chaseRange = 10f; // Khoảng cách phát hiện Player để chuyển sang Chase


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Lấy NavMeshAgent từ GameObject cha của Animator
        agent = animator.transform.parent.GetComponent<NavMeshAgent>();


        // Tìm Player theo tag và lấy Transform
        player = GameObject.FindGameObjectWithTag("Player").transform;


        // Tìm GameObject cha chứa các waypoint
        GameObject wpParent = GameObject.FindGameObjectWithTag("WayPoints");


        // Khởi tạo mảng waypoint dựa trên số lượng con của WayPoints
        waypoints = new Transform[wpParent.transform.childCount];


        // Duyệt qua từng waypoint con
        for (int i = 0; i < waypoints.Length; i++)
        {
            // Lấy Transform của từng waypoint và lưu vào mảng
            waypoints[i] = wpParent.transform.GetChild(i);
        }


        // Chọn ngẫu nhiên một waypoint để bắt đầu di chuyển
        currentIndex = Random.Range(0, waypoints.Length);


        // Ra lệnh cho enemy di chuyển tới waypoint đã chọn
        agent.SetDestination(waypoints[currentIndex].position);
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Tính khoảng cách giữa Player và Enemy
        float distance = Vector3.Distance(player.position, animator.transform.position);


        // Nếu Player đi vào phạm vi phát hiện
        if (distance < chaseRange)
        {
            // Bật biến isChasing để chuyển sang state Chase
            animator.SetBool("isChasing", true);


            // Thoát hàm để không tiếp tục logic đi tuần
            return;
        }


        // Nếu enemy đã gần tới waypoint hiện tại
        if (agent.remainingDistance < 0.5f)
        {
            // Chọn ngẫu nhiên waypoint mới
            currentIndex = Random.Range(0, waypoints.Length);


            // Tiếp tục di chuyển tới waypoint mới
            agent.SetDestination(waypoints[currentIndex].position);
        }
    }
}
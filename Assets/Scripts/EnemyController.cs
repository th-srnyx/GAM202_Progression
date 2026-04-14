using UnityEngine;
using UnityEngine.AI;


public class EnemyController : MonoBehaviour
{
    NavMeshAgent agent;        // Điều khiển di chuyển enemy trên NavMesh
    Animator animator;         // Điều khiển animation (Idle / Walk)


    Transform[] waypoints;     // Danh sách các waypoint để tuần tra
    int currentIndex;          // Chỉ số waypoint hiện tại


    public float walkSpeed = 2f;   // Tốc độ đi bộ khi tuần tra


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        // Lấy NavMeshAgent gắn trên Enemy (object cha)


        animator = GetComponentInChildren<Animator>();
        // Lấy Animator nằm trong EnemyTPose (object con)


        GameObject wpParent = GameObject.FindGameObjectWithTag("WayPoints");
        // Tìm object WayPoints bằng Tag


        waypoints = new Transform[wpParent.transform.childCount];
        // Tạo mảng waypoint với số lượng = số object con trong WayPoints


        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = wpParent.transform.GetChild(i);
            // Lưu từng waypoint con vào mảng
        }


        currentIndex = Random.Range(0, waypoints.Length);
        // Chọn ngẫu nhiên 1 waypoint ban đầu


        agent.speed = walkSpeed;
        // Đặt tốc độ đi bộ cho enemy


        agent.SetDestination(waypoints[currentIndex].position);
        // Ra lệnh cho enemy bắt đầu đi tới waypoint
    }


    void Update()
    {
        // ===== ĐIỀU KHIỂN ANIMATION =====
        float speed = agent.velocity.magnitude;
        // Lấy vận tốc hiện tại của NavMeshAgent


        animator.SetBool("isPatrolling", speed > 0.1f);
        // Nếu enemy đang di chuyển → Walk
        // Nếu đứng yên → Idle


        // ===== ĐIỀU KHIỂN DI CHUYỂN =====
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            // Khi enemy đã tới gần waypoint


            currentIndex = Random.Range(0, waypoints.Length);
            // Chọn waypoint mới ngẫu nhiên


            agent.SetDestination(waypoints[currentIndex].position);
            // Đi tiếp tới waypoint mới
        }
    }
}

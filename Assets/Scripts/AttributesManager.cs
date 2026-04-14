using UnityEngine;
using UnityEngine.UI;

public class AttributesManager : MonoBehaviour
{
    public int health;// Máu hiện tại của nhân vật
    public int attack; // Sát thương cơ bản
    
    [Header("ENEMY Die")]
    public bool isDead = false;

    // ====== ĐÒN CHÍ MẠNG (CRITICAL HIT) ======
    // Hệ số sát thương khi đánh chí mạng
    //  1.5 = damage tăng 150%
    public float critDamage = 1.5f;

    // Tỉ lệ đánh chí mạng
    // 0.5 = 50% khả năng ra đòn chí mạng
    public float critChance = 0.5f;

    // =======================
    // BIẾN LIÊN QUAN HEALTH BAR
    // =======================

    // Slider đại diện cho thanh máu
    private Slider healthSlider;

    // Canvas chứa thanh máu (World Space)
    private Transform healthCanvas;

    // Camera chính để thanh máu luôn quay về màn hình
    private Camera mainCamera;

    // =======================
    // HÀM START – CHẠY 1 LẦN KHI OBJECT SINH RA
    // =======================
    void Start()
    {
        // Lấy camera chính trong scene
        mainCamera = Camera.main;

        // Nếu object này KHÔNG PHẢI Enemy → bỏ qua
        if (!CompareTag("Enemy")) return;

        // =======================
        // TÌM CANVAS THEO TÊN
        // (KHÔNG DÙNG GetChild(index) để tránh lỗi)
        // =======================
        healthCanvas = transform.Find("Canvas");

        // Nếu không tìm thấy Canvas → báo lỗi
        if (healthCanvas == null)
        {
            Debug.LogError(" Không tìm thấy Canvas trong Enemy: " + gameObject.name);
            return;
        }

        // =======================
        // TÌM HEALTH BAR (SLIDER)
        // =======================
        Transform bar = healthCanvas.Find("HealthBar");

        // Nếu không tìm thấy HealthBar → báo lỗi
        if (bar == null)
        {
            Debug.LogError("Không tìm thấy HealthBar trong Canvas: " + gameObject.name);
            return;
        }

        // Lấy component Slider từ HealthBar
        healthSlider = bar.GetComponent<Slider>();

        // Nếu HealthBar không có Slider → báo lỗi
        if (healthSlider == null)
        {
            Debug.LogError("HealthBar không có Slider component");
            return;
        }

        // Gán giá trị max cho thanh máu
        healthSlider.maxValue = health;

        // Gán giá trị ban đầu
        healthSlider.value = health;
    }

    // =======================
    // UPDATE – CHẠY MỖI FRAME
    // =======================
    void Update()
    {
        // Nếu có Canvas và Camera
        if (healthCanvas != null && mainCamera != null)
        {
            // Làm cho thanh máu LUÔN QUAY VỀ CAMERA
            // => Enemy xoay hướng nào thì máu vẫn nhìn thẳng
            healthCanvas.LookAt(
                healthCanvas.position + mainCamera.transform.forward
            );
        }
    }

    // =======================
    // HÀM NHẬN SÁT THƯƠNG
    // =======================
    public void TakeDamage(int amount)
    {
        // Trừ máu theo damage nhận vào
        health -= amount;

        // Nếu không phải Enemy thì không xử lý UI
        if (!CompareTag("Enemy")) return;

        // Cập nhật giá trị thanh máu
        if (healthSlider != null)
            healthSlider.value = health;

        // Nếu máu <= 0 → chết
        if (health <= 0)
            EnemyDie();
    }

    // =======================
    // HÀM ENEMY CHẾT
    // =======================

    void EnemyDie()
    {
        // Nếu enemy đã chết rồi thì thoát hàm ngay, tránh chạy lại nhiều lần
        if (isDead) return;

        // Đánh dấu enemy đã chết
        isDead = true;

        // In log ra Console để debug: tên enemy + trạng thái Dead
        Debug.Log(gameObject.name + " Dead");

        // =======================
        // 0. TẮT AI (RẤT QUAN TRỌNG)
        // =======================

        // Lấy component EnemyController (script điều khiển AI)
        EnemyController enemyController = GetComponent<EnemyController>();

        // Nếu enemy có EnemyController thì tắt script này
        // → Ngăn AI tiếp tục di chuyển / tấn công sau khi chết
        if (enemyController != null)
            enemyController.enabled = false;

        // =======================
        // 1. TẮT NAVMESH AGENT
        // =======================

        // Lấy NavMeshAgent để dừng hệ thống pathfinding
        UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        // Kiểm tra enemy có dùng NavMeshAgent không
        if (agent != null)
        {
            // Dừng ngay việc di chuyển
            agent.isStopped = true;
            // Tắt hoàn toàn NavMeshAgent để tránh bug giật / trượt
            agent.enabled = false;
        }

        // =======================
        // 2. TẮT CHARACTER CONTROLLER
        // =======================

        // Lấy CharacterController (dùng cho va chạm và di chuyển)
        CharacterController cc = GetComponent<CharacterController>();

        // Nếu có thì tắt để enemy không còn tương tác vật lý
        if (cc != null)
            cc.enabled = false;

        // =======================
        // 3. TẮT COLLIDER
        // =======================

        // Lấy Collider chính của enemy
        Collider col = GetComponent<Collider>();

        // Tắt collider để:
        // - Không bị đánh thêm
        // - Không cản đường player
        if (col != null)
            col.enabled = false;

        // =======================
        // 4. BẬT ANIMATION CHẾT
        // =======================

        // Lấy Animator trong enemy hoặc trong các object con
        Animator animator = GetComponentInChildren<Animator>();

        // Kiểm tra enemy có Animator không
        if (animator != null)
        {
            // Tắt root motion để animation không kéo nhân vật di chuyển
            animator.applyRootMotion = false;
            // Set biến isDead = true để kích hoạt animation chết
            animator.SetBool("isDead", true);
        }

        // =======================
        // 6. HỦY ENEMY
        // =======================

        // Hủy enemy sau 2 giây
        // → Đủ thời gian cho animation chết chạy xong
        Destroy(gameObject, 2f);
    }
    
     // =======================
    // HÀM GÂY SÁT THƯƠNG
    // =======================
    public void DealDamage(GameObject target)
    {
        // Lấy AttributesManager của đối tượng bị đánh
        AttributesManager atm = target.GetComponent<AttributesManager>();
        
        // Nếu target không có AttributesManager → thoát
        if (atm == null) return;
        
        // Damage ban đầu = attack cơ bản
        float totalDamage = attack;
        
        // Random kiểm tra crit
        if (Random.Range(0f, 1f) < critChance)
        {
            // Nếu crit → nhân damage
            totalDamage *= critDamage;
            Debug.Log("Critical Hit!");
        }

        // Gây damage cho target (ép float → int)
        atm.TakeDamage((int)totalDamage);
    }
}
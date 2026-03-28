using UnityEngine;

public class AttackController : MonoBehaviour
{
    // Biến lưu Animator của nhân vật
    // Animator dùng để điều khiển các animation (Idle, Run, Slash…)
    private Animator anim;

    // Biến đánh dấu trạng thái nhân vật đang chém hay không
    // Dùng để:
    // - Không cho chém liên tục
    // - Không cho nhận input di chuyển khi đang chém
    private bool isAttacking;


    void Start()
    {
        // Lấy component Animator gắn trên cùng GameObject (Player/Ninja)
        // và gán vào biến anim để sử dụng trong script
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Nếu đang chém thì KHÔNG xử lý input mới
        // Tránh trường hợp spam chuột gây lỗi animation
        if (isAttacking) return;

        // Kiểm tra nếu người chơi nhấn chuột trái (Mouse0)
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // Đánh dấu trạng thái đang chém
            isAttacking = true;

            // Gọi Trigger "Slash" trong Animator
            // Animator sẽ chuyển sang animation tấn công (Slash)
            anim.SetTrigger("Slash");
        }
    }

    // Hàm này KHÔNG tự chạy
    // Nó được gọi thông qua Animation Event
    // Gắn Event này ở CUỐI animation Slash
    public void EndSlash()
    {
        // Kết thúc trạng thái chém
        // Cho phép nhận lại input và hành động khác
        isAttacking = false;
    }

    void FixedUpdate()
    {
        // Nếu KHÔNG đang chém thì không cần xử lý gì
        if (!isAttacking) return;

        // Reset toàn bộ input trục di chuyển về 0
        // Mục đích:
        // - Không cho controller nhận input khi đang chém
        // - Tránh hiện tượng nhân vật bị trượt (patin)
        // - KHÔNG cần sửa code controller Invector
        Input.ResetInputAxes();
    }
}

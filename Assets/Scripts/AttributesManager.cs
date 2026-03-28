using UnityEngine; 

public class AttributesManager : MonoBehaviour
{
    public int health; // Máu hiện tại của nhân vật
    public int attack;// Sát thương tấn công cơ bản

    // ====== ĐÒN CHÍ MẠNG (CRITICAL HIT) ======

    // Hệ số sát thương khi đánh chí mạng
    //  1.5 = damage tăng 150%
    public float critDamage = 1.5f;

    // Tỉ lệ đánh chí mạng
    // 0.5 = 50% khả năng ra đòn chí mạng
    public float critChance = 0.5f;

    // ====== NHẬN SÁT THƯƠNG ======

    // Hàm nhận sát thương
    // amount: lượng damage mà nhân vật bị trừ
    public void TakeDamage(int amount)
    {
        // Trừ máu theo lượng sát thương nhận vào
        health -= amount;
        // Kiểm tra nếu máu <= 0 thì nhân vật chết
        if (health <= 0)
        {
            // In ra tên GameObject để biết ai đã chết
            Debug.Log(gameObject.name + " is Dead");
        }
    }

    // ====== GÂY SÁT THƯƠNG ======

    // Hàm gây sát thương cho đối tượng khác
    // target: GameObject bị tấn công
    public void DealDamage(GameObject target)
    {
        // Lấy component AttributesManager của target
        // Dùng để truy cập máu và các chỉ số của target
        AttributesManager atm = target.GetComponent<AttributesManager>();

        // Nếu target không có AttributesManager
        // thì không thể gây damage → thoát hàm
        if (atm == null) return;

        // Khởi tạo damage ban đầu bằng attack cơ bản
        float totalDamage = attack;

        // Kiểm tra có ra đòn chí mạng hay không
        // Random.Range(0f, 1f) tạo số ngẫu nhiên từ 0 đến 1
        if (Random.Range(0f, 1f) < critChance)
        {
            // Nếu crit thì nhân damage lên theo hệ số crit
            totalDamage *= critDamage;
            // In log để biết đòn đánh là chí mạng
            Debug.Log("Critical Hit!");
        }
        // Truyền damage cuối cùng (đã tính crit nếu có)
        // Ép kiểu float → int trước khi trừ máu
        atm.TakeDamage((int)totalDamage);
    }
}

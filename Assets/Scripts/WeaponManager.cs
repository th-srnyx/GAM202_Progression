using UnityEngine;


public class WeaponManager : MonoBehaviour
{
    // Tham chiếu đến vũ khí của người chơi
    public GameObject weapon;


    // Hàm được gọi từ Animation Event
    // isEnable = 1 → bật collider
    // isEnable = 0 → tắt collider
    public void EnableWeaponCollider(int isEnable)
    {
        // Kiểm tra đã gán vũ khí chưa
        if (weapon != null)
        {
            // Lấy collider của vũ khí
            Collider col = weapon.GetComponent<Collider>();

            // Nếu có collider thì bật / tắt
            if (col != null)
            {
                if (isEnable == 1)
                    col.enabled = true;
                else
                    col.enabled = false;
            }
        }
    }
}

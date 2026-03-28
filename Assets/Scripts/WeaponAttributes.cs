using UnityEngine;

public class WeaponAttributes : MonoBehaviour
{
    // Attributes của người chơi (Player)
    // Kéo Player (Ninja) có AttributesManager vào đây
    public AttributesManager atm;


    private void OnTriggerEnter(Collider other)
    {
        // Kiểm tra va chạm với Enemy
        if (other.CompareTag("Enemy"))
        {
            // GỌI DealDamage để áp dụng:
            // - Damage cơ bản
            // - Critical Hit
            // - Logic chết
            atm.DealDamage(other.gameObject);
        }
    }
}
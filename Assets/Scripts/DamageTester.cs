using UnityEngine;

public class DamageTester : MonoBehaviour
{
    // Tham chiếu tới AttributesManager của Player
    // Dùng để gọi hàm DealDamage của Player
    public AttributesManager player;

    // Tham chiếu tới AttributesManager của Enemy
    // Dùng để gọi hàm DealDamage của Enemy
    public AttributesManager enemy;

    private void Update()
    {
        // Khi nhấn phím O
        // Player sẽ gây sát thương lên Enemy
        if (Input.GetKeyDown(KeyCode.O))
            player.DealDamage(enemy.gameObject);

        // Khi nhấn phím P
        // Enemy sẽ gây sát thương lên Player
        if (Input.GetKeyDown(KeyCode.P))
            enemy.DealDamage(player.gameObject);
    }
}
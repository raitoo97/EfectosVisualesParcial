using UnityEngine;
public class HealthPickUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<Player>();
            if (player != null)
            {
                if (player.GetLife.GetHealth >= player.GetLife.MaxHealth) return;
                player.GetLife.Heal(40);
                Destroy(gameObject);
            }
        }
    }
}

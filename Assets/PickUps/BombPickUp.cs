using UnityEngine;
public class BombPickUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<Player>();
            if (player != null)
            {
                player.GetPlayerRecolectObjects.AddBomb();
                Destroy(gameObject);
            }
        }
    }
}

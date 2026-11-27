using UnityEngine;
public class DeadZoneL0 : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 11)
        {
            other.gameObject.SetActive(false);
            return;
        }
        if (other.gameObject.layer == 6)
            RespawnPlayer();
    }
    private void RespawnPlayer()
    {
        Transform player = GameManager.instance.player.transform;
        Rigidbody rb = player.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.position = respawnPoint.position;
        player.SetPositionAndRotation(respawnPoint.position, respawnPoint.rotation);
    }
}

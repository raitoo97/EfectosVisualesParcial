using UnityEngine;
public class PortalScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            GameManager.OnGameOver?.Invoke();
        }
    }
}

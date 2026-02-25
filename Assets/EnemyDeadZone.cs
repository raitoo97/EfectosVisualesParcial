using UnityEngine;
using UnityEngine.SceneManagement;
public class EnemyDeadZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 11)
        {
            other.gameObject.SetActive(false);
            Debug.Log("Enemy entered the dead zone and has been deactivated.");
        }
        if(other.gameObject.layer == 6)
        {
            Debug.LogError("Player entered the dead zone! This should not happen. Check your level design.");
            SceneManager.LoadScene(0);
        }
    }
}

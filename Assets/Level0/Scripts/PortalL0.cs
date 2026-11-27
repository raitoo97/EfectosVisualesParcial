using UnityEngine;
using UnityEngine.SceneManagement;
public class PortalL0 : MonoBehaviour
{
    [SerializeField] private string nextScene = "Level1";

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
            SceneManager.LoadScene(nextScene);
    }
}

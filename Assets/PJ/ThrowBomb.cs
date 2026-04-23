using UnityEngine;
public class ThrowBomb : MonoBehaviour
{
    public GameObject _bombPrefab;
    public Transform _ThrowPoint;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            var inventory = GameManager.instance.player.GetComponent<Player>().GetPlayerRecolectObjects;
            if (!inventory.HasBombs()) return;
            SoundManager.Instance?.PlayClip(SoundManager.Instance.GetAudioClip("ThrowBomb"), 1f, false);
            GameObject bomb = Instantiate(_bombPrefab, _ThrowPoint.position + transform.forward, Quaternion.identity);
            Rigidbody rb = bomb.GetComponent<Rigidbody>();
            if (rb == null) return;
            rb.AddForce(transform.forward * 500f);
            inventory.UseBomb();
        }
    }
}

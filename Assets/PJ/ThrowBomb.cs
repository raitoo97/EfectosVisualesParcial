using UnityEngine;
public class ThrowBomb : MonoBehaviour
{
    public GameObject _bombPrefab;
    public Transform _ThrowPoint;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SoundManager.Instance?.PlayClip(SoundManager.Instance.GetAudioClip("ThrowBomb"), 1f, false);
            GameObject bomb = Instantiate(_bombPrefab, _ThrowPoint.position + transform.forward, Quaternion.identity);
            Rigidbody bombRb = bomb.GetComponent<Rigidbody>();
            bombRb.AddForce(transform.forward * 500f);
        }
    }
}
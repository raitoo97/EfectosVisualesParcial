using UnityEngine;
public class Bullet : MonoBehaviour
{
    private TrailRenderer _trail;
    private void Awake()
    {
        _trail = GetComponent<TrailRenderer>();
    }
    private void OnEnable()
    {
        Invoke("DesactivateBullet", 2f);
    }
    void Update()
    {
        this.transform.position += this.transform.forward * Time.deltaTime * 200;
    }
    public void DesactivateBullet()
    {
        this.gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IShield>(out var shield))
        {
            shield.OnImpact();
            DesactivateBullet();
        }
    }
    private void OnDisable()
    {
        _trail.Clear();
        CancelInvoke();
    }
}

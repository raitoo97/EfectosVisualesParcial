using UnityEngine;
public class Bomb : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField]private GameObject _acidPlatform;
    [SerializeField]private float _radius;
    [SerializeField]private LayerMask _groundLayer;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (_rb == null) return;
        GameObject acidPlatform = null;
        if (CheckGround())
        {
            SoundManager.Instance?.PlayClip(SoundManager.Instance.GetAudioClip("ExplosionImpact"), 0.5f, false);
            acidPlatform = Instantiate(_acidPlatform, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    private bool CheckGround()
    {
        return Physics.CheckSphere(this.transform.position, _radius, _groundLayer);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}

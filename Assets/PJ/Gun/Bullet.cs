using UnityEngine;
public enum BulletType
{
    Player,
    Enemy
}
public class Bullet : MonoBehaviour
{
    private TrailRenderer _trail;
    [SerializeField]private BulletType _bulletType;
    [SerializeField]private float _speed = 200f;
    [SerializeField]private float _damage = 10f;
    [SerializeField] private float _maxSpreadAngle = 5f;
    private Vector3 _movementDirection;
    private void Awake()
    {
        _trail = GetComponent<TrailRenderer>();
    }
    private void OnEnable()
    {
        if (_bulletType == BulletType.Enemy)
        {
            SetEnemyMovementDirection();
        }
        Invoke("DesactivateBullet", 2f);
    }
    private void SetEnemyMovementDirection()
    {
        float spreadX = Random.Range(-_maxSpreadAngle, _maxSpreadAngle);
        float spreadY = Random.Range(-_maxSpreadAngle, _maxSpreadAngle);
        Quaternion spreadRotation = Quaternion.Euler(spreadX, spreadY, 0f);
        Quaternion finalRotation = transform.rotation * spreadRotation;
        transform.rotation = finalRotation;
        _movementDirection = transform.forward;
    }
    void Update()
    {
        if(_bulletType == BulletType.Player)
        {
            this.transform.position += this.transform.forward * Time.deltaTime * _speed;
        }
        if(_bulletType == BulletType.Enemy)
        {
            transform.position += _movementDirection * Time.deltaTime * _speed;
        }
    }
    public void DesactivateBullet()
    {
        this.gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<IImpact>(out var impact) && _bulletType == BulletType.Player)
        {
            impact.OnImpact(this.transform.position);
            DesactivateBullet();
        }
        if (other.gameObject.TryGetComponent<Player>(out var player) && _bulletType == BulletType.Enemy)
        {
            Debug.Log(player.name);
            DesactivateBullet();
        }
    }
    private void OnDisable()
    {
        _trail.Clear();
        CancelInvoke();
    }
}

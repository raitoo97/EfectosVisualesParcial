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
    }
    void Update()
    {
        this.transform.position += this.transform.forward * Time.deltaTime * _speed;
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
            print("sii");
            DesactivateBullet();
        }
        if (other.gameObject.TryGetComponent<Player>(out var player) && _bulletType == BulletType.Enemy)
        {
            if (GameManager.instance.player.GetPlayerController._isOnCinematic)
            {
                DamageScreen.instance.HideDamage();
            }
            else
            {
                DamageScreen.instance.ShowDamage();
                DesactivateBullet();
            }
        }
        if (other.gameObject.TryGetComponent<ITakeDamage>(out var takedamageable))
        {
            if (_bulletType == BulletType.Enemy && other.GetComponent<Player>())
            {
                takedamageable.TakeDamage(_damage);
                DesactivateBullet();
            }
            else if (_bulletType == BulletType.Player && other.GetComponent<Enemy>())
            {
                takedamageable.TakeDamage(_damage);
                DesactivateBullet();
            }
            else if (_bulletType == BulletType.Player && other.GetComponent<Shield>())
            {
                takedamageable.TakeDamage(_damage);
                DesactivateBullet();
            }
        }
    }
    private void OnDisable()
    {
        _trail.Clear();
        CancelInvoke();
    }
}

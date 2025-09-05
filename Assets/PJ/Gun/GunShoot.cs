using UnityEngine;
public class GunShoot
{
    private Transform _gunSight;
    private float _timeBetweenShots = 0.1f;
    private float Cooldown;
    public GunShoot(Transform _gunSight)
    {
        this._gunSight = _gunSight;
    }
    public void Shoot()
    {
        if (Cooldown > 0) return;
        Cooldown = _timeBetweenShots;
        var bullet = PoolBullets.instance.GetBullet();
        bullet.transform.position = _gunSight.position;
        bullet.transform.rotation = _gunSight.rotation;
    }
    public void OnUpdate()
    {
        if (Cooldown > 0)
            Cooldown -= Time.deltaTime;
    }
}

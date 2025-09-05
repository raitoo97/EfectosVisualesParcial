using UnityEngine;
public class GunShoot
{
    private Transform _gunSight;
    private Light _light;
    private float _timeBetweenShots = 0.25f;
    private float Cooldown;
    private float _flashDuration = 0.05f;
    private float _flashTimer;
    public GunShoot(Transform _gunSight, Light _light)
    {
        this._gunSight = _gunSight;
        this._light = _light;
        this._light.intensity = 0;
    }
    public void Shoot()
    {
        if (Cooldown > 0) return;
        Cooldown = _timeBetweenShots;
        _flashTimer = _flashDuration;
        var bullet = PoolBullets.instance.GetBullet();
        bullet.transform.position = _gunSight.position;
        bullet.transform.rotation = _gunSight.rotation;
        _light.intensity = 100;
    }
    public void OnUpdate()
    {
        if (Cooldown > 0)
            Cooldown -= Time.deltaTime;
        if (_flashTimer > 0)
        {
            _flashTimer -= Time.deltaTime;
            if (_flashTimer <= 0)
                _light.intensity = 0;
        }
    }
}

using UnityEngine;
public class Gun : MonoBehaviour
{
    private GunShoot _gunShoot;
    public Transform gunSight;
    public Light gunLight;
    private void OnEnable()
    {
        _gunShoot = new GunShoot(gunSight, gunLight);
    }
    private void Update()
    {
        _gunShoot?.OnUpdate();
        if (PlayerInputs.instance.GetInteract())
        {
            _gunShoot?.Shoot();
        }
    }
    private void OnDisable()
    {
        _gunShoot = null;
    }
}

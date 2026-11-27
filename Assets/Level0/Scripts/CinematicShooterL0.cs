using System.Collections;
using UnityEngine;

public class CinematicShooterL0 : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float fireRate = 8f;
    [SerializeField] private float bulletSpeed = 200f;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private GameObject impactPrefab;
    private float nextShotTime;

    private void OnEnable()
    {
        nextShotTime = 0f;
    }

    private void Update()
    {
        if (target == null || Time.time < nextShotTime) return;
        nextShotTime = Time.time + 1f / fireRate;
        Shoot();
    }

    private void Shoot()
    {
        GameObject bullet = PoolBulletEnemy.instance.GetBullet();
        bullet.transform.position = transform.position;
        bullet.transform.rotation = Quaternion.LookRotation(target.position - transform.position);
        bullet.SetActive(true);

        if (muzzleFlash != null) muzzleFlash.Play();
        SoundManager.Instance?.PlayClip(SoundManager.Instance.GetAudioClip("EnemyShoot"), 0.1f, false);

        float travelTime = Vector3.Distance(transform.position, target.position) / bulletSpeed;
        StartCoroutine(SpawnImpact(travelTime));
    }

    private IEnumerator SpawnImpact(float delay)
    {
        yield return new WaitForSeconds(delay);
        Vector3 hitPosition = target.position + Random.insideUnitSphere * 0.2f;
        GameObject impact = Instantiate(impactPrefab, hitPosition, Quaternion.identity);
        Destroy(impact, 1f);
    }
}

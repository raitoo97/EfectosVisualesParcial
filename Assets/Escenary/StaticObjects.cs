using UnityEngine;
public class StaticObjects : MonoBehaviour, IImpact
{
    public void OnImpact(Vector3 hitPosition)
    {
        if (GameManager.instance.impactParticlesPrefab != null)
        {
            SoundManager.Instance?.PlayClip(SoundManager.Instance.GetAudioClip("ExplosionGun"), 0.1f, false);
            ParticleSystem ps = Instantiate(GameManager.instance.impactParticlesPrefab, hitPosition, Quaternion.identity);
            ps.Play();
            var main = ps.main;
            Destroy(ps.gameObject, main.duration);
        }
    }
}

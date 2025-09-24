using UnityEngine;
public class StaticObjects : MonoBehaviour, IImpact
{
    public void OnImpact(Vector3 hitPosition)
    {
        if (GameManager.instance.impactParticlesPrefab != null)
        {
            ParticleSystem ps = Instantiate(GameManager.instance.impactParticlesPrefab, hitPosition, Quaternion.identity);
            ps.Play();
            Destroy(ps.gameObject, ps.main.duration);
        }
    }
}

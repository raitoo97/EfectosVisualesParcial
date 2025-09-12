using UnityEngine;
public interface IShield
{
    public void ActivateShield();
    public void DeactivateShield();
    public void OnImpact(Vector3 hitPosition);
}

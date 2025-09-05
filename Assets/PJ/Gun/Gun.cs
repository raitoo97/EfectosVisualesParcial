using UnityEngine;
public class Gun : MonoBehaviour
{
    private void OnEnable()
    {
    }
    private void Update()
    {
        if (PlayerInputs.instance.GetInteract())
        {
            Shoot();
        }
    }
    public void Shoot()
    {
        print("Pew Pew");
    }
}

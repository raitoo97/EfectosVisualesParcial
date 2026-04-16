using UnityEngine;
public class PortalScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            // Aquí puedes agregar cualquier lógica adicional que quieras ejecutar cuando el jugador entre en el portal
        }
    }
}

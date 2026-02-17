using UnityEngine;
public class GalaxyRotate : MonoBehaviour
{
    void Update()
    {
       transform.Rotate(0,  10 * Time.deltaTime, 0);
    }
}

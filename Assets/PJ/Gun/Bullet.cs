using UnityEngine;
public class Bullet : MonoBehaviour
{
    private void Start()
    {
        Invoke("DesactivateBullet", 2f);
    }
    void Update()
    {
        this.transform.position += this.transform.forward * Time.deltaTime * 200;
    }
    public void DesactivateBullet()
    {
        this.gameObject.SetActive(false);
    }
}

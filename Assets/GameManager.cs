using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Player player;
    public ParticleSystem impactParticlesPrefab;
    public Material _glowMaterial;
    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    void Start()
    {
        _glowMaterial.SetInt("_ActivateOutLine", 0);
        player = FindObjectOfType<Player>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}

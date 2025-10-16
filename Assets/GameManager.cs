using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Player player;
    public ParticleSystem impactParticlesPrefab;
    public Material _glowMaterial;
    private Timer timer;
    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    private void OnEnable()
    {
        timer = new Timer();
    }
    private void Update()
    {
        timer?.OnUpdate();
    }
    void Start()
    {
        _glowMaterial.SetInt("_ActivateOutLine", 0);
        player = FindObjectOfType<Player>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        timer?.OnStart();
    }
    public void StartTimer()
    {
        if (timer != null)
            timer.stop = false;
    }
    public string GetTime { get => timer.GetTime; }
}

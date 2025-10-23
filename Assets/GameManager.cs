using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Player player;
    public ParticleSystem impactParticlesPrefab;
    public Material _glowMaterial;
    private Timer timer;
    public static Action OnGameOver;
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
        OnGameOver += GoToFinish;
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
        Time.timeScale = 1f;
        timer?.OnStart();
    }
    public void StartTimer()
    {
        if (timer != null)
            timer.stop = false;
    }
    public void GoToFinish()
    {
        StartCoroutine(OnFinishCorutine());
    }
    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }
    IEnumerator OnFinishCorutine()
    {
        CanvasManager.instance.FadeIn();
        yield return new WaitForSeconds(1.3f);
        CinematicDirector.instance.GetPlayableDirector(2).Play();
    }
    public string GetTime { get => timer.GetTime; }
}

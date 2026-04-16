using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
[DefaultExecutionOrder(-100)]
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
        timer.FinishTimer += ActivePortal;
    }
    private void Update()
    {
        timer?.OnUpdate();
    }
    void Start()
    {
        _glowMaterial.SetInt("_ActivateOutLine", 0);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        InitializeGameStart();
    }
    public void InitializeGameStart()
    {
        Time.timeScale = 1f;
        timer?.OnStart();
        SoundManager.Instance?.PlayClip(SoundManager.Instance.GetAudioClip("MusicBackground"), 0.8f, true);
    }
    public void StartTimer()
    {
        if (timer != null)
            timer.stop = false;
    }
    public void ActivePortal()
    {
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(OnFinishCorutine());
        }
        else
        {
            Debug.LogError("GameManager intentó iniciar ActivePortal, pero fue destruido/inactivo. Se abortó la corrutina.");
        }
    }
    public void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
    IEnumerator OnFinishCorutine()
    {
        yield return null;
        CinematicDirector.instance.GetPlayableDirector(2).Play();
    }
    private void OnDisable()
    {
        if (timer != null)
        {
            timer.FinishTimer -= ActivePortal;
            timer = null;
        }
    }
    public string GetTime { get => timer.GetTime; }
}

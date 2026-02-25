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
    public static Action OnGameOver;
    public GameObject _portal;
    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    private void OnEnable()
    {
        _portal.SetActive(false);
        timer = new Timer(_portal);
        OnGameOver -= GoToFinish;
        OnGameOver += GoToFinish;
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
        if (CinematicDirector.instance != null)
        {
            CinematicDirector.instance.ActivateCleanStair();
        }
        SoundManager.Instance?.PlayClip(SoundManager.Instance.GetAudioClip("MusicBackground"), 0.8f, true);
    }
    public void StartTimer()
    {
        if (timer != null)
            timer.stop = false;
    }
    public void GoToFinish()
    {
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(OnFinishCorutine());
        }
        else
        {
            Debug.LogError("GameManager intentó iniciar GoToFinish, pero fue destruido/inactivo. Se abortó la corrutina.");
        }
    }
    public void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
    IEnumerator OnFinishCorutine()
    {
        if (CanvasManager.instance != null)
            CanvasManager.instance.FadeIn();
        yield return new WaitForSeconds(1.3f);
        if (CinematicDirector.instance != null)
        {
            CinematicDirector.instance.GetPlayableDirector(2).Play();
        }
    }
    private void OnDisable()
    {
        OnGameOver -= GoToFinish;
        timer = null;
    }
    public string GetTime { get => timer.GetTime; }
}

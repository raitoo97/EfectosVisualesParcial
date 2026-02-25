using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    public Button startGameButon;
    public Button ExitButton;
    public GameObject panelMain;
    private bool _isStartGameActivate;
    private bool _isExitGameActivate;
    void Start()
    {
        startGameButon.onClick.AddListener(StartGame);
        ExitButton.onClick.AddListener(QuitGame);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1f;
        _isStartGameActivate = false;
        _isExitGameActivate = false;
        startGameButon.interactable = true;
        ExitButton.interactable = true;
        SoundManager.Instance?.PlayClipMenu(SoundManager.Instance.GetAudioClip("MenuBackGround"), 0.5f, true);
    }
    private void StartGame()
    {
        if (_isStartGameActivate) return;
        _isStartGameActivate = true;
        startGameButon.interactable = false;
        StartCoroutine(GoLevelCorutine());
    }
    private void QuitGame()
    {
        if (_isExitGameActivate) return;
        _isExitGameActivate = true;
        ExitButton.interactable = false;
        StartCoroutine(QuitCorutine());
    }
    IEnumerator GoLevelCorutine()
    {
        SoundManager.Instance?.PlayClipMenu(SoundManager.Instance.GetAudioClip("UIConfirmButton"), 0.5f, false);
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(1);
    }
    IEnumerator QuitCorutine()
    {
        SoundManager.Instance?.PlayClipMenu(SoundManager.Instance.GetAudioClip("UIConfirmButton"), 0.5f, false);
        yield return new WaitForSeconds(0.1f);
        Application.Quit();
        print("No funciona en Editor");
    }
}

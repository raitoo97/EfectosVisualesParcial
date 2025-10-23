using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    public Button startGameButon;
    public Button ExitButton;
    public GameObject panelMain;
    void Start()
    {
        startGameButon.onClick.AddListener(StartGame);
        ExitButton.onClick.AddListener(QuitGame);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1f;
    }
    private void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    private void QuitGame()
    {
        Application.Quit();
        print("No funciona en Editor");
    }
}

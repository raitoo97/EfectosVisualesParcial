using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EscapeTimerL0 : MonoBehaviour
{
    [SerializeField] private float duration = 60f;
    [SerializeField] private TextMeshProUGUI timerText;
    private float timeLeft;
    private bool isRunning;
    private int lastSecond = -1;
    public void StartTimer()
    {
        timeLeft = duration;
        isRunning = true;
        timerText.gameObject.SetActive(true);
        UpdateText();
    }
    private void Update()
    {
        if (!isRunning) return;
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0f)
        {
            isRunning = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            return;
        }
        UpdateText();
        PlayTick();
    }
    private void UpdateText()
    {
        int minutes = Mathf.FloorToInt(timeLeft / 60f);
        int seconds = Mathf.FloorToInt(timeLeft % 60f);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }
    private void PlayTick()
    {
        int currentSecond = Mathf.CeilToInt(timeLeft);
        if (currentSecond == lastSecond) return;
        lastSecond = currentSecond;
        SoundManager.Instance?.PlayClip(SoundManager.Instance.GetAudioClip("Tick"), 0.1f, false);
    }
}

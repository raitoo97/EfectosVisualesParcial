using System.Collections;
using TMPro;
using UnityEngine;
public class TutorialHintL0 : MonoBehaviour
{
    public static TutorialHintL0 Instance;
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI hintText;
    private Coroutine hideRoutine;
    private void Awake()
    {
        Instance = this;
        panel.SetActive(false);
    }
    public void ShowHint(string message, float duration)
    {
        hintText.text = message;
        panel.SetActive(true);
        if (hideRoutine != null) StopCoroutine(hideRoutine);
        hideRoutine = StartCoroutine(HideAfter(duration));
    }
    public void HideHint()
    {
        if (hideRoutine != null) StopCoroutine(hideRoutine);
        hideRoutine = null;
        panel.SetActive(false);
    }
    private IEnumerator HideAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        panel.SetActive(false);
        hideRoutine = null;
    }
}

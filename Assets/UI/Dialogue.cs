using System.Collections;
using UnityEngine;
using TMPro;
using System;
public class Dialogue : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private TextMeshProUGUI continuePrompt;
    private string[] currentLines;
    public float textSpeed;
    private int index;
    public static Action onDialogueEnd;
    void Start()
    {
        _text.text = string.Empty;
        this.gameObject.SetActive(false);
        NPCCameraManager.Instance.StartDialogue += StartDialogue;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && this.gameObject.activeSelf)
        {
            if (_text.text == currentLines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                _text.text = currentLines[index];
                ShowContinuePrompt();
            }
        }
    }
    public void StartDialogue(string[] newLines)
    {
        currentLines = newLines;
        this.gameObject.SetActive(true);
        index = 0;
        _text.text = string.Empty;
        StartCoroutine(TypeLine());
    }
    IEnumerator TypeLine()
    {
        foreach (char c in currentLines[index].ToCharArray())
        {
            _text.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        ShowContinuePrompt();
    }
    private void ShowContinuePrompt()
    {
        if (continuePrompt == null) return;
        bool isLastLine = index == currentLines.Length - 1;
        continuePrompt.text = isLastLine ? "[E] Cerrar" : "[E] Continuar";
        continuePrompt.gameObject.SetActive(true);
    }
    public void NextLine()
    {
        if (index < currentLines.Length - 1)
        {
            index++;
            _text.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            onDialogueEnd?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
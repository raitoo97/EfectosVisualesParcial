using UnityEngine;

public class TutorialHintSourceL0 : MonoBehaviour
{
    [TextArea(2, 4)][SerializeField] private string message;
    [SerializeField] private float duration = 6f;
    [SerializeField] private bool showOnDialogueEnd;
    [SerializeField] private bool showOnTriggerEnter;
    [SerializeField] private bool hideOnTriggerExit;
    private bool alreadyShown;

    private void OnEnable()
    {
        if (showOnDialogueEnd) DialogueL0.onDialogueEnd += Show;
    }

    private void OnDisable()
    {
        DialogueL0.onDialogueEnd -= Show;
    }

    public void Show()
    {
        if (alreadyShown) return;
        alreadyShown = true;
        TutorialHintL0.Instance.ShowHint(message, duration);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (showOnTriggerEnter && other.gameObject.layer == 6) Show();
    }

    private void OnTriggerExit(Collider other)
    {
        if (hideOnTriggerExit && other.gameObject.layer == 6) TutorialHintL0.Instance.HideHint();
    }
}
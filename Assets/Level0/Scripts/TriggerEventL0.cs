using UnityEngine;
using UnityEngine.Events;
public class TriggerEventL0 : MonoBehaviour
{
    [SerializeField] private UnityEvent onPlayerEnter;
    private bool alreadyTriggered;

    private void OnTriggerEnter(Collider other)
    {
        if (alreadyTriggered || other.gameObject.layer != 6) return;
        alreadyTriggered = true;
        onPlayerEnter?.Invoke();
    }
}

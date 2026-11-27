using UnityEngine;
using UnityEngine.Events;
public class ReactorValvesL0 : MonoBehaviour
{
    [SerializeField] private int valvesRequired = 2;
    [SerializeField] private UnityEvent onAllValvesActivated;
    private int activatedValves;
    public void ValveActivated()
    {
        activatedValves++;
        if (activatedValves == valvesRequired)
            onAllValvesActivated?.Invoke();
    }
}

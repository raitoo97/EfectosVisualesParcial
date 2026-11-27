using UnityEngine;

public class HeadLookL0 : MonoBehaviour
{
    [SerializeField] private Transform body;
    [SerializeField] private Transform head;
    [SerializeField] private Transform target;
    [SerializeField] private float turnDuration = 1f;
    private float weight;

    private void OnEnable()
    {
        weight = 0f;
    }

    private void LateUpdate()
    {
        if (head == null || body == null || target == null) return;
        weight = Mathf.MoveTowards(weight, 1f, Time.deltaTime / turnDuration);
        Vector3 toTarget = target.position - head.position;
        Quaternion turn = Quaternion.FromToRotation(body.forward, toTarget);
        float smoothWeight = Mathf.SmoothStep(0f, 1f, weight);
        head.rotation = Quaternion.Slerp(head.rotation, turn * head.rotation, smoothWeight);
    }
}

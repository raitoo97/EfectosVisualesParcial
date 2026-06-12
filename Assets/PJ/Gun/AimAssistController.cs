using UnityEngine;
public class AimAssistController : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private PlayerCameraMovement cameraController;
    [Header("Detección")]
    [SerializeField] private float maxDistance = Mathf.Infinity;
    [SerializeField] private float maxAngle = 40f;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private LayerMask obstacleMask;
    private Transform currentTarget;
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            Debug.Log("Buscando objetivo...");
            FindBestTarget();

            if (currentTarget != null)
            {
                Debug.Log("Objetivo encontrado: " + currentTarget.name);
                ApplyAimAssist();
            }
        }
        else
        {
            currentTarget = null;
        }
    }
    void FindBestTarget()
    {
        Collider[] hits = Physics.OverlapSphere(Camera.main.transform.position, maxDistance, enemyMask);
        float bestScore = float.MaxValue;
        Transform bestTarget = null;
        foreach (var hit in hits)
        {
            Vector3 targetPos = hit.bounds.center;
            Vector3 dirToTarget = (targetPos - Camera.main.transform.position).normalized;
            float angle = Vector3.Angle(Camera.main.transform.forward, dirToTarget);
            if (angle > maxAngle * 1.5f) continue;
            float distance = Vector3.Distance(Camera.main.transform.position, targetPos);
            if (Physics.Raycast(Camera.main.transform.position, dirToTarget, out RaycastHit hitInfo, maxDistance))
            {
                if (hitInfo.transform != hit.transform)
                    continue;
            }
            float score = angle * 0.7f + distance * 0.3f;
            if (score < bestScore)
            {
                bestScore = score;
                bestTarget = hit.transform;
            }
        }
        currentTarget = bestTarget;
    }
    void ApplyAimAssist()
    {
        if (cameraController == null || currentTarget == null) return;
        Vector3 camPos = Camera.main.transform.position;
        Vector3 currentForward = cameraController.GetCurrentForward();
        Vector3 targetPos = currentTarget.position + Vector3.up * 1.5f;
        Vector3 targetDir = (targetPos - camPos).normalized;
        float angle = Vector3.Angle(currentForward, targetDir);
        if (angle < 2f)
        {
            cameraController.SnapToDirection(targetDir);
            return;
        }
        if (Physics.Raycast(camPos, currentForward, out RaycastHit hit, maxDistance))
        {
            if (hit.transform == currentTarget) return;
        }
        float normalizedAngle = angle / maxAngle;
        float slerpSpeed = Mathf.Lerp(10f, 30f, normalizedAngle);
        Vector2 mouse = PlayerInputs.instance.GetMouseMovement();
        if (mouse.magnitude > 0.1f)
            slerpSpeed *= 2f;
        float snapBoost = Mathf.Lerp(1f, 2f, normalizedAngle);
        slerpSpeed *= snapBoost;
        cameraController.SlerpToDirection(targetDir, slerpSpeed);
    }
    private void OnDrawGizmos()
    {
        if (Camera.main == null) return;
        Vector3 origin = Camera.main.transform.position;
        // Radio de detección
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(origin, maxDistance);
        // Forward
        Gizmos.color = Color.red;
        Gizmos.DrawRay(origin, Camera.main.transform.forward * maxDistance);
        //  Bordes del cono
        Gizmos.color = Color.green;
        Vector3 leftBoundary = Quaternion.Euler(0, -maxAngle, 0) * Camera.main.transform.forward;
        Vector3 rightBoundary = Quaternion.Euler(0, maxAngle, 0) * Camera.main.transform.forward;
        Gizmos.DrawRay(origin, leftBoundary * maxDistance);
        Gizmos.DrawRay(origin, rightBoundary * maxDistance);
    }
}
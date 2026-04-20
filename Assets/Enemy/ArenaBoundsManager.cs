using UnityEngine;
public class ArenaBoundsManager : MonoBehaviour
{
    public static ArenaBoundsManager Instance;
    [Header("Arena Bounds")]
    [SerializeField] private BoxCollider arenaCollider;
    private void Awake()
    {
        Instance = this;
    }
    public bool IsInsideBounds(Vector3 position)
    {
        return arenaCollider.bounds.Contains(position);
    }
    public Vector3 ClampToBounds(Vector3 position)
    {
        Bounds b = arenaCollider.bounds;
        position.x = Mathf.Clamp(position.x, b.min.x, b.max.x);
        position.z = Mathf.Clamp(position.z, b.min.z, b.max.z);
        return position;
    }
    public Vector3 GetRandomPointInside()
    {
        Bounds b = arenaCollider.bounds;
        float x = Random.Range(b.min.x, b.max.x);
        float z = Random.Range(b.min.z, b.max.z);
        return new Vector3(x, b.center.y, z);
    }
    private void OnDrawGizmos()
    {
        if (arenaCollider == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(
            arenaCollider.bounds.center,
            arenaCollider.bounds.size
        );
    }
}

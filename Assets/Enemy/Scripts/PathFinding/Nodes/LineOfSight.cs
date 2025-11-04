using UnityEngine;
public static class LineOfSight
{
    public static bool IsOnSight(Vector3 start,Vector3 end)
    {
        var direction = end - start;
        return !Physics.Raycast(start, direction.normalized, out RaycastHit hitInfo, direction.magnitude, LayerMask.GetMask("Wall", "Ground"));
    }
}

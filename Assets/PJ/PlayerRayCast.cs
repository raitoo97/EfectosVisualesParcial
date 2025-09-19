using UnityEngine;
public class PlayerRayCast
{
    private Transform _CamerTransform;
    private float _rayDistance;
    private LayerMask _wallLayer;
    private Transform _groundChecker;
    private LayerMask _groundLayer;
    private float radiusChecker = 0.5f;
    public PlayerRayCast(Transform playerTransform, Transform groundChecker, LayerMask wallLayer, LayerMask groundLayer, float rayDistance)
    {
        _CamerTransform = playerTransform;
        _groundChecker = groundChecker;
        _wallLayer = wallLayer;
        _groundLayer = groundLayer;
        _rayDistance = rayDistance;
    }
    public bool CheckWall()
    {
        Ray ray = new Ray(_CamerTransform.position, _CamerTransform.forward);
        if(Physics.Raycast(ray, out RaycastHit hit, _rayDistance, _wallLayer))
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public bool CheckGrounded()
    {
        return Physics.CheckSphere(_groundChecker.position, radiusChecker, _groundLayer);
    }
}

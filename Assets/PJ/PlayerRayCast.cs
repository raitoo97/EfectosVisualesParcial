using UnityEngine;
public class PlayerRayCast
{
    private Transform _CamerTransform;
    private float _rayDistance;
    private LayerMask _wallLayer;
    private Transform _groundChecker;
    private LayerMask _groundLayer;
    private float radiusChecker = 0.5f;
    private float _enemyViewDistance;
    private float _interactDistance;
    private LayerMask _enemyLayer;
    public PlayerRayCast(Transform playerTransform, Transform groundChecker, LayerMask wallLayer, LayerMask groundLayer,LayerMask enemyLayer, float rayDistance,float enemyViewDistance,float interactDistance)
    {
        _CamerTransform = playerTransform;
        _groundChecker = groundChecker;
        _wallLayer = wallLayer;
        _groundLayer = groundLayer;
        _rayDistance = rayDistance;
        _enemyLayer = enemyLayer;
        _enemyViewDistance = enemyViewDistance;
        _interactDistance = interactDistance;
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
    public bool CheckViewEnemy()
    {
        Ray ray = new Ray(_CamerTransform.position, _CamerTransform.forward);
        if (Physics.Raycast(ray,out RaycastHit hit , _enemyViewDistance, _enemyLayer))
        {
            var enemy = hit.transform.gameObject.GetComponent<IEnemy>();
            if (enemy == null) return false;
            Debug.Log("Enemy in sight");
            return true;
        }
        return false;
    }
    public void CheckInteract()
    {
        Ray ray = new Ray(_CamerTransform.position, _CamerTransform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, _interactDistance))
        {
            if(hit.transform.gameObject.TryGetComponent<IInteractiveObject>(out var ObjectInteract))
            {
                ObjectInteract.Interact();
            }
        }
    }
}

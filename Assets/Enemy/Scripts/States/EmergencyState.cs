using UnityEngine;
public class EmergencyState : Istate
{
    private Enemy _enemy;
    private FSM _fsm;
    private float _raiseSpeed = 3f;
    private LayerMask _groundMask;
    private bool _isEscaping;
    public EmergencyState(Enemy enemy, FSM fsm)
    {
        _enemy = enemy;
        _fsm = fsm;
        _groundMask = LayerMask.GetMask("Ground");
    }
    public void OnEnter()
    {
        _enemy.ChangeMove(false);
        _isEscaping = true;
    }
    public void OnUpdate()
    {
        if (_isEscaping)
        {
            bool inside = Physics.CheckSphere(_enemy._emergencyCheck.position, 0.3f, _groundMask);
            if (inside)
            {
                _enemy.transform.position += Vector3.up * _raiseSpeed * Time.deltaTime;
                Debug.Log("Raising..." + _enemy.transform.position.y);
                return;
            }
            _isEscaping = false;
        }
        Debug.DrawRay(_enemy.transform.position + Vector3.up, Vector3.down * 10f, Color.magenta);
        if (Physics.Raycast(_enemy.transform.position + Vector3.up, Vector3.down, out RaycastHit hit, 10f, _groundMask))
        {
            Vector3 pos = _enemy.transform.position;
            pos.y = hit.point.y;
            _enemy.transform.position = pos;
            _fsm.ChangeState(FSM.StateID.Chase);
        }
    }
    public void OnExit()
    {
        _enemy.ChangeMove(true);
    }
}

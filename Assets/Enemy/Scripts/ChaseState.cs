using UnityEngine;
public class ChaseState : Istate
{
    private Enemy _enemy;
    public ChaseState(Enemy _enemy)
    {
        this._enemy = _enemy;
    }
    public void OnEnter()
    {
        Debug.Log("Entering Chase State");
    }
    public void OnExit()
    {
        Debug.Log("Exiting Chase State");
    }
    public void OnUpdate()
    {
        var dir = GameManager.instance.player.transform.position - _enemy.transform.position;
        dir.y = 0;
        if(dir != Vector3.zero)
        {
            var targetRotation = Quaternion.LookRotation(dir);
            _enemy.transform.rotation = Quaternion.RotateTowards(_enemy.transform.rotation, targetRotation, Time.deltaTime * 180f);
        }
    }
}

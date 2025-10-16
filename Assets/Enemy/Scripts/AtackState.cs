using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtackState : Istate
{
    private Enemy _enemy;
    private FSM _fsm;
    private Animator _animator;
    public AtackState(Enemy enemy,FSM fsm, Animator animator)
    {
        _fsm = fsm;
        _enemy = enemy;
        _animator = animator;
    }
    public void OnEnter()
    {
        Debug.Log("Enter Atack State");
    }

    public void OnExit()
    {
        Debug.Log("Exiting Atack State");
    }

    public void OnUpdate()
    {
        Debug.Log("Update Atack State");
    }
}

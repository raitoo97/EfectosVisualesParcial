using System.Collections.Generic;
public class FSM
{
    public enum StateID
    {
        Idle,
        Patrol,
        Chase,
        Attack
    }
    private Dictionary<StateID, Istate> _allStates = new Dictionary<StateID, Istate>();
    private Istate _currentState;
    public void AddState(StateID key, Istate value)
    {
        if (_allStates.ContainsKey(key)) return;
        _allStates.Add(key, value);
    }
    public void ChangeState(StateID key)
    {
        if (!_allStates.ContainsKey(key)) return;
        _currentState?.OnExit();
        _currentState = _allStates[key];
        _currentState?.OnEnter();
    }
    public void onUpdateState()
    {
        _currentState?.OnUpdate();
    }
}

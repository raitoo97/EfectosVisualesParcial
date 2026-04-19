using System.Collections.Generic;
public class FSM
{
    public enum StateID
    {
        Chase,
        Attack,
        Jump,
        Fall,
        Emergency
    }
    private Dictionary<StateID, Istate> _allStates = new Dictionary<StateID, Istate>();
    private Istate _currentState;
    public void AddState(StateID key, Istate value)
    {
        if (_allStates.ContainsKey(key)) return;
        _allStates.Add(key, value);
    }
    public void RemoveState(StateID key)
    {
        if (!_allStates.ContainsKey(key)) return;
        _allStates.Remove(key);
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
    public T GetState<T>() where T : class, Istate
    {
        foreach (var state in _allStates.Values)
        {
            if (state is T typedState)
                return typedState;
        }
        return null;
    }
    public Istate getCurrentState { get => _currentState; }
}

using UnityEngine;
public class Timer
{
    private float _min, _seg ,_mmm;
    private string _timerToString;
    public bool stop;
    public void OnStart()
    {
        stop = true;
        _min = 0;
        _seg = 5;
        _mmm = 0;
    }
    public void OnUpdate()
    {
        if (stop) return;
        _mmm -= Time.deltaTime * 60f;
        if (_mmm < 0)
        {
            _seg--;
            _mmm += 60f;
        }
        if (_seg < 0)
        {
            if (_min > 0)
            {
                _min--;
                _seg = 59;
            }
            else
            {
                _min = 0;
                _seg = 0;
                _mmm = 0;
                stop = true;
                GameManager.OnGameOver?.Invoke();
            }
        }
        int fracciones = Mathf.FloorToInt(_mmm);
        _timerToString = $"{_min:00}:{_seg:00}:{fracciones:00}";
    }
    public string GetTime { get => _timerToString; }
}

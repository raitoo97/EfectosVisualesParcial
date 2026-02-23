using System;
public class Life
{
    private float _currentHealth;
    private float _maxHealth;
    public Life(float maxHealth)
    {
        _maxHealth = maxHealth;
        _currentHealth = _maxHealth;
    }
    public void TakeDamage(float damage , Action Ondead)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            Ondead?.Invoke();
        }
    }
    public void SetHealthToMax()
    {
        _currentHealth = _maxHealth;
    }
    public float GetHealth { get => _currentHealth; }
    public float SetLife { set => _currentHealth = value; }
}

using System;
using UnityEngine;
public class Life
{
    private float _currentHealth;
    private float _maxHealth;
    public Action<float> ChangeLife;
    public Life(float maxHealth)
    {
        _maxHealth = maxHealth;
        _currentHealth = _maxHealth;
    }
    public void TakeDamage(float damage , Action Ondead)
    {
        _currentHealth -= damage;
        ChangeLife?.Invoke(_currentHealth / _maxHealth);
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
    public void Heal(float healAmount)
    {
        _currentHealth += healAmount;
        _currentHealth = Mathf.Min(_currentHealth,_maxHealth);
        ChangeLife?.Invoke(_currentHealth/_maxHealth);
    }
    public float GetHealth { get => _currentHealth; }
    public float SetLife { set => _currentHealth = value; }
    public float MaxHealth { get => _maxHealth; }
}

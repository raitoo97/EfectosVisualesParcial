using UnityEngine.SceneManagement;
public class Life
{
    private float _currentHealth;
    private float _maxHealth;
    public Life(float maxHealth)
    {
        _maxHealth = maxHealth;
        _currentHealth = _maxHealth;
    }
    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }
    public float GetHealth { get => _currentHealth; }
}

using UnityEngine;
public enum DropType
{
    None,
    Ammo,
    Health
}
public class DropSystem
{
    private float ammoChance = 0.3f;
    private float healthChance = 0.85f;
    private GameObject _ammoPrefab;
    private GameObject _healthPrefab;
    private Transform _transform;
    public DropSystem(GameObject ammoPrefab, GameObject healthPrefab,Transform transform)
    {
        _ammoPrefab = ammoPrefab;
        _healthPrefab = healthPrefab;
        _transform = transform;
    }
    private DropType GetDrop()
    {
        float roll = Random.value;
        if (roll < ammoChance)
            return DropType.Ammo;
        if (roll < healthChance)
            return DropType.Health;
        return DropType.None;
    }
    public void DropLoot()
    {
        DropType drop = GetDrop();
        GameObject prefabToSpawn = null;
        switch (drop)
        {
            case DropType.Ammo:
                prefabToSpawn = _ammoPrefab;
                break;

            case DropType.Health:
                prefabToSpawn = _healthPrefab;
                break;
        }
        if (prefabToSpawn != null)
        {
            Object.Instantiate(prefabToSpawn, _transform.position + Vector3.up, Quaternion.identity);
        }
    }
}

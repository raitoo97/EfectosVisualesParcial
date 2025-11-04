using System.Collections.Generic;
using UnityEngine;
public class GetStaticObjectsHandler
{
    public List<GameObject> Walls;
    public List<GameObject> Floor;
    public GetStaticObjectsHandler()
    {
        GameObject[] allObjects = Object.FindObjectsOfType<GameObject>();
        Walls = new List<GameObject>();
        Floor = new List<GameObject>();
        foreach (var obj in allObjects)
        {
            if (obj.layer == LayerMask.NameToLayer("Wall"))
                Walls.Add(obj);
            else if (obj.layer == LayerMask.NameToLayer("Ground"))
                Floor.Add(obj);
        }
        foreach (var wall in Walls)
        {
            if (wall.GetComponent<StaticObjects>() == null)
                wall.AddComponent<StaticObjects>();
        }
        foreach (var floor in Floor)
        {
            if (floor.GetComponent<StaticObjects>() == null)
                floor.AddComponent<StaticObjects>();
        }
    }
}

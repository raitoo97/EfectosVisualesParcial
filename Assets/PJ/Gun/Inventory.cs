using System.Collections.Generic;
public class Inventory <T>
{
    public List<T> items = new List<T>();
    public void AddItem(T item)
    {
        items.Add(item);
    }
    public void RemoveItem(T item)
    {
        if (items.Contains(item))
            items.Remove(item);
    }
    public int GetItemCount()
    {
        return items.Count;
    }
    public T GetItem()
    {
        if (items.Count > 0)
            return items[0];
        else
            return default(T);
    }
}

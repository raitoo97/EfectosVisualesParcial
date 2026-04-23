using System;
using UnityEngine;
public class PlayerRecolectObjects
{
    private Inventory<Bomb> _bombs;
    public Action<int> OnBombsCountChanged;
    public PlayerRecolectObjects(int initBombs)
    {
        _bombs = new Inventory<Bomb>();
        for (int i = 0; i < initBombs; i++)
        {
            _bombs.AddItem(default);
        }
    }
    public bool HasBombs()
    {
        return _bombs.GetItemCount() > 0;
    }
    public void AddBomb()
    {
        _bombs.AddItem(default);
        OnBombsCountChanged?.Invoke(_bombs.GetItemCount());
    }
    public void UseBomb()
    {
        if (_bombs.GetItemCount() <= 0) return;
        _bombs.RemoveItem(_bombs.GetItem());
        OnBombsCountChanged?.Invoke(_bombs.GetItemCount());
    }
}

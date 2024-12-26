using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class Item
{
    [SerializeField] private ItemBase _itemBase;
    public int Level { get; set; }

    public Item(ItemBase itemBase,int level)
    {
        _itemBase = itemBase;
        Level = level;
    }
    
    public ItemBase ItemBase
    {
        get { return _itemBase; }
    }

    public float Attack
    {
        get { return (Level * _itemBase.AttackRatio) + _itemBase.Attack; }
    }

    public float AttackSpeed
    {
        get { return (Level * _itemBase.AtkSpeedRatio ) + ItemBase.AttackSpeed; }
    }

    public int Price
    {
        get { return (Level * ItemBase.PriceUp) + ItemBase.PriceOrigin; }
    }
}

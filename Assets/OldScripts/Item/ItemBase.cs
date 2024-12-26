using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Item",menuName = "Item/Create new Item")]
public class ItemBase : ScriptableObject
{
    [SerializeField] private string name;
    [TextArea]
    [SerializeField] private string description;
    [SerializeField] private float attackRatio;
    [SerializeField] private float atkSpeedRatio;
    [SerializeField] private float attackSpeed;
    [SerializeField] private int priceOrigin;
    [SerializeField] private int priceUp;
    [SerializeField] private float attack;
    [SerializeField] private Sprite sprite;
    [SerializeField] private ItemType type;
    [SerializeField] private int diamondPrice;
    [SerializeField] private bool isPriceDiamond;


    public float AtkSpeedRatio
    {
        get { return atkSpeedRatio; }
    }
    public bool IsPriceDiamond
    {
        get { return isPriceDiamond; }
    }
    public int DiamondPrice
    {
        get { return diamondPrice; }
    }
    public int PriceUp
    {
        get { return priceUp; }
    }
    public int PriceOrigin
    {
        get { return priceOrigin; }
    }
    public ItemType Type
    {
        get { return type; }
    }
    public string Name
    {
        get { return name; }
    }
    public string Description
    {
        get { return description; }
    }
    public float AttackRatio
    {
        get { return attackRatio; }
    }
    public float AttackSpeed
    {
        get { return attackSpeed; }
    }
    public float Attack
    {
        get { return attack; }
    }
    public Sprite Sprite
    {
        get { return sprite; }
    }
}

public enum ItemType
{
    Weapon,
    Armor
}

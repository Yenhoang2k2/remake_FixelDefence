using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
[System.Serializable]
public class Hero
{
    [SerializeField] private HeroBase _heroBase;
    public Item Sword { get; set; }
    public Item Armor { get; set; }
    public int Level { get; set; }
    public Hero(HeroBase heroBase,int level)
    {
        _heroBase = heroBase;
        Level = level;
    }
    public HeroBase HeroBase
    {
        get { return _heroBase; }
    }
    public int PriceCurrent()
    {
        if (Level == 0)
        {
            return HeroBase.Price;
        }
        else
        {
            return Level * HeroBase.PriceIncreacePerLevel + HeroBase.PriceIncreacePerLevel;
        }
    }
    public int AttackApply()
    {
        if(Sword != null && Armor != null) 
            return (int)(Level * HeroBase.AtkIncreacePerLevel + HeroBase.Attack) + (int)(Sword.Attack) +(int)(Armor.Attack);
        else if (Sword != null)
            return (int)(Level * HeroBase.AtkIncreacePerLevel + HeroBase.Attack) + (int)(Sword.Attack) ;
        else if (Armor != null)
            return (int)(Level * HeroBase.AtkIncreacePerLevel + HeroBase.Attack) +(int)(Armor.Attack);
        else
            return (int)(Level * HeroBase.AtkIncreacePerLevel + HeroBase.Attack);
    }
    public float AtkSpeedApply()
    {
        if(Sword != null && Armor != null) 
            return (HeroBase.AtkSpeed) / ((Sword.AttackSpeed) * (Armor.AttackSpeed));
        else if (Sword != null)
            return (HeroBase.AtkSpeed) / (Sword.AttackSpeed);
        else if (Armor != null)
            return (HeroBase.AtkSpeed) / (Armor.AttackSpeed);
        else
            return (HeroBase.AtkSpeed);
    }
}

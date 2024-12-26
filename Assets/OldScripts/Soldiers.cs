using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldiers : MonoBehaviour
{
    public static Soldiers Instance;
    [SerializeField] private List<SoldierUnit> soldierUnits;
    [SerializeField] private Sprite spriteSoldier;
    [SerializeField] private Sprite spriteBullet;
    [SerializeField] private float attack;
    [SerializeField] private int atkRange;
    [SerializeField] private float atkSpeed;
    [SerializeField] private float speedBullet;
    [SerializeField] private int price;
    [SerializeField] private int priceIncreacePerLevel;
    public int Level { get; set; }

    public int PriceCurrent
    {
        get { return price + (Level * priceIncreacePerLevel); }
    }
    private void Start()
    {
        Instance = this;
        SetSoldiers();
    }
    public void SetSoldiers()
    {
        foreach (var soldierUnit in soldierUnits)
        {
            soldierUnit.SetSoldiers(Level,atkRange,attack,speedBullet,atkSpeed,spriteSoldier,spriteBullet);
        }
    }

    public void UpdateSoldiersAttack()
    {
        foreach (var soldierUnit in soldierUnits)
        {
            soldierUnit.UpdateSoldierAttack();
        }
    }
}

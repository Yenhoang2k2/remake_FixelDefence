using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TowerUnit : MonoBehaviour
{
    public static TowerUnit Instance;
    [SerializeField] private Image health;
    [SerializeField] private Text healthText;
    [SerializeField] private int priceBase;
    [SerializeField] private int maxHpIncreacePerLv;
    [SerializeField] private int priceIncreacePerLv;
    [SerializeField] int hpbase;
    public int Level { get; set; }
    public float HpCurrent { get; set; }
    private void Start()
    {
        Instance = this;
    }

    public void SetUpTowerUnit()
    {
        HpCurrent = MaxHp;
        UpdateHp();
    }

    public int MaxHp
    {
        get { return hpbase + ( Level * maxHpIncreacePerLv); }
    }
    public int Price
    {
        get { return priceBase + ( Level * priceIncreacePerLv); }
    }

    public void UpdateHp()
    {
        health.fillAmount = HpCurrent / MaxHp;
        healthText.text = Mathf.FloorToInt(HpCurrent).ToString();
    }

    public bool TakeDame(float dame)
    {
        HpCurrent -= dame;
        UpdateHp();
        if (HpCurrent <= 0)
        {
            HpCurrent = 0;
            FireBaseAuthentication.Instance.FirebaseUpdateWhenEndBattle();
            return true;
        }
        return false;
    }
}
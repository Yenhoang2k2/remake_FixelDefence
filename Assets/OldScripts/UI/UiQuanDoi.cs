using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UiQuanDoi : MonoBehaviour
{
    public static UiQuanDoi Instance;
    [SerializeField] private Button btnUpTower;
    [SerializeField] private Button btnUpSoldier;
    [SerializeField] private Text goldUpTower;
    [SerializeField] private Text lvTower;
    [SerializeField] private TowerUnit towerUnit;
    [SerializeField] private Soldiers soldiers;
    [SerializeField] private Text goldUpSoldier;
    [SerializeField] private Text levelSoldier;
    private ResourcesHub _resourceHub;
    private FireBaseAuthentication _firebaseAuthentication;

    private void Start()
    {
        Instance = this;
        btnUpTower.onClick.AddListener(UpLevelTower);
        btnUpSoldier.onClick.AddListener(UpLevelSoldier);
    }
    public void SetUpUiSoldier()
    {
        goldUpSoldier.text = "-" + soldiers.PriceCurrent;
        levelSoldier.text = "lv" + soldiers.Level;
    }
    public void SetUpUiTower()
    {
        goldUpTower.text = "-" + towerUnit.Price;
        lvTower.text = "lv" + towerUnit.Level;
    }
    public void UpLevelTower()
    {
        _firebaseAuthentication = FireBaseAuthentication.Instance;
        _resourceHub  = ResourcesHub.Instance;
        int price = towerUnit.Price;
        int gold = _resourceHub.Monney;
        if (gold < price)
        {
            Debug.Log("thiếu tài nguyên để thăng cấp");
            return;
        }
        int goldCurrent = gold - price;
        int towerunitCurrent = towerUnit.Level += 1;
        _firebaseAuthentication.SaveLevelTower(towerunitCurrent);
        SetAndupdateResource(goldCurrent);
    }

    public void UpLevelSoldier()
    {
        _firebaseAuthentication = FireBaseAuthentication.Instance;
        _resourceHub  = ResourcesHub.Instance;
        int price = soldiers.PriceCurrent;
        int gold = _resourceHub.Monney;
        if (gold < price)
        {
            Debug.Log("thiếu tài nguyên để thăng cấp");
            return;
        }
        int goldCurrent = gold - price;
        int soldiersCurrent = soldiers.Level += 1;
        _firebaseAuthentication.SaveLevelSoldier(soldiersCurrent);
        SetAndupdateResource(goldCurrent);
    }

    public void SetAndupdateResource(int current)
    {
        int monneyCurrent = current;
        int diamond = _resourceHub.Diamond;
        _resourceHub.SetResources(diamond,monneyCurrent);
        _firebaseAuthentication.SaveResources(diamond,monneyCurrent);
        _firebaseAuthentication.LoadResources();
        SetUpUiTower();
        SetUpUiSoldier();
        towerUnit.SetUpTowerUnit();
    }
}

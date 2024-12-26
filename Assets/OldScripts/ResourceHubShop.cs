using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceHubShop : MonoBehaviour
{
    public static ResourceHubShop Instance;
    [SerializeField] private Text gold;
    [SerializeField] private Text diamond;

    private void Start()
    {
        Instance = this;
    }

    public void SetResourceHubShop(string gold,string diamond)
    {
        this.gold.text = gold;
        this.diamond.text = diamond;
    }
}

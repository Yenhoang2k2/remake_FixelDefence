using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SheetHero : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] Image avatar;
    [SerializeField] Text nameText;
    [SerializeField] Text priceText;
    [SerializeField] Text levelText;
    
    public Image Avatar
    {
        get { return avatar; }
    }
    
    public Text NameText
    {
        get { return nameText; }
    }
    public Text PriceText
    {
        get { return priceText; }
    }
    public Text LevelText
    {
        get { return levelText; }
    }

    public Button Button
    {
        get { return button; }
    }
}

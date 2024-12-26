using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UiInformationItem : MonoBehaviour
{
    public static UiInformationItem instance;
    [SerializeField] Image avatar;
    [SerializeField] Text nameText;
    [SerializeField] Text priceText;
    [SerializeField] Text levelText;
    [SerializeField] private Button btnUpLevel;
    [SerializeField] private Button btnPut;
    [SerializeField] private Button btnX;
    [SerializeField] private Text txtDescription;
    [SerializeField] private Text txtAttack;
    [SerializeField] private Text txtAtkSpeed;
    [SerializeField] private ListAllHero _heros;
    [SerializeField] private ListItem _listItem;
    [SerializeField] private GameObject _uiBalo;
    private Item item;
    int remove;
    public Hero Hero { get; set; }
    
    private void Start()
    {
        instance = this;
        btnX.onClick.AddListener(OffInformation);
        btnPut.onClick.AddListener(ClickPut);
        btnUpLevel.onClick.AddListener(ClickUpLevel);
    }

    public void ClickUpLevel()
    {
        FireBaseAuthentication fireBaseAuthentication = FireBaseAuthentication.Instance;
        int diamond = ResourcesHub.Instance.Diamond;
        int monney = ResourcesHub.Instance.Monney;
        if (monney >= item.Price)
        {
            item.Level += 1;
            int currentMoney = monney - item.Price;
            ResourcesHub.Instance.SetResources(diamond,currentMoney);
            fireBaseAuthentication.SaveResources(diamond,currentMoney);
            fireBaseAuthentication.LoadResources();
            SetUpUiInformationItem(Hero,item);
            fireBaseAuthentication.SaveItem(ListItem.Instante);
            ListItem.Instante.Items.Clear();
            fireBaseAuthentication.LoadItem();
        }
    }
    public void SetUpUiInformationItem(Hero hero,Item pItem)
    {
        this.Hero = hero;
        item = pItem;
        avatar.sprite = pItem.ItemBase.Sprite;
        nameText.text = "Name :" + pItem.ItemBase.Name;
        priceText.text = "Price Up level :" + pItem.Price;
        levelText.text = "Lv : " + pItem.Level;
        txtAttack.text = "Attack : " + item.Attack;
        txtAtkSpeed.text = "AttackSpeed : " + item.AttackSpeed +" Lần";
        txtDescription.text = "" + pItem.ItemBase.Description;
    }
    public void ClickPut()
    {
        foreach (var hero in _heros.Heros)
        {
            if (hero.HeroBase.NewName == Hero.HeroBase.NewName)
            {
                if (item.ItemBase.Type == ItemType.Weapon)
                {
                    RemoveItem();
                    if (Hero.Sword != null)
                        ListItem.Instante.Items.Add(Hero.Sword);
                    hero.Sword = item;
                }
                else
                {
                    RemoveItem();
                    if (Hero.Armor != null)
                        ListItem.Instante.Items.Add(Hero.Armor);
                    hero.Armor = item;
                }
                ListHeros.Instance.CheckClickSheet(Hero);
            }
        }
        FireBaseAuthentication fireBaseAuthentication = FireBaseAuthentication.Instance;
        fireBaseAuthentication.SaveItem(ListItem.Instante);
        ListItem.Instante.Items.Clear();
        fireBaseAuthentication.LoadItem();
        fireBaseAuthentication.SaveItemHero(ListHeros.Instance.heroes);
        fireBaseAuthentication.LoadItemOfHero();
        gameObject.SetActive(false);
    }

    public void RemoveItem()
    {
        
        int leght = _listItem.Items.Count;

        for (int i = 0;i < leght;i++)
        {
            if (_listItem.Items[i].ItemBase.Name == item.ItemBase.Name && _listItem.Items[i].Level == item.Level)
            {
                remove = i;
            }
        }
        _uiBalo.gameObject.SetActive(false);
        _listItem.Items.RemoveAt(remove);
    }
    public Text TxtDescription
    {
        get { return txtDescription; }
    }
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

    public Button BtnPut
    {
        get { return btnPut; }
    }
    public void OffInformation()
    {
        gameObject.SetActive(false);
    }
}

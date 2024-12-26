using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class InformationOfShop : MonoBehaviour
{
    public static InformationOfShop instance;
    [SerializeField] Image avatar;
    [SerializeField] Text nameText;
    [SerializeField] Text priceText;
    [SerializeField] private Text priceDiamondText;
    [SerializeField] Text levelText;
    [SerializeField] private Button btnBuy;
    [SerializeField] private Button btnX;
    [SerializeField] private Text txtDescription;
    [SerializeField] private Text txtAttack;
    [SerializeField] private Text txtAttackSpeed;
    [SerializeField] private GameObject TextDiamond;
    [SerializeField] private GameObject TextMonney;
    private ItemBase item;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        instance = this;
        btnX.onClick.AddListener(OffInformation);
        btnBuy.onClick.AddListener(ClickButtonBuy);
    }

    public void ClickButtonBuy()
    {
        FireBaseAuthentication fireBaseAuthentication = FireBaseAuthentication.Instance;
        int diamond = ResourcesHub.Instance.Diamond;
        int monney = ResourcesHub.Instance.Monney;
        int currentMoney = monney;
        int currentDiamond = diamond;
        if (item.IsPriceDiamond == true && diamond >= item.DiamondPrice)
        {
            int random = Random.Range(1, 30);
            Item itemp = new Item(this.item, random);
            ListItem.Instante.Items.Add(itemp);
            currentDiamond = diamond - item.DiamondPrice;
            UIManager.Instance.OnDialog();
            StartCoroutine(UIManager.Instance.Dialog.SetDialogSmooth("Mua trang bị thành công !"));
        }
        else if(item.IsPriceDiamond == false && monney >= item.PriceOrigin)
        {
            Item item = new Item(this.item, 1);
            ListItem.Instante.Items.Add(item);
            currentMoney = monney - this.item.PriceOrigin;
            UIManager.Instance.OnDialog();
            StartCoroutine(UIManager.Instance.Dialog.SetDialogSmooth("Mua trang bị thành công !"));
        }
        ResourceHubShop.Instance.SetResourceHubShop(currentMoney.ToString(),currentDiamond.ToString());
        fireBaseAuthentication.SaveResources(currentDiamond,currentMoney);
        ResourcesHub.Instance.SetResources(currentDiamond,currentMoney);
        fireBaseAuthentication.LoadResources();
        fireBaseAuthentication.SaveItem(ListItem.Instante);
        ListItem.Instante.Items.Clear();
        fireBaseAuthentication.LoadItem();
    }
    public void SetUpUiInformationItem(ItemBase pItem)
    {
        item = pItem;
        avatar.sprite = pItem.Sprite;
        nameText.text = "Name :" + pItem.Name;
        if (item.IsPriceDiamond == true)
        {
            TextDiamond.SetActive(true);
            TextMonney.SetActive(false);
            priceDiamondText.text = "Price :" + pItem.DiamondPrice;
            levelText.text = "Lv : 1 - 30 ngẫu nhiên";
            float attack = item.Attack + 1 * item.AttackRatio;
            float atkSpeed = item.AttackSpeed + 1 * item.AtkSpeedRatio;
            float atkSpeedLv30 = item.AttackSpeed + 30 * item.AtkSpeedRatio;
            float attackLv30 = item.Attack + 30 * item.AttackRatio;
            txtAttack.text = "Attack : " + attack + " - " + attackLv30;
            txtAttackSpeed.text = "AtkSpeed : " + atkSpeed + " - " + atkSpeedLv30 + " lần";
        }
        else
        {
            TextDiamond.SetActive(false);
            TextMonney.SetActive(true);
            priceText.text = "Price :" + pItem.PriceOrigin;
            levelText.text = "Lv : " + 1;
            float attack = item.Attack + 1 * item.AttackRatio;
            float atkSpeed = item.AttackSpeed + 1 * item.AtkSpeedRatio;
            txtAttack.text = "Attack : " + attack;
            txtAttackSpeed.text = "AtkSpeed : " + atkSpeed + " lần";
        }
        txtDescription.text = "" + pItem.Description;
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
    public void OffInformation()
    {
        gameObject.SetActive(false);
    }
}

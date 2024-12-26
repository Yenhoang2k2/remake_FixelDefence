using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public static ShopItem Instance;
    [SerializeField] private ListItemBase listItemBase;
    [SerializeField] private ListItemDiamond listItemDiamond;
    [SerializeField] private Transform containerVang;
    [SerializeField] private Transform containerDiamond;
    [SerializeField] private Button btnX;
    [SerializeField] private GameObject btnItem;
    [SerializeField] private GameObject gojUiInformationItemOfShop;
    [SerializeField] private InformationOfShop informationItemOfShop;
    [SerializeField] private ResourceHubShop resourceHubShop;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        resourceHubShop.SetResourceHubShop(ResourcesHub.Instance.Monney.ToString(),ResourcesHub.Instance.Diamond.ToString());
        btnX.onClick.AddListener(ClickX);
        SetShopItem();
    }

    public void ClickX()
    {
        gameObject.SetActive(false);
    }
    public void SetShopItem()
    {
        DetroyGameobjectInShopVang(() =>
        {
            if(listItemBase.ItemBases == null) return;
            foreach (var item in listItemBase.ItemBases)
            {
                var tempory = Instantiate(btnItem, containerVang);
                var itemUnitShop = tempory.GetComponent<ItemUnitShop>();
                itemUnitShop.ItemBase = item;
                itemUnitShop.Image.sprite = item.Sprite;
                itemUnitShop.BtnItem.onClick.AddListener(()=>OnUiInformationItem(itemUnitShop));
            }
        });
        DetroyGameobjectInShopDiamond(() =>
        {
            if(listItemDiamond.List == null) return;
            foreach (var itemdiamond in listItemDiamond.List)
            {
                var tempory = Instantiate(btnItem, containerDiamond);
                var itemUnitShop = tempory.GetComponent<ItemUnitShop>();
                itemUnitShop.ItemBase = itemdiamond;
                itemUnitShop.Image.sprite = itemdiamond.Sprite;
                itemUnitShop.BtnItem.onClick.AddListener(()=>OnUiInformationItem(itemUnitShop));
            }
        });
    }
    public void OnUiInformationItem(ItemUnitShop item)
    {
        gojUiInformationItemOfShop.SetActive(true);
        informationItemOfShop.SetUpUiInformationItem(item.ItemBase);
    }
    public void DetroyGameobjectInShopVang(Action callback)
    {
        if(containerVang == null) return;
        foreach (Transform item in containerVang)
        {
            Destroy(item.gameObject);
        }
        callback?.Invoke();
    }
    public void DetroyGameobjectInShopDiamond(Action callback)
    {
        if(containerDiamond == null) return;
        foreach (Transform item in containerDiamond)
        {
            Destroy(item.gameObject);
        }
        callback?.Invoke();
    }
}

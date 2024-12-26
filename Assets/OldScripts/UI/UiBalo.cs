using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UiBalo : MonoBehaviour
{
    public static UiBalo Instance;
    public List<Item> Items { get; set; }
    [SerializeField] private ListItem listItem;
    [SerializeField] private Transform container;
    [SerializeField] private GameObject btnItemUnit;
    [SerializeField] private GameObject gojUiInformationItem;
    [SerializeField] private UiInformationItem _uiInformationItem;
    [SerializeField] private Button btnX;
    private Hero _hero;
    private int stt;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        btnX.onClick.AddListener(OffGameObject);
        Items = listItem.Items;
    }

    public void SetBalo(Hero hero)
    {
        _hero = hero;
        DetroyGameobjectInBalo(() =>
        {
            if(listItem.Items == null) return;
            foreach (var item in listItem.Items)
            {
                var tempory = Instantiate(btnItemUnit, container);
                var itemUnit = tempory.GetComponent<ItemUnit>();
                itemUnit.stt = stt;
                itemUnit.Item = item;
                itemUnit.Hero = hero;
                itemUnit.TxtLevel.text = item.Level.ToString();
                itemUnit.Image.sprite = item.ItemBase.Sprite;
                itemUnit.BtnItem.onClick.AddListener(()=>OnUiInformationItem(itemUnit));
            }
        });
    }

    public void OnUiInformationItem(ItemUnit item)
    {
        gojUiInformationItem.SetActive(true);
        _uiInformationItem.SetUpUiInformationItem(_hero,item.Item);
        
    }
    public void DetroyGameobjectInBalo( Action callback)
    {
        if(container == null) return;
        foreach (Transform item in container)
        {
            Destroy(item.gameObject);
        }
        callback?.Invoke();
    }

    public void OffGameObject()
    {
        gameObject.SetActive(false);
    }

}

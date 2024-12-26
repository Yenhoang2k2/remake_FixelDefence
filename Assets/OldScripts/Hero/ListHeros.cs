using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ListHeros : MonoBehaviour
{
    [SerializeField] private GameObject uiInformation;
    [SerializeField] private List<GameObject> poiHero;
    [SerializeField] private Information information;
    [SerializeField] private Transform container;
    [SerializeField] private GameObject heroBar;
    [SerializeField] private Button btnX;
    public static ListHeros Instance;
    public List<Hero> heroes;
    public BtnHero BtnHero { get; set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SetListHero();
        btnX.onClick.AddListener(OffListHero);
        information.BtnPut.onClick.AddListener(ClickPut);
        information.BtnRemove.onClick.AddListener(ClickRemove);
        information.BtnSwap.onClick.AddListener(ClickSwap);
    }
    
    public void SetListHero()
    {
        DetroyAllChildOfObject(() =>
        {
            foreach (var hero in heroes)
            {
                var temporary = Instantiate(heroBar, container);
                var sheetHero = temporary.GetComponent<SheetHero>();
                sheetHero.NameText.text = hero.HeroBase.NewName;
                sheetHero.LevelText.text = hero.Level.ToString();
                sheetHero.PriceText.text = "Price : " + hero.PriceCurrent();
                sheetHero.Avatar.sprite = hero.HeroBase.Sprite;
                sheetHero.Button.onClick.AddListener(()=>CheckClickSheet(hero));
            }
        });
    }

    public void DetroyAllChildOfObject(Action CallBack)
    {
        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }
        CallBack?.Invoke();
    }
    public void CheckClickSheet(Hero hero)
    {
        uiInformation.SetActive(true);
        information.Hero = hero;
        information.Avatar.sprite = hero.HeroBase.Sprite;
        information.LevelText.text = "Level : " +hero.Level;
        information.NameText.text = "Name : " + hero.HeroBase.NewName;
        information.PriceText.text = "Price : " + hero.PriceCurrent();
        information.TxtAttack.text = "Attack : " + hero.AttackApply();
        information.TxtAttackSpeed.text = "AtkSpeed : " + hero.AtkSpeedApply().ToString("F2")+"/s";
        information.TxtDescription.text = hero.HeroBase.Skills[0].Description;
        if (hero.Armor != null)
        {
            information.ImgArmor.sprite = hero.Armor.ItemBase.Sprite;
            information.TxtLevelArmor.text = hero.Armor.Level.ToString();
        }
        else
        {
            information.ImgArmor.sprite = null;
            information.TxtLevelArmor.text = "";
        }
        if (hero.Sword != null)
        {
            information.ImgSword.sprite = hero.Sword.ItemBase.Sprite;
            information.TxtLevelSword.text = hero.Sword.Level.ToString();
        }
        else
        {
            information.ImgSword.sprite = null;
            information.TxtLevelSword.text = "";
        }   
        SetButtonChangeHero(hero);
    }

    public void SetButtonChangeHero(Hero hero)
    {
        if (BtnHero.CountHero == 0)
        {
            information.NullInformation();
        }
        else if (BtnHero.CountHero >= 1)
        {
            uiInformation.SetActive(true);
            var temporary = BtnHero.PointHero.transform.GetChild(0).GetComponent<HeroUnit>();
            if (temporary.Hero == hero)
            {
                information.Alike();
            }
            else
            {
                information.Different();
            }
        }
    }

    public void ClickPut()
    {
        SweetAllpoiHero();
        BtnHero.CreateHeroUnit(information.Hero);
        uiInformation.SetActive(false);
        gameObject.SetActive(false);
        
    }

    public void ClickRemove()
    {
        BtnHero.DetroyHeroUnit();
        uiInformation.SetActive(false);
        gameObject.SetActive(false);
    }
    public void ClickSwap()
    {
        SweetAllpoiHero();
        BtnHero.DetroyHeroUnit();
        BtnHero.CreateHeroUnit(information.Hero);
        uiInformation.SetActive(false);
        gameObject.SetActive(false);
    }
    public void OffListHero()
    {
        gameObject.SetActive(false);
    }

    public void SweetAllpoiHero()
    {
        foreach (var poi in poiHero)
        {
            if (poi.transform.childCount > 0)
            {
                HeroUnit heroUnit = poi.transform.GetChild(0).GetComponent<HeroUnit>();
                if (heroUnit.Hero.HeroBase.name == information.Hero.HeroBase.name)
                {
                    Destroy(poi.transform.GetChild(0).gameObject);
                }
            }
        }
    }
}


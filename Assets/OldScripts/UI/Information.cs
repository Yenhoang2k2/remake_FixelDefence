using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Information : MonoBehaviour
{
    public static Information instance;
    [SerializeField] Image avatar;
    [SerializeField] Text nameText;
    [SerializeField] Text priceText;
    [SerializeField] Text levelText;
    [SerializeField] private Button btnUpLevel;
    [SerializeField] private Button btnPut;
    [SerializeField] private Button btnSwap;
    [SerializeField] private Button btnRemove;
    [SerializeField] private Button btnX;
    [SerializeField] private Button btnChangeItem;
    [SerializeField] private Button btnRemoveItem;
    [SerializeField] private GameObject gojBtnPut;
    [SerializeField] private GameObject gojBtnSwap;
    [SerializeField] private GameObject gojBtnRemove;
    [SerializeField] private GameObject gojUiBalo;
    [SerializeField] private UiBalo _uiBalo;
    [SerializeField] private Image imgSword;
    [SerializeField] private Image imgArmor;
    [SerializeField] private Text txtLevelSword;
    [SerializeField] private Text txtLevelArmor;
    [SerializeField] private Text txtDescription;
    [SerializeField] private Text txtAttack;
    [SerializeField] private Text txtAttackSpeed;
    public Hero Hero { get; set; }
    
    private void Start()
    {
        instance = this;
        btnX.onClick.AddListener(OffInformation);
        btnUpLevel.onClick.AddListener(UpdateLevelHero);
        btnChangeItem.onClick.AddListener(ClickButtonChange);
        btnRemoveItem.onClick.AddListener(ClickButtonRemove);
        
    }

    public void ClickButtonRemove()
    {
        foreach (var hero in ListAllHero.Instance.Heros)
        {
            if (hero.HeroBase.NewName == Hero.HeroBase.NewName)
            {
                if(Hero.Armor != null)
                    ListItem.Instante.Items.Add(Hero.Armor);
                if(Hero.Sword != null)
                    ListItem.Instante.Items.Add(Hero.Sword);
                hero.Armor = null;
                hero.Sword = null;
            }
        }
        FireBaseAuthentication fireBaseAuthentication = FireBaseAuthentication.Instance;
        fireBaseAuthentication.SaveItem(ListItem.Instante);
        ListItem.Instante.Items.Clear();
        fireBaseAuthentication.LoadItem();
        fireBaseAuthentication.SaveItemHero(ListHeros.Instance.heroes);
        fireBaseAuthentication.LoadItemOfHero();
        ListHeros.Instance.CheckClickSheet(Hero);
    }
    public void ClickButtonChange()
    {
        gojUiBalo.SetActive(true);
        _uiBalo.SetBalo(Hero);
    }
    public void UpdateLevelHero()
    {
        foreach (var hero in ListAllHero.Instance.Heros)
        {
            if (Hero.HeroBase.NewName == hero.HeroBase.NewName)
            {
                ResourcesHub resourcesHub = ResourcesHub.Instance;
                var firebaseAuthentication = FireBaseAuthentication.Instance;
                int diamond = resourcesHub.Diamond;
                int monney = resourcesHub.Monney;
                if (hero.PriceCurrent() < monney)
                {
                    int monneyCurrent = monney - hero.PriceCurrent();
                    resourcesHub.SetResources(diamond,monneyCurrent);
                    hero.Level += 1;
                    firebaseAuthentication.SaveResources(diamond,monneyCurrent);
                    firebaseAuthentication.LoadResources();
                    firebaseAuthentication.SaveInforHero();
                    firebaseAuthentication.LoadInforHero();
                    ListHeros.Instance.heroes = ListAllHero.Instance.Heros;
                    ListHeros.Instance.SetListHero();
                    ListHeros.Instance.CheckClickSheet(Hero);
                }
            }
        }
    }

    public Text TxtAttack
    {
        get { return txtAttack; }
    }

    public Text TxtAttackSpeed
    {
        get { return txtAttackSpeed; }
    }
    public Text TxtDescription
    {
        get { return txtDescription; }
    }
    public Text TxtLevelSword
    {
        get { return txtLevelSword; }
    }
    public Text TxtLevelArmor
    {
        get { return txtLevelArmor; }
    }
    public Image Avatar
    {
        get { return avatar; }
    }
    public Image ImgSword
    {
        get { return imgSword; }
    }
    public Image ImgArmor
    {
        get { return imgArmor; }
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
    public Button BtnSwap
    {
        get { return btnSwap; }
    }
    public Button BtnRemove
    {
        get { return btnRemove; }
    }
    public GameObject GojBtnPut
    {
        get { return gojBtnPut; }
    }
    public GameObject GojBtnSwap
    {
        get { return gojBtnSwap; }
    }
    public GameObject GojBtnRemove
    {
        get { return gojBtnRemove; }
    }

    public void NullInformation()
    {
        gojBtnRemove.SetActive(false);
        gojBtnPut.SetActive(true);
        gojBtnSwap.SetActive(false);
    }

    public void Alike()
    {
        gojBtnRemove.SetActive(true);
        gojBtnPut.SetActive(false);
        gojBtnSwap.SetActive(false);
    }
    public void Different()
    {
        gojBtnRemove.SetActive(false);
        gojBtnPut.SetActive(false);
        gojBtnSwap.SetActive(true);
    }

    public void OffInformation()
    {
        gameObject.SetActive(false);
    }
}

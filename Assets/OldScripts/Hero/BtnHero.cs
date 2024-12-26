
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Button = UnityEngine.UI.Button;

public class BtnHero : MonoBehaviour
{
    [SerializeField] private GameObject heroUnit;
    [SerializeField] private GameObject pointHero;
    [SerializeField] private Button btnBtnHero;
    [SerializeField] private GameObject uilistHero;
    private int _heroCount;

    public int CountHero
    {
        get { return _heroCount; }
    }

    public GameObject PointHero
    {
        get { return pointHero; }
    }

    public void CreateHeroUnit(Hero hero)
    {
        var temporary = Instantiate(this.heroUnit, pointHero.transform);
        temporary.transform.localPosition = new Vector3(0, 0);
        HeroUnit heroUnit = temporary.GetComponent<HeroUnit>();
        heroUnit.Hero = hero;
        heroUnit.SetHeroUnit(hero,hero.Level);
    }

    public void DetroyHeroUnit()
    {
        Destroy(pointHero.transform.GetChild(0).gameObject);
    }
    private void Start()
    {
        btnBtnHero.onClick.AddListener(ClickButton);
    }

    public void ClickButton()
    {
        _heroCount = pointHero.transform.childCount;
        uilistHero.SetActive(true);
        ListHeros.Instance.BtnHero = this;
        ListHeros.Instance.heroes = ListAllHero.Instance.Heros;
        FireBaseAuthentication.Instance.LoadItemOfHero();
        ListHeros.Instance.SetListHero();
    }
}

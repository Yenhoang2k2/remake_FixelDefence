using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;
using UnityEngine.UI;

enum State
{
    Busy,Battle
}
public class GameController : MonoBehaviour
{
    public static GameController Instance;
    UiStateEndBallte uistate;
    [SerializeField] private List<GameObject> pointHeros;
    [SerializeField] private SpamEnemy spamEnemy;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private Button statrBattle;
    [SerializeField] private TowerUnit towerUnit;
    [SerializeField] private GameObject gojShop;
    [SerializeField] private Soldiers soldiers;
    [SerializeField] private GameObject _uiQuanDoi;
    private State _state;

    private void Start()
    {
        uistate = UiStateEndBallte.Instance;
        Instance = this;
        _state = State.Busy;
    }

    public void SetUpGameController()
    {
        towerUnit.SetUpTowerUnit();
        spamEnemy.SetUpPointSpawn();
        statrBattle.onClick.AddListener(BattleBegin);
    }

    public void SetWaittingSkillWhenEndBattke()
    {
        foreach (var hero in pointHeros)
        {
            if(hero.transform.childCount > 0)
            {
                HeroUnit heroUnit = hero.GetComponentInChildren<HeroUnit>();
                heroUnit.WaitingSkill = 0;
                heroUnit.SlideSkillOfHero.UpdateHp(0,heroUnit.Hero.HeroBase.Skills[0].Cooldown);
            }
        }
    }
    private void Update()
    {
        if (_state == State.Battle)
        {
            soldiers.UpdateSoldiersAttack();
            foreach (var hero in pointHeros)
            {
                if(hero.transform.childCount > 0)
                {
                    HeroUnit heroUnit = hero.GetComponentInChildren<HeroUnit>();
                    heroUnit.UpdateHeroUnit();
                }
            }
            if(spamEnemy.TimeBatlleCurent >= 0)
                StartCoroutine(spamEnemy.SpawnEnemy());
            if (spamEnemy.TimeBatlleCurent <= 0)
            {
                GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
                if (enemys.Length <= 0)
                {
                    BattleEnd();
                    SpamEnemy pSpamEnemy = SpamEnemy.Instance;
                    int levelBattleNext = pSpamEnemy.LevelBattle + 1;
                    FireBaseAuthentication fireBaseAuthentication = FireBaseAuthentication.Instance;
                    fireBaseAuthentication.SaveLevelWave(levelBattleNext);
                    fireBaseAuthentication.LoadLevelWave((() =>
                    {
                        SpamEnemy.Instance.SetUpPointSpawn();
                    }));
                    uistate.SetStateText("Victory");
                }
            }
            if (towerUnit.HpCurrent <= 0)
            {
                BattleEnd();
                uistate.SetStateText("Defeat");
            }
        }
        else if (_state == State.Busy)
        {
            // Busy
        }
    }

    void BattleEnd()
    {
        _state = State.Busy;
        _uiQuanDoi.SetActive(true);
        uiManager.UiBusy();
        towerUnit.SetUpTowerUnit();
        spamEnemy.SetUpPointSpawn();
        var Teporary = GameObject.FindGameObjectsWithTag("Enemy");
        FireBaseAuthentication.Instance.FirebaseUpdateWhenEndBattle();
        foreach (var enemy in Teporary)
        {
            Destroy(enemy);
        }
        StartCoroutine(UiStateEndBallte.Instance.AnimationEndBattle());
        gojShop.SetActive(true);
        SetWaittingSkillWhenEndBattke();
    }

    void BattleBegin()
    {
        _state = State.Battle;
        spamEnemy.SetUpPointSpawn();
        uiManager.UiBattle();
        gojShop.SetActive(false);
    }
    
}

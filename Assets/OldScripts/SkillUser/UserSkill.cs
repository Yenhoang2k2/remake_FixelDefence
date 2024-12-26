using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;


public class UserSkill : MonoBehaviour
{
    public static UserSkill Instance;
    [SerializeField] private int expIncreacePerLevel;
    [SerializeField] private int goldIncreacePerLevel;
    [SerializeField] private int ratioGoldIncreacePerLevel;
    [SerializeField] private int attackSoldiersIncreacePerLevel;
    [SerializeField] private int ratioExpIncreacePerLevel;
    
    public int Level { get; set; }
    public Dictionary<string, object> Data;
    public int LevelOfExp { get; set; }
    public int LevelOfGold { get; set; }
    public int LevelOfRatioGold { get; set; }
    public int LevelOfAttackSoldiers { get; set; }
    public int LevelOfRatioExp { get; set; }
    private void Start()
    {
        SetDataDictionary();
        Instance = this;
    }
    public int Exp
    {
        get { return (LevelOfExp * expIncreacePerLevel); }
    }
    public int RatioExp
    {
        get { return LevelOfRatioExp * ratioExpIncreacePerLevel; }
    }
    public int AttackSoldiers
    {
        get { return (LevelOfAttackSoldiers * attackSoldiersIncreacePerLevel); }
    }
    public int Gold
    {
        get { return  (LevelOfGold * goldIncreacePerLevel); }
    }
    public int RatioGold
    {
        get { return  (LevelOfRatioGold * ratioGoldIncreacePerLevel); }
    }

    public void SetDataDictionary()
    {
        Data = new Dictionary<string, object>
        {
            {"exp",0},
            {"ratioExp",0},
            {"gold",0},
            {"ratioGold",0},
            {"attackSoldiers",0}
        };
    }

    public void UpDataInUserSkill()
    {
        foreach (var data in Data)
        {
            if (data.Key == "exp")
            {
                LevelOfExp = Convert.ToInt32(data.Value);
            }
            if (data.Key == "ratioExp")
            {
                LevelOfRatioExp = Convert.ToInt32(data.Value);
            }
            if (data.Key == "gold")
            {
                LevelOfGold = Convert.ToInt32(data.Value);
            }
            if (data.Key == "ratioGold")
            {
                LevelOfRatioGold = Convert.ToInt32(data.Value);
            }
            if (data.Key == "attackSoldiers")
            {
                LevelOfAttackSoldiers = Convert.ToInt32(data.Value);
            }
        }
    }

    public void CheckClickbtnUserSkillAndUpLevel(string name)
    {
        
    }

    public void UpdateUserSkillWhenUserUplevel(int level)
    {
        Level = level;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class Userform : MonoBehaviour
{
    public static Userform Instance;
    [SerializeField] Text textUserName;
    [SerializeField] private UserSkill _userSkill;
    [SerializeField] Text textLevel;
    [SerializeField] private int expIncreacePerLevel;
    private string _userName;
    private int _level;
    private int _expCurrent;

    [SerializeField] private ExpBar expBar;

    private void Start()
    {
        _userSkill.UpdateUserSkillWhenUserUplevel(Level);
        Instance = this;
    }

    public int Level
    {
        get { return _level; }
    }

    public int ExpCurrent
    {
        get { return _expCurrent; }
    }
    public int MaxExp
    {
        get { return (_level * expIncreacePerLevel); }
    }

    public string UserName
    {
        get { return _userName; }
    }

    public void AddExp(int exp)
    {
        _expCurrent += exp;
        UpLevel();
        textLevel.text = _level.ToString();
        expBar.SetExp(this._expCurrent,MaxExp);
    }

    public void UpLevel()
    {
        for (;_expCurrent >= MaxExp;)
        {
            _level += 1;
            _expCurrent -= MaxExp;
            if (_expCurrent <= 0) _expCurrent = 1;
            _userSkill.UpdateUserSkillWhenUserUplevel(Level);
        }
    }
    public void SetUpUserForm(int level, int expCurrent, string userName)
    {
        this._userName = userName;
        textUserName.text = userName;
        textLevel.text = level.ToString();
        this._expCurrent = expCurrent;
        this._level = level;
        if (this._expCurrent <= 0) this._expCurrent = 1;
        expBar.SetExp(this._expCurrent,MaxExp);
    }
}

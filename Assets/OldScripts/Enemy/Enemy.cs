using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Enemy
{
    [SerializeField] private EnemyBase enemyBase;
    public int Level;

    public Enemy(EnemyBase enemyBase, int level)
    {
        this.enemyBase = enemyBase;
        this.Level = level;
    }

    public EnemyBase EnemyBase
    {
        get { return enemyBase; }
    }
    public float Attack
    {
        get { return enemyBase.Attack + (Level * enemyBase.DameIncreacePerWave); }
    }
    public int MaxHp
    {
        get { return (int)(enemyBase.Hp + (Level * enemyBase.HpIncreacePerWave)); }
    }

    public int Exp
    {
        get { return (int)(enemyBase.ExpOfenemy + (Level * enemyBase.ExpIncreacePerWave)); }
    }

    public int Price
    {
        get { return (int)(enemyBase.PriceOfEnemy + (this.Level * enemyBase.PriceIncreacePerWave)); }
    }
}

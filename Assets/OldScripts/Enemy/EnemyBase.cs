using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Enemy",fileName = "Enemy/Create new enemy")]
public class EnemyBase : ScriptableObject
{
    [SerializeField] private string newName;
    [TextArea]
    [SerializeField] private string description;
    [SerializeField] private Sprite sprite;
    [SerializeField] private float hp;
    [SerializeField] private float attack;
    [SerializeField] private float speed;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float dameIncreacePerWave;
    [SerializeField] private float hpIncreacePerWave;
    [SerializeField] private int expIncreacePerWave;
    [SerializeField] private int priceincreacePerWave;
    [SerializeField] private int exp;
    [SerializeField] private int price;

    public int PriceIncreacePerWave
    {
        get { return priceincreacePerWave; }
    }
    public int ExpIncreacePerWave
    {
        get { return expIncreacePerWave; }
    }
    public float HpIncreacePerWave
    {
        get { return hpIncreacePerWave; }
    }
    public float DameIncreacePerWave
    {
        get { return dameIncreacePerWave; }
    }
    public string NewName
    {
        get { return newName; }
    }
    public string Description
    {
        get { return description; }
    }
    public Sprite Sprite
    {
        get { return sprite; }
    }
    public float Hp
    {
        get { return hp; }
    }
    public float Attack
    {
        get { return attack; }
    }
    public float Speed
    {
        get { return speed; }
    }
    public float AttackSpeed
    {
        get { return attackSpeed; }
    }

    public int ExpOfenemy
    {
        get { return exp; }
    }

    public int PriceOfEnemy
    {
        get { return price; }
    }
}

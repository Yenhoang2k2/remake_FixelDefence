using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "HeroBase",menuName = "Hero/Create new hero")]
public class HeroBase : ScriptableObject
{
    [SerializeField] private Sprite spriteBullet;
    [SerializeField] private string newName;
    [TextArea]
    [SerializeField] private string desciption;
    [SerializeField] private Sprite sprite;
    [SerializeField] private int price;
    [SerializeField] private float attack;
    [SerializeField] private float atkSpeed;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private int atkRange;
    [SerializeField] private List<SkillBase> skills;
    [SerializeField] private float atkIncreacePerLevel;
    [SerializeField] private int priceIncreacePerLevel;

    public string NewName
    {
        get { return newName; }
    }
    public List<SkillBase> Skills
    {
        get { return skills; }
    }
    public string Description
    {
        get { return desciption; }
    }
    public int Price
    {
        get { return price; }
    }
    public float Attack
    {
        get { return attack; }
    }
    public float AtkSpeed
    {
        get { return atkSpeed; }
    }
    public float AtkIncreacePerLevel
    {
        get { return atkIncreacePerLevel; }
    }
    public int PriceIncreacePerLevel
    {
        get { return priceIncreacePerLevel; }
    }
    public int AtkRange
    {
        get { return atkRange; }
    }

    public Sprite Sprite
    {
        get { return sprite; }
    }

    public float BulletSpeed
    {
        get { return bulletSpeed; }
    }

    public Sprite SpriteBullet
    {
        get { return spriteBullet; }
    }
}

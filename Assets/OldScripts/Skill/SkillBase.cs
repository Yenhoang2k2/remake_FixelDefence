using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillBase",menuName = "Create/NewSkillbase")]
public class SkillBase : ScriptableObject
{
    [SerializeField] private stateSkill stateskill;
    [TextArea]
    [SerializeField] private string description;
    [SerializeField] private string nameSkill;
    [SerializeField] private float dameSkill;
    [SerializeField] private int coolDown;
    [SerializeField] private int quantity;
    [SerializeField] private int timeOfQuantity;
    [SerializeField] private Vector3 scaleBullet;
    [SerializeField] private Sprite sprite;
    [SerializeField] private int buffattackSpeed;
    [SerializeField] private int buffAttack;
    [SerializeField] private int applyTime;

    public virtual void Activate(HeroUnit hero)
    {
        GameObject temporary = Instantiate(hero.BulletUnit, hero.PointShoot);
        temporary.GetComponent<SpriteRenderer>().sprite = sprite;
        temporary.transform.localScale = scaleBullet;
        var bullet = temporary.GetComponent<Bullet>();
        var varBullet = hero.Hero.HeroBase.SpriteBullet;
        bullet.rangeHero = hero.Hero.HeroBase.AtkRange;
        bullet.DameBullet = (float)(hero.Hero.AttackApply() * dameSkill);
        bullet.BulletSpeed = (hero.Hero.HeroBase.BulletSpeed);
    }

    public stateSkill StateSkill
    {
        get { return stateskill; }
    }
    public int BuffAttackSpeed
    {
        get { return buffattackSpeed; }
    }
    public int BuffAttack
    {
        get { return buffAttack; }
    }
    public int ApplyTime
    {
        get { return applyTime; }
    }
    public int Cooldown
    {
        get { return coolDown; }
    }

    public int Quantity
    {
        get { return quantity; }
    }

    public int TimeOfQuantity
    {
        get { return timeOfQuantity; }
    }

    public string NameSkill
    {
        get { return nameSkill; }
    }

    public string Description
    {
        get { return description; }
    }
}

public enum stateSkill
{
    buff,
    raise,
    aoe
}

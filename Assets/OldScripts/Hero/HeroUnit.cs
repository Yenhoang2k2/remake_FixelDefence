using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class HeroUnit : MonoBehaviour
{
    [SerializeField] private Transform pointShoot;
    [SerializeField] private GameObject bulletUnit;
    [SerializeField] private Slider slideSkillOfHero;
    private Collider2D[] _enemys;
    private SpriteRenderer _image;
    private float _gunWaitingTime;
    public float WaitingSkill { get; set; }
    private float _countTimeAplly;
    private float _buffAttack;
    private float _buffAtkSpeed;
    private float _attackSpeed;
    private bool _isBuff = false;
    private List<GameObject> _enemyUnits;
    public Hero Hero { get; set; }
    public int Level { get; set; }

    public Slider SlideSkillOfHero
    {
        get { return slideSkillOfHero; }
    }

    public GameObject BulletUnit
    {
        get { return bulletUnit; }
    }

    public Transform PointShoot
    {
        get { return pointShoot; }
    }

    public void SetHeroUnit(Hero heroBase,int level)
    {
        _enemyUnits = new List<GameObject>();
        Hero = heroBase;
        Level = level;
        _buffAttack = 1;
        _buffAtkSpeed = 1;
        _attackSpeed = Hero.AtkSpeedApply();
        _image = GetComponent<SpriteRenderer>();
        _image.sprite = Hero.HeroBase.Sprite;
    }
    
    public void UpdateHeroUnit()
    {
        _gunWaitingTime += Time.deltaTime;
        WaitingSkill += Time.deltaTime;
        slideSkillOfHero.UpdateHp(WaitingSkill,Hero.HeroBase.Skills[0].Cooldown);
        CheckTimeShoot();
        CheckTimeSkill();
    }

    public void CheckTimeSkill()
    {
        if (WaitingSkill > Hero.HeroBase.Skills[0].Cooldown) WaitingSkill = Hero.HeroBase.Skills[0].Cooldown;
        if (WaitingSkill >= Hero.HeroBase.Skills[0].Cooldown)
        {
            FindAllEnemyInRange(Hero.HeroBase.AtkRange);
            TypeSkill();
        }
        if (_isBuff == true)
        {
            _countTimeAplly += Time.deltaTime;
            if (_countTimeAplly >= Hero.HeroBase.Skills[0].ApplyTime)
            {
                _attackSpeed = _attackSpeed * Hero.HeroBase.Skills[0].BuffAttackSpeed;
                _isBuff = false;
            }
        }
    }
    public void CheckTimeShoot()
    {
        if (_gunWaitingTime > _attackSpeed)
        {
            FindAllEnemyInRange(Hero.HeroBase.AtkRange);
            Shoot();
        }
    }

    public void ChangeBuff(float buffAttack, float buffAttackSpeed)
    {
        this._buffAttack = buffAttack;
        _attackSpeed = _attackSpeed / buffAttackSpeed;
    }

    public void FindAllEnemyInRange(int range)
    {
        _enemys = Physics2D.OverlapCircleAll(transform.position, range);
        if (_enemys.Length > 0)
        {
            for (int i = 0; i < _enemys.Length; i++)
            {
                if (_enemys[i].gameObject.CompareTag("Enemy"))
                {
                    _enemyUnits.Add(_enemys[i].gameObject);
                }
            }
        }
    }
    public void Shoot()
    {
        if (_enemyUnits.Count > 0)
        {
            CreateBullet();
        }
        _enemyUnits.Clear();
    }
    public void TypeSkill()
    {
        if ( _enemyUnits.Count > 0 && WaitingSkill >= Hero.HeroBase.Skills[0].Cooldown && Hero.HeroBase.Skills.Count > 0)
        {
            if (Hero.HeroBase.Skills[0].StateSkill == stateSkill.buff)
            {
                UseBuffSkill();
            }
            else if(Hero.HeroBase.Skills[0].StateSkill == stateSkill.raise)
            {
                UseSkill();
            }
            else if(Hero.HeroBase.Skills[0].StateSkill == stateSkill.aoe)
            {
                UseSkillAoe();
            }
            WaitingSkill = 0;
        }
        _enemyUnits.Clear();
    }
    public void UseSkill()
    {
        Hero.HeroBase.Skills[0].Activate(this);
    }

    public void UseBuffSkill()
    {
        ChangeBuff(Hero.HeroBase.Skills[0].BuffAttack,Hero.HeroBase.Skills[0].BuffAttackSpeed);
        _countTimeAplly = 0;
        _isBuff = true;
    }

    public void UseSkillAoe()
    {
        for (int i = 0; i < Hero.HeroBase.Skills[0].Quantity; i++)
        {
            CreateBullet();   
        }
    }
    public void CreateBullet()
    {
        GameObject temporary = Instantiate(bulletUnit, pointShoot);
        SpriteRenderer spriteRenderer = temporary.GetComponent<SpriteRenderer>();
        var bullet = temporary.GetComponent<Bullet>();
        var varBullet = Hero.HeroBase.SpriteBullet;
        bullet.rangeHero = Hero.HeroBase.AtkRange;
        spriteRenderer.sprite = varBullet;
        bullet.DameBullet = Hero.AttackApply() * _buffAttack;
        bullet.BulletSpeed = Hero.HeroBase.BulletSpeed;
        _gunWaitingTime = 0;
    }
}

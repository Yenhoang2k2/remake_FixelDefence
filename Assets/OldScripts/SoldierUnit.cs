using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SoldierUnit : MonoBehaviour
{
    [SerializeField] private Transform pointShoot;
    [SerializeField] private GameObject bulletUnit;
    private float _attack;
    private float _atkIncreacePerLevel;
    private int _atkRange;
    private float _atkSpeed;
    private float _speedBullet;
    private Collider2D[] _enemys;
    private SpriteRenderer _spriteSoldier;
    private Sprite _spriteBullet;
    private float _gunWaitingTime;
    private List<GameObject> _enemyUnits;

    private void Start()
    {
        _enemyUnits = new List<GameObject>();
        _spriteSoldier = GetComponent<SpriteRenderer>();
    }

    public int Level { get; set; }
    

    public GameObject BulletUnit
    {
        get { return bulletUnit; }
    }

    public Transform PointShoot
    {
        get { return pointShoot; }
    }

    public float Attack
    {
        get { return _attack + (Level * _atkIncreacePerLevel); }
    }

    public void SetSoldiers(int level,int atkRange, float attack,float speedBullet, float atkSpeed,Sprite spSoldier,Sprite spBullet)
    {
        _spriteSoldier = GetComponent<SpriteRenderer>();
        _spriteBullet = spBullet;
        _spriteSoldier.sprite = spSoldier;
        Level = level;
        _speedBullet = speedBullet;
        _atkRange = atkRange;
        _attack = attack;
        _atkSpeed = atkSpeed;
    }
    
    public void UpdateSoldierAttack()
    {
        _gunWaitingTime += Time.deltaTime;
        CheckTimeShoot();
    }
    
    public void CheckTimeShoot()
    {
        if (_gunWaitingTime > _atkSpeed)
        {
            FindAllEnemyInRange(_atkRange);
            Shoot();
        }
    }

    public void ChangeBuffSoldier(float buffAttack, float buffAttackSpeed)
    {
        _attack = _attack * buffAttack;
        _atkSpeed = _atkSpeed / buffAttackSpeed;
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
    public void CreateBullet()
    {
        GameObject temporary = Instantiate(bulletUnit, pointShoot);
        SpriteRenderer spriteRenderer = temporary.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = _spriteBullet;
        var bullet = temporary.GetComponent<Bullet>();
        bullet.rangeHero = _atkRange;
        bullet.DameBullet = Attack;
        bullet.BulletSpeed = _speedBullet;
        _gunWaitingTime = 0;
    }
}

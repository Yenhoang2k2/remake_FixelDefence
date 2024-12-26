using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Debug = UnityEngine.Debug;

public class EnemyUnit : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject gojTextGold;
    
    private TowerUnit _towerUnit;
    private bool _isInRange;
    private bool _isMoving;
    private float _hpCurrent;
    private Transform _enemyTransForm;

    public Enemy Enemy { get; set; }

    private void Awake()
    {
        _enemyTransForm = transform;
    }

    private void Update()
    {
        StartCoroutine(EnemyAttack());
        EnemyMove();
    }

    public IEnumerator EnemyAttack()
    {
        while (_isInRange)
        {
            _towerUnit.TakeDame(Enemy.Attack);
            yield return new WaitForSeconds(Enemy.EnemyBase.AttackSpeed);
        }
    }
    public void SetupEnemy()
    {
        _isMoving = true;
        _hpCurrent = Enemy.MaxHp;
        slider.SetHp();
    }
    public void EnemyMove()
    {
        if (_isMoving)
        {
            Vector3 move = _enemyTransForm.position;
            move.x -= Time.deltaTime * Enemy.EnemyBase.Speed;
            _enemyTransForm.position = move;
        }
        
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Tower"))
        {
            _isMoving = false;
            _towerUnit = other.GetComponent<TowerUnit>();
            if(_towerUnit != null) _isInRange = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Tower"))
        {
            _isInRange = false;
        }
    }

    public void TakeDame(float dame)
    {
        _hpCurrent -= dame;
        if (_hpCurrent <= 0)
        {
            _hpCurrent = 0;
            UpdateResourceAndExpUser();
            AnimationAddGold();
            EnemyDie();
            return;
        }
        slider.UpdateHp(_hpCurrent,Enemy.MaxHp);
    }

    public void EnemyDie()
    {
        Destroy(gameObject);
    }

    public void UpdateResourceAndExpUser()
    {
        Userform.Instance.AddExp(Enemy.Exp);
        ResourcesHub.Instance.AddMonney(Enemy.Price);
    }
    
    public void AnimationAddGold()
    {
        var popup = Instantiate(gojTextGold,transform.position,_enemyTransForm.rotation);
        TxtGold txtGold = popup.GetComponent<TxtGold>();
        txtGold.TextMesh.text = Enemy.Price.ToString();
    }
}

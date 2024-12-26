using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Sprite sprite;
    [SerializeField] private int timeX;
    private List<TagetvsRange> tagets;
    private Collider2D[] _enemys;
    private GameObject taget;
    private Vector3 tagetPos;
    private float timeBegin;
    private bool _thereTaget;
    public List<GameObject> enemyUnits;
    public float rangeHero;
    
    public float DameBullet { get; set; }
    public float BulletSpeed { get; set; }
    
    private void Start()
    {
        tagets = new List<TagetvsRange>();
        FindEnemyNearest();
    }

    private void Update()
    {
        BulletMove();
        AutoDetroy();
        DetroyBullet();
    }
    
    public void BulletMove()
    {
        if (taget)
        {
            transform.position =
                Vector3.MoveTowards(transform.position, taget.transform.position, BulletSpeed * Time.deltaTime);
            tagetPos = taget.transform.position;
        }
        else
        {
            transform.position =
                Vector3.MoveTowards(transform.position, tagetPos, BulletSpeed * Time.deltaTime);
        }
    }
    public void FindEnemyNearest()
    {
        _enemys = Physics2D.OverlapCircleAll(transform.position, rangeHero);
        if (_enemys.Length > 0)
        {
            for (int i=0;i<_enemys.Length;i++)
            {
                if (_enemys[i].gameObject.CompareTag("Enemy"))
                {
                    enemyUnits.Add(_enemys[i].gameObject);
                }
            }
        }
        if (enemyUnits != null)
        {
            foreach (var enemyUnit in enemyUnits)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemyUnit.transform.position);
                TagetvsRange tagetvsRange = new TagetvsRange(enemyUnit, distanceToEnemy);
                tagets.Add(tagetvsRange);
            }
            // hàm sắp xếp có sẵn
            if (tagets != null && tagets.Count > 0)
            {
                tagets.Sort((a, b) => a.DistanceToEnemy.CompareTo(b.DistanceToEnemy));
                int ran = tagets.Count >= 3 ? Random.Range(0, 3) : 0;
                
                taget = tagets[ran].Taget;
                tagetPos = taget.transform.position;
            }
        }
    }
    public void DetroyBullet()
    {
        if (Vector3.Distance(transform.position, tagetPos) <= 0.1f)
        {
            if (taget != null)
            {
                taget.GetComponent<EnemyUnit>().TakeDame(DameBullet);
                Destroy(gameObject);
            }
        }
    }

    public void AutoDetroy()
    {
        timeBegin += Time.deltaTime;
        if (timeBegin >= timeX)
        {
            Destroy(gameObject);
        }
    }
    public Sprite Sprite
    {
        get { return sprite; }
    }
}

class TagetvsRange
{
    public GameObject Taget { get; set; }
    public float DistanceToEnemy { get; set; }

    public TagetvsRange(GameObject taget,float distanceToEnemy)
    {
        Taget = taget;
        DistanceToEnemy = distanceToEnemy;
    }
    
}

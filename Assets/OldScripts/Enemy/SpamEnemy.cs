using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SpamEnemy : MonoBehaviour
{
    public static SpamEnemy Instance;
    [SerializeField] private GameObject gojEnemyUnit;
    [SerializeField] private List<EnemyBase> enemys;
    [SerializeField] private List<GameObject> spawnPoints;
    [SerializeField] private SliderWave sliderWave;
    [SerializeField] private float timeWaveBase;
    [SerializeField] private float timeIncreacePerWave;
    [SerializeField] float timeSpawnPerSeconds;
    public float TimeBatlleCurent { get; set; }
    public int LevelBattle { get; set; }
    private int _enemyOder;
    private float _timeCanSpawnCurrent;

    private void Start()
    {
        Instance = this;
    }
    
    public void SetUpPointSpawn()
    {
        _timeCanSpawnCurrent = TimeBatlleWithWave;
        TimeBatlleCurent = TimeBatlleWithWave;
        SetSliderWave();
    }
    public float TimeBatlleWithWave
    {
        get { return timeWaveBase +(LevelBattle*timeIncreacePerWave); }
    }
    
    public IEnumerator SpawnEnemy()
    {
        TimeBatlleCurent -= Time.deltaTime;
        SetSliderWave();
        if (TimeBatlleCurent > 0)
        {
            if (_timeCanSpawnCurrent >= TimeBatlleCurent)
            {
                _enemyOder++;
                _timeCanSpawnCurrent = TimeBatlleCurent - timeSpawnPerSeconds;
                RecreateEnemyLoop();
                yield return CreateEnemysAndAddValue();
            }
            // tăng lên để spawn quái khác trong danh sách
        }
    }
    public IEnumerator CreateEnemysAndAddValue()
    {
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            var temporary = Instantiate(gojEnemyUnit, spawnPoints[i].transform);
            EnemyUnit enemyUnitCurrent = temporary.GetComponent<EnemyUnit>();
            SpriteRenderer spriteRenderer = temporary.GetComponent<SpriteRenderer>();
            Enemy enemy = new Enemy(enemys[_enemyOder], LevelBattle);
            enemyUnitCurrent.Enemy = enemy;
            enemyUnitCurrent.SetupEnemy();
            spriteRenderer.sprite = enemys[_enemyOder].Sprite;
        }
        yield return null;
    }

    public void SetSliderWave()
    {
        sliderWave.SetSlider(TimeBatlleCurent/TimeBatlleWithWave,LevelBattle);
    }
    public void RecreateEnemyLoop()
    {
        if (_enemyOder >= enemys.Count)
            _enemyOder = 0;
    }
}

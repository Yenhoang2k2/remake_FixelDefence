using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListAllHero : MonoBehaviour
{
    public static ListAllHero Instance;
    [SerializeField] private List<Hero> _heroes;

    private void Start()
    {
        Instance = this;
    }

    public List<Hero> Heros
    {
        get { return _heroes; }
    }
}

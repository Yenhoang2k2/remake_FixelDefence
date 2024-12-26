using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListItemDiamond : MonoBehaviour
{
    public static ListItemDiamond Instance;
    [SerializeField] private List<ItemBase> _list;

    private void Start()
    {
        Instance = this;
    }

    public List<ItemBase> List
    {
        get { return _list; }
    }
}

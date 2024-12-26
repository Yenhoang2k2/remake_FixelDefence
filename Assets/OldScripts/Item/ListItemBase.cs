using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListItemBase : MonoBehaviour
{
    public static ListItemBase Instance;
    [SerializeField] private List<ItemBase> _itemBases;

    private void Start()
    {
        Instance = this;
    }

    public List<ItemBase> ItemBases
    {
        get { return _itemBases; }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class ListItem : MonoBehaviour
{
    public static ListItem Instante;
    public List<Item> Items;

    private void Start()
    {
        Instante = this;
    }
    
}

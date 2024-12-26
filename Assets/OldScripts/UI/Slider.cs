using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slider : MonoBehaviour
{
    [SerializeField] private GameObject health;
    
    public void SetHp()
    {
        health.transform.localScale = new Vector3(0.5f, 0.05f);
        health.transform.localPosition = new Vector3(0, 0.4f);
        
    }

    public void UpdateHp(float hpCurrent,float maxHp)
    {
        health.transform.localScale = new Vector3((hpCurrent/ maxHp*0.5f ),0.05f);
        health.transform.localPosition = new Vector3(0+(0.5f -hpCurrent/ maxHp*0.5f)/2, 0.4f);
    }
}

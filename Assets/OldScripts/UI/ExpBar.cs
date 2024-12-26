using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    [SerializeField] private Image exp;

    public void SetExp(float expCurrent, float maxHp)
    {
        exp.fillAmount = expCurrent / maxHp;
    }
}

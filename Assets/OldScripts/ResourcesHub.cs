using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesHub : MonoBehaviour
{
    public static ResourcesHub Instance;
    [SerializeField] Text txtDiamond;
    [SerializeField] Text txtMonney;
    private int _diamond;
    private int _monney;

    private void Start()
    {
        Instance = this;
    }

    public int Diamond
    {
        get { return _diamond; }
    }

    public int Monney
    {
        get { return _monney; }
    }

    public void SetResources(int diamond, int monney)
    {
        this._diamond = diamond;
        this._monney = monney;
        txtDiamond.text = diamond.ToString();
        txtMonney.text = monney.ToString();
    }

    public void AddDiamond(int diamond)
    {
        this._diamond += diamond;
        txtDiamond.text = this._diamond.ToString();
    }

    public void AddMonney(int monney)
    {
        this._monney += monney;
        txtMonney.text = this._monney.ToString();
    }
}

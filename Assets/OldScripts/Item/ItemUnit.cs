using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUnit : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] private Text txtLevel;
    [SerializeField] private Button btnItem;

    private void Awake()
    {
        
    }
    public Hero Hero { get; set; }
    public Item Item { get; set; }
    public int stt;

    public Button BtnItem
    {
        get { return btnItem; }
    }

    public Image Image
    {
        get { return image; }
    }

    public Text TxtLevel
    {
        get { return txtLevel; }
    }
}

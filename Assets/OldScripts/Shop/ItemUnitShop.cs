using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUnitShop : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] private Button btnItem;

    private void Awake()
    {
        
    }
    public ItemBase ItemBase { get; set; }

    public Button BtnItem
    {
        get { return btnItem; }
    }

    public Image Image
    {
        get { return image; }
    }
}

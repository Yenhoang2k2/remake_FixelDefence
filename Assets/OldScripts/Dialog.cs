using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    public static Dialog Instance;
    [SerializeField] private Text dialog;
    [SerializeField] private int speedAddText;
    [SerializeField] private Button btnClose;

    private void Start()
    {
        Instance = this;
        btnClose.onClick.AddListener(CloseDialog);
    }

    public void SetDialog(string dialog)
    {
        this.dialog.text = dialog;
    }

    public IEnumerator SetDialogSmooth(string dialog)
    {
        this.dialog.text = "";
        foreach (var dia in dialog.ToCharArray())
        {
            this.dialog.text += dia;
            yield return new WaitForSeconds((float)1 / speedAddText);
        }
    }

    public void CloseDialog()
    {
        gameObject.SetActive(false);
    }
}

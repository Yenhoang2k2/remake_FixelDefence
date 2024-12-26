using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private GameObject uiAttack;
    [SerializeField] private GameObject uiListHero;
    [SerializeField] private GameObject uiInformation;
    [SerializeField] private GameObject uiQuanDoi;
    [SerializeField] private GameObject uiShop;
    [SerializeField] private GameObject btnHeros;
    [SerializeField] private GameObject uiLogin;
    [SerializeField] private GameObject uiRigister;
    [SerializeField] private GameObject uiForgotPassWord;
    [SerializeField] private GameObject uiDialog;
    [SerializeField] private Button btnRisgisterBack;
    [SerializeField] private Button btnForgetPassWordBack;
    [SerializeField] private Button btnForgetPassWord;
    [SerializeField] private Button btnRigister;
    [SerializeField] private Button btnShop;
    [SerializeField] private ShopItem _shopItem;
    [SerializeField] private Dialog _dialog;


    public void Start()
    {
        Instance = this;
        btnRigister.onClick.AddListener(ClickBtnRigister);
        btnForgetPassWord.onClick.AddListener(ClickBtnForgetPassWord);
        btnRisgisterBack.onClick.AddListener(ClickBtnRigisterBack);
        btnForgetPassWordBack.onClick.AddListener(ClickBtnForgetPassWordBack);
        btnShop.onClick.AddListener(ClickBtnShop);
    }

    public Dialog Dialog
    {
        get { return _dialog; }
    }

    public void OnDialog()
    {
        uiDialog.SetActive(true);
    }
    public void ClickBtnShop()
    {
        uiShop.SetActive(true);
    }
    public void ClickBtnRigister()
    {
        uiLogin.SetActive(false);
        uiRigister.SetActive(true);
        uiForgotPassWord.SetActive(false);
    }
    public void ClickBtnForgetPassWord()
    {
        uiLogin.SetActive(false);
        uiRigister.SetActive(false);
        uiForgotPassWord.SetActive(true);
    }
    public void ClickBtnRigisterBack()
    {
        uiLogin.SetActive(true);
        uiRigister.SetActive(false);
        uiForgotPassWord.SetActive(false);
    }
    public void ClickBtnForgetPassWordBack()
    {
        uiLogin.SetActive(true);
        uiRigister.SetActive(false);
        uiForgotPassWord.SetActive(false);
    }
    public void UiBattle()
    {
        uiAttack.SetActive(false);
        uiQuanDoi.SetActive(false);
        btnHeros.SetActive(false);
    }

    public void UiBusy()
    {
        uiAttack.SetActive(true);
        btnHeros.SetActive(true);
    }
    
}

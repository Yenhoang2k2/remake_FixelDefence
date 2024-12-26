using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UiStateEndBallte : MonoBehaviour
{
    public static UiStateEndBallte Instance;
    [SerializeField] private GameObject rightSword;
    [SerializeField] private GameObject leftSword;
    [SerializeField] private GameObject shile;
    [SerializeField] private GameObject backGround;
    [SerializeField] private Text state;
    private Vector3 originalLeft;
    private Vector3 originalRight;
    private Vector3 originalShile;
    private Vector3 originalBackGround;
    private void Start()
    {
        Instance = this;
        originalLeft = leftSword.transform.position;
        originalRight = rightSword.transform.position;
        originalShile = shile.transform.position;
        originalBackGround = backGround.transform.position;
    }

    public void SetStateText(string state)
    {
        this.state.text = state;
    }
    public IEnumerator AnimationEndBattle()
    {
        backGround.transform.DOLocalMoveY(originalBackGround.y, 0.5f);
        yield return new WaitForSeconds(0.5f);
        var sequenceRight = DOTween.Sequence();
        var sequenceleft = DOTween.Sequence();
        var sequenceShiel = DOTween.Sequence();
        sequenceRight.Append(rightSword.transform.DOLocalMove(new Vector3(originalRight.x +60,originalRight.y + 60), 0.75f));
        sequenceRight.Append(rightSword.transform.DOLocalMove(originalRight, 0.25f));
        sequenceleft.Append(leftSword.transform.DOLocalMove(new Vector3(originalLeft.x -60,originalLeft.y + 60), 0.75f));
        sequenceleft.Append(leftSword.transform.DOLocalMove(originalLeft, 0.25f));
        sequenceShiel.Append(shile.transform.DOLocalMoveY(originalShile.y + 10, 0.75f));
        sequenceShiel.Append(shile.transform.DOLocalMoveY(originalShile.y, 0.25f));
        yield return new WaitForSeconds(2f);
        yield return null;
        backGround.transform.DOLocalMoveY(originalBackGround.y - 300, 0.5f);
        yield return new WaitForSeconds(0.5f);
    }
}

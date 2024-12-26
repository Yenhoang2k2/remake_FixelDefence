using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TxtGold : MonoBehaviour
{
    [SerializeField,Min(0.1f)] float timeDetroy;
    private float _timeCurrent;
    private TextMesh _textMesh;

    private void Awake()
    {
        _textMesh= GetComponent<TextMesh>();
        if(_textMesh == null) Debug.Log("Không lấy được thành phần TextMesh trong TxtGold");
    }

    private void Start()
    {
        Destroy(gameObject,timeDetroy);
    }

    private void Update()
    {
        AnimationText();
    }
    public TextMesh TextMesh
    {
        get { return _textMesh; }
    }
    public void AnimationText()
    {
        // di chuyển chữ lên
        transform.position += Vector3.up * Time.deltaTime;
    }
}

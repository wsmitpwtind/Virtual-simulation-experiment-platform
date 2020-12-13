using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TipsController : MonoBehaviour
{
    private bool shown = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowTips(string message)
    {
        if (!shown)
            HideTips();
        gameObject.GetComponentInChildren<Text>(true).text = message;
        var animate = gameObject.GetComponent<RectTransform>().DOLocalMoveX(300, .5f);
        animate.SetEase(Ease.OutExpo);
        shown = true;
    }

    public void HideTips()
    {
        var animate = gameObject.GetComponent<RectTransform>().DOLocalMoveX(500, .5f);
        animate.SetEase(Ease.OutExpo);
        shown = false;
    }
}


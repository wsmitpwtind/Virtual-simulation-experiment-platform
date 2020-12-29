using System;
using System.Dynamic;
using System.Reflection;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TipsScript : MonoBehaviour
{
    private bool shown = false;
    private string messageToShow = "";
    public void ShowTips(string message)
    {
        if (shown)
            HideTips();
        gameObject.GetComponentInChildren<Text>(true).text = message;
        var animate = gameObject.GetComponent<RectTransform>().DOLocalMoveX(270, .5f);
        animate.SetEase(Ease.OutExpo);
        shown = true;
    }

    public void HideTips()
    {
        var animate = gameObject.GetComponent<RectTransform>().DOLocalMoveX(530, .5f);
        animate.SetEase(Ease.OutExpo);
        shown = false;
    }
}

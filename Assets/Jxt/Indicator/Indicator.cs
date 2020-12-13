using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// 右上角按键提示框处理程序
/// </summary>
public class Indicator : MonoBehaviour
{
    private Canvas IndicatorCanvas = null;
    private Text IndicateText = null;
    private Text KeyText = null;

    private bool shown = false;

    /// <summary>
    /// 按键文本。仅能显示一个字符。
    /// </summary>
    public string keyText
    {
        get => KeyText.text;
        set => KeyText.text = value;
    }

    /// <summary>
    /// 提示说明文本。最多显示6个汉字。
    /// </summary>
    public string indicateText
    {
        get => IndicateText.text;
        set => IndicateText.text = value;
    }


    // Start is called before the first frame update
    void Start()
    {
        // 获取全局组件
        IndicatorCanvas = GetComponent<Canvas>();
        var texts = GetComponentsInChildren<Text>(true);
        foreach (var item in texts)
        {
            if ("KeyText".Equals(item.gameObject.name))
                KeyText = item;
            if ("IndicateText".Equals(item.gameObject.name))
                IndicateText = item;
        }
    }

    /// <summary>
    /// 显示按键提示框。
    /// </summary>
    /// <param name="Key">按键文本</param>
    /// <param name="Message">提示说明文本</param>
    public void ShowIndicate(string Key, string Message)
    {
        if (shown)
            HideIndicate();
        IndicateText.text = Message;
        KeyText.text = Key;
        var animate = IndicatorCanvas.gameObject.GetComponent<RectTransform>().DOLocalMoveX(300, .5f);
        animate.SetEase(Ease.OutExpo);
        shown = true;
        // IndicatorCanvas.gameObject.SetActive(true);
    }

    /// <summary>
    /// 隐藏按键提示框。
    /// </summary>
    public void HideIndicate()
    {
        var animate = IndicatorCanvas.gameObject.GetComponent<RectTransform>().DOLocalMoveX(500, .5f);
        animate.SetEase(Ease.OutExpo);
        shown = false;
        // IndicatorCanvas.gameObject.SetActive(false);
    }
}

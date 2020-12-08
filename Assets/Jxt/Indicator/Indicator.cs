using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Indicator : MonoBehaviour
{
    Canvas IndicatorCanvas = null;

    Text IndicateText = null;

    Text KeyText = null;

    public string keyText
    {
        get => KeyText.text;
    }

    public string indicateText
    {
        get => IndicateText.text;
    }


    // Start is called before the first frame update
    void Start()
    {
        IndicatorCanvas = GetComponent<Canvas>();
        var texts = GetComponentsInChildren<Text>(true);
        foreach (var item in texts)
        {
            if ("KeyText".Equals(item.gameObject.name))
                KeyText = item;
            if ("IndicateText".Equals(item.gameObject.name))
                IndicateText = item;
        }
        IndicatorCanvas.gameObject.SetActive(false);
    }

    public void ShowIndicate(string Key, string Message)
    {
        IndicateText.text = Message;
        KeyText.text = Key;
        IndicatorCanvas.gameObject.SetActive(true);
    }

    public void HideIndicate()
    {
        IndicatorCanvas.gameObject.SetActive(false);
    }
}

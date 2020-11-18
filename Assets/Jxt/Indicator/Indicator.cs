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

    // Start is called before the first frame update
    void Start()
    {
        IndicatorCanvas = GameObject.Find("Indicator").GetComponent<Canvas>();
        IndicateText = GameObject.Find("IndicateText").GetComponent<Text>();
        KeyText = GameObject.Find("KeyText").GetComponent<Text>();

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Click222 : MonoBehaviour
{
    private GameObject canvas;
    bool shown = false;
    TipsController tipsController = null;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas");

        tipsController = GameObject.Find("Tips").GetComponent<TipsController>();
        gameObject.GetComponent<Button>().onClick.AddListener(switchTips);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void switchTips()
    {
        if (shown)
        {
            // tipsController.ShowTips("嘿嘿，这道题选什么你猜啊。\n猜不到吧233\nhhhhh");
            tipsController.HideTips();
            shown = false;

            canvas.transform.Find("wwl").gameObject.SetActive(false);//张峻凡
        }
        else
        {
            tipsController.ShowTips("嘿嘿，这道题选什么你猜啊。\n猜不到吧\nhhhhh");
            // tipsController.ShowTips("嘿嘿，这道题选什么你猜啊。\n猜不到吧2222\nhhhhh");
            shown = true;

            canvas.transform.Find("wwl").gameObject.SetActive(true);//张峻凡
        }
    }
}

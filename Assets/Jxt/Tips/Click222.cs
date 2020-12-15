using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Click222 : MonoBehaviour
{
    bool shown = false;
    TipsController tipsController = null;

    // Start is called before the first frame update
    void Start()
    {

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

        }
        else
        {
            tipsController.ShowTips("嘿嘿，这道题选什么你猜啊。\n猜不到吧\nhhhhh");
            // tipsController.ShowTips("嘿嘿，这道题选什么你猜啊。\n猜不到吧2222\nhhhhh");
            shown = true;

        }
    }
}

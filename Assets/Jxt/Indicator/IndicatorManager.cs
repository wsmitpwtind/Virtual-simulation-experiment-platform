using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 按键提示框管理器
/// </summary>
public class IndicatorManager : MonoBehaviour
{
    /// <summary>
    /// 第一个提示框。
    /// </summary>
    public Indicator Indicator1;
    /// <summary>
    /// 第二个提示框。
    /// </summary>
    public Indicator Indicator2;
    /// <summary>
    /// 第三个提示框。
    /// </summary>
    public Indicator Indicator3;

    // Start is called before the first frame update
    void Start()
    {
        //Indicator1 = GameObject.Find("Indicator1").GetComponent<Indicator>();
        //Indicator2 = GameObject.Find("Indicator2").GetComponent<Indicator>();
        //Indicator3 = GameObject.Find("Indicator3").GetComponent<Indicator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 隐藏所有提示框。
    /// </summary>
    public void HideAllIndicator()
    {
        Indicator1.HideIndicate();
        Indicator2.HideIndicate();
        Indicator3.HideIndicate();
    }
}

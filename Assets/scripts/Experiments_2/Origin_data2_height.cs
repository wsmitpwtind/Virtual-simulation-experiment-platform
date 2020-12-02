using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Origin_data2_height : MonoBehaviour
{
    
    private int Number = 1;
    private double Input = 1;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<InputField>().onEndEdit.AddListener(Accept);
    }

    // Update is called once per frame
    void Accept(string value)
    {
        if(double.TryParse(value,out Input)==true)
        {
            Exp_2.Experiment2_height[Number - 1] = Input;
            Number += 1;
            GameObject.Find("Text_number2").GetComponent<Text>().text = "请输入您高度的第" + Number + "个实验数据/mm";
        }
        
        
    }
}

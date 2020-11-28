using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Origin_data2 : MonoBehaviour
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
            Exp_2.Experiment2_measure[Number - 1] = Input;
            Number += 1;
            GameObject.Find("Text_number1").GetComponent<Text>().text = "请输入您的第" + Number + "个实验数据";
        }
        
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class To_page4 : MonoBehaviour
{
    private GameObject Canvas;
    private int Number = 11;//1的个数代表本页题目数

    private int Answers = 0;
    private double input1 = 0;
    // Start is called before the first frame update
    void Start()
    {
        Canvas = GameObject.Find("Canvas");
        GetComponent<Button>().onClick.AddListener(Topage4);
        GameObject.Find("InputField5").GetComponent<InputField>().onEndEdit.AddListener(Answer1);
        GameObject.Find("InputField6").GetComponent<InputField>().onEndEdit.AddListener(Answer2);
    }


    private void Answer1(string value)
    {
        if (double.TryParse(value, out input1) == true)
        {
            if (input1 - 5 == 0)//5为答案
            {
                Test_score.Pre_Test[5] = true;
            }
            else
            {
                Test_score.Pre_Test[5] = false;
            }

            if ((Answers & 1) == 0)
            {
                Answers += 1;
            }

        }
    }
    private void Answer2(string value)
    {
        if (value == "丙")//答案为丙
        {
            Test_score.Pre_Test[6] = true;
        }
        else
        {
            Test_score.Pre_Test[6] = false;
        }
        if ((Answers & 10) == 0)
        {
            Answers += 10;
        }
    }



    void Topage4()
    {
        if (Answers == Number)
        {
            Canvas.transform.Find("Page4").gameObject.SetActive(true);
            Canvas.transform.Find("Page3").gameObject.SetActive(false);
        }
    }
}

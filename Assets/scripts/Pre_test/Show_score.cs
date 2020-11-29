using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Show_score : MonoBehaviour
{
    private double score = 0;    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i < Test_score.Pre_Test.Length; i++)
        {
            if (Test_score.Pre_Test[i] == true)
            {
                score += 0.1;
            }
        }
        GetComponent<Text>().text += score;
        Debug.Log(Test_score.Pre_Test[1]);
        Debug.Log(Test_score.Pre_Test[3]);
        Debug.Log(Test_score.Pre_Test[5]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Answer_2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Text>().text = "正确不确定度为:" + Exp_2.V_real_thickness + StaticMethods.BookVolume(Exp_2.Experiment2_length, Exp_2.Experiment2_width, Exp_2.Experiment2_height).Item2; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

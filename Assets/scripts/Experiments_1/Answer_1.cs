﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Answer_1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Text>().text = "正确不确定度为:"+Exp_1.A_real_thickness;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
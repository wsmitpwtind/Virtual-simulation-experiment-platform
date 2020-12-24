﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Create_vernier : MonoBehaviour
{
    public GameObject My_vernier;
    private GameObject Now_vernier;
    private GameObject Now_vernier1;
    private GameObject Now_vernier2;
    private Vector3 Vv1;
    private Vector3 Vv2;
    Vector3 position = new Vector3(0.3f, 2f, -1.8f);
    Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, 0));

    IndicatorManager indicatorManager;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(Vernier);
        Now_vernier = GameObject.Find("Real_Vernier");
        indicatorManager = GameObject.Find("Indicator").GetComponent<IndicatorManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Vernier()
    {
        if (Now_vernier.transform.position.x-9f>0)
        {
            Now_vernier.transform.position = position;
            //indicatorManager.Indicator2.ShowIndicate("P", "进行夹持");
        }
        else
        {
            Now_vernier.transform.position= new Vector3(10.2f, 2f, -2.5f);
            //indicatorManager.Indicator2.HideIndicate();
        }

        GameObject.Find("Dropdown").GetComponent<Dropdown>().value = 0;
    }
}

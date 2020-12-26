﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{    
    private GameObject Player;
    private GameObject Camera;
    private GameObject Canvas;

    public static int Move_able = 1;

    public static MonitorableValue<int> state = new MonitorableValue<int>(0);
    /*
     * 0代表刚进入教室，1代表坐上凳子，2代表呼出了菜单栏中的仪器部分，3代表呼出了实验一的数据处理，
     * 4代表呼出了实验二的数据处理，5代表正在拖动书和尺子，6代表开始读数，7代表所有实验完成


    */
    public static Record record = new Record();//根据存档初始化



    private void Awake()
    {
        //加载存档,优先级大于start
        if (RecordManager.RecordContains(RecordManager.currentRecordId))
        {
            record = RecordManager.currentDefaultRecord;
        }
        else
        {
            RecordManager.currentRecordId = RecordManager.GetFirstNone();
        }
    }
    void Start()
    {
        Player = GameObject.Find("Player");
        Camera = GameObject.Find("MainCamera");
        Canvas = GameObject.Find("Canvas");


        //赋值
        Manager.state.Value = record.Manager_state;







        if (Manager.state.Value > 0)
        {
            Invoke("SitonChair", 0.01f);
            
        }





        
        
    }
    private void SitonChair()
    {
        GameObject.Find("Chair_01_Snaps014").GetComponent<Start_experiment>().Start_the_experienment();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        If_bug();
        

        if (Manager.state.Equals(1))
        {
                        
            if (Input.GetKey(KeyCode.Q))
            {
                Debug.Log("退出试验");
                Manager.state.Value = 0;
                Quit_the_experienment();
            }
        }

        
    }


    void If_bug()
    {
        if(Player.transform.position.y < 0f)
        {
            Manager.state.Value = 0;
            Quit_the_experienment();
            SceneManager.LoadScene("Bug");
        }
    }
    

    void Quit_the_experienment()
    {
        Canvas.transform.Find("Dropdown").gameObject.SetActive(false);
        Camera.GetComponent<Look>().enabled = true;
        Manager.Move_able = 1;
        GameObject.Find("Indicator").GetComponent<IndicatorManager>().HideAllIndicator();
    }
}



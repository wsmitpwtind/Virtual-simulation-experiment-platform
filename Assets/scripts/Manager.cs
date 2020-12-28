using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Manager : MonoBehaviour
{
    private GameObject Player;
    private GameObject Camera;
    private GameObject Canvas;

    public static int Move_able = 1;

    public static MonitorableValue<int> state = new MonitorableValue<int>(0);
    /*
     * 0代表刚进入教室，1代表坐上凳子，2代表呼出了菜单栏中的仪器部分，3代表生成了尺子或游标卡尺，4代表开始读数，5代表所有实验完成
    */
    public static Record record = new Record();//根据存档初始化
    public static void UpdateRecord()
    {
        Move_able = 1;
        var rec = RecordManager.currentDefaultRecord;
        if (rec != null)
            record = rec;
        else
            record = new Record();
        state.Value = record.Manager_state;
    }
    private void Awake()
    {
        //加载存档,优先级大于start
        UpdateRecord();
        Exp_2.Initialize();
    }
    void Start()
    {
        Player = GameObject.Find("Player");
        Camera = GameObject.Find("MainCamera");
        Canvas = GameObject.Find("Canvas");

        //赋值
        GameObject.Find("book_0001b").GetComponent<Transform>().position = record.BookPosition;
        GameObject.Find("book_0001b").GetComponent<Transform>().localEulerAngles = record.BookRotation;
        GameObject.Find("ruler").GetComponent<Transform>().position = record.RulerPosition;
        GameObject.Find("ruler").GetComponent<Transform>().localEulerAngles = record.RulerRotation;
        GameObject.Find("Real_Vernier").GetComponent<Transform>().position = record.VerinerPosition;
        GameObject.Find("Real_Vernier").GetComponent<Transform>().localEulerAngles = record.VerinerRotation;


        if (Manager.state.Value > 0)
        {
            Invoke("SitonChair", 0.01f);
            Move_able = 0;
        }
        Debug.Log($"Manager: {Manager.state.Value}");
        Debug.Log($"Exp_2: {Exp_2.state.Value}");
    }
    private void SitonChair()
    {
        GameObject.Find("Chair_01_Snaps014").GetComponent<Start_experiment>().Start_the_experienment();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        If_bug();


        if (Exp_2.state.Value == 5)
        {
            Manager.state.Value = 5;
        }

        if (Manager.state.Equals(5))
        {

            if (Input.GetKey(KeyCode.Q))
            {
                Quit_the_experienment();
            }
        }


    }


    void If_bug()
    {
        if (Player.transform.position.y < 0f)
        {
            Manager.state.Value = 0;
            Quit_the_experienment();
            SceneManager.LoadScene("Bug");
        }
    }


    void Quit_the_experienment()
    {
        Canvas.transform.Find("Dropdown").gameObject.GetComponent<Dropdown>().value = 0;
        Canvas.transform.Find("Dropdown").gameObject.SetActive(false);
        Camera.GetComponent<Look>().enabled = true;
        Manager.Move_able = 1;
        GameObject.Find("Indicator").GetComponent<IndicatorManager>().HideAllIndicator();
    }
}



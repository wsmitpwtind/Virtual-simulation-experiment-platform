using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wwlplay : MonoBehaviour
{
    private float[] time = {7f,6f,6f};//每段音频的时间
    private bool[] IfnotFirst = new bool[10];
    private bool isplay = false;

    private void Start()
    {
        Exp_2.state.onMyValueChanged += test;   
    }

    private void Update()
    {
        if (Exp_1.state_1 == 0&&IfnotFirst[0]==false&&isplay==false)
        {
            transform.Find("Wwl").gameObject.SetActive(true);
            transform.Find("voice0").gameObject.SetActive(true);
            IfnotFirst[0] = true;
            isplay = true;
            Invoke("ShutWwl", time[0]);
        }
        if (Exp_1.state_1 == 1 && IfnotFirst[1] == false && isplay == false)
        {
            transform.Find("Wwl").gameObject.SetActive(true);
            transform.Find("voice1").gameObject.SetActive(true);
            IfnotFirst[1] = true;
            Invoke("ShutWwl", time[1]);
        }


        //Debug.Log(Exp_2.state);
    }

    private void ShutWwl()
    {
        transform.Find("Wwl").gameObject.SetActive(false);
        isplay = false;
    }

    private void test(object sender, MonitorableValue<int>.ValueChangedEventArgs e)
    {
        if (e.newValue == 1)
        {
            Debug.Log("123");
        }

    }


}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wwlplay : MonoBehaviour
{
    private float[] time = {7f,6f,6f};//每段音频的时间
    private bool isplay = false;
    private int i_2 = 0;//播放第几个视频,与state相对应

    private void Start()
    {
        Exp_2.state.onMyValueChanged += Voice;



        //开场白
        WakeWwl();

    }

    private void Update()
    {


    }

    private void ShutWwl()
    {
        transform.Find("Wwl").gameObject.SetActive(false);
        isplay = false;
    }
    private void WakeWwl()
    {
        if (isplay == true)
        {
            Invoke("WakeWwl",1f);
        }
        else
        {
            transform.Find("Wwl").gameObject.SetActive(true);
            transform.Find("voice" + i_2).gameObject.SetActive(true);
            Invoke("ShutWwl", time[i_2]);
            i_2++;
            isplay = true;
        }
        
    }

    private void Voice(object sender, MonitorableValue<int>.ValueChangedEventArgs e)
    {
        if (e.newValue == i_2)
        {
            WakeWwl();
        }
        
        
    }




}
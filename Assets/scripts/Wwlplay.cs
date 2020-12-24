using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wwlplay : MonoBehaviour
{
    private float[] time0_ = {7f,6f,6f};//每段音频的时间
    private float[] time1_ = { 7f, 6f, 6f };
    private float[] time2_ = { 7f, 6f, 6f };
    private bool isplay = false;


    private bool[] ifFirst0_= {true, true, true};
    private bool[] ifFirst1_= { true, true, true };
    private bool[] ifFirst2_= { true, true, true };
    private int i_0 = 0;//_0代表manager
    private int i_1 = 0;
    private int i_2 = 0;//播放第几个视频,与state相对应

    private void Start()
    {
        Exp_2.state.onMyValueChanged += Voice2_;



        //开场白
        if (Manager.state.Value == 0)
        {
            WakeWwl0_();
        }
        

    }

    private void Update()
    {

    }

    private void ShutWwl()
    {
        transform.Find("Wwl").gameObject.SetActive(false);
        isplay = false;
    }
    private void WakeWwl0_()
    {
        if (isplay == true)
        {
            Invoke("WakeWwl0_",1f);
        }
        else
        {
            if (ifFirst0_[i_0] == true)
            {
                transform.Find("Wwl").gameObject.SetActive(true);
                transform.Find("voice0_" + i_0).gameObject.SetActive(true);
                Invoke("ShutWwl", time0_[i_0]);
                i_0++;
                isplay = true;
                ifFirst0_[i_0]= false;
                    
            }
            
        }
        
    }
    private void WakeWwl1_()
    {
        if (isplay == true)
        {
            Invoke("WakeWwl1_", 1f);
        }
        else
        {
            if (ifFirst1_[i_1] == true)
            {
                transform.Find("Wwl").gameObject.SetActive(true);
                transform.Find("voice1_" + i_1).gameObject.SetActive(true);
                Invoke("ShutWwl", time1_[i_1]);
                i_1++;
                isplay = true;
                ifFirst1_[i_1] = false;

            }
        }

    }
    private void WakeWwl2_()
    {
        if (isplay == true)
        {
            Invoke("WakeWwl2_", 1f);
        }
        else
        {
            if (ifFirst2_[i_2] == true)
            {
                transform.Find("Wwl").gameObject.SetActive(true);
                transform.Find("voice2_" + i_2).gameObject.SetActive(true);
                Invoke("ShutWwl", time2_[i_2]);
                i_2++;
                isplay = true;
                ifFirst0_[i_2] = false;

            }
        }

    }



    private void Voice0_(object sender, MonitorableValue<int>.ValueChangedEventArgs e)
    {
        if (e.newValue == i_2)
        {
            WakeWwl0_();
        }

    }
    private void Voice1_(object sender, MonitorableValue<int>.ValueChangedEventArgs e)
    {
        if (e.newValue == i_2)
        {
            WakeWwl1_();
        }

    }
    private void Voice2_(object sender, MonitorableValue<int>.ValueChangedEventArgs e)
    {
        if (e.newValue == i_2)
        {
            WakeWwl2_();
        }
                
    }




}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wwlplay : MonoBehaviour
{
    private float[] time0_ = { 7f, 5f, 13f, 8f, 7f, 7f };//每段音频的时间
    private float[] time1_ = { 0f, 6f, 6f };
    private float[] time2_ = { 15f, 10f, 9f, 9f };
    private bool isplay = false;


    public static bool[] ifFirst0_ = { true, true, true, true, true, true };
    public static bool[] ifFirst1_ = { true, true, true };
    public static bool[] ifFirst2_ = { true, true, true, true, true };
    public static int i_0 = 0;//_0代表manager
    public static int i_1 = 0;
    public static int i_2 = 0;//播放第几个视频,与state相对应

    private MonitorableValue<float> timeScale = new MonitorableValue<float>(1);
    private void Start()
    {
        //初始化
        ifFirst0_ = Manager.record.ifFirst0_;
        ifFirst1_ = Manager.record.ifFirst1_;
        ifFirst2_ = Manager.record.ifFirst2_;
        i_0 = Manager.record.i_0;
        i_1 = Manager.record.i_1;
        i_2 = Manager.record.i_2;


        Manager.state.onMyValueChanged += Voice0_;
        Exp_1.state.onMyValueChanged += Voice1_;
        Exp_2.state.onMyValueChanged += Voice2_;

        timeScale.onMyValueChanged += OnPause;


        //开场白
        if (Manager.state.Value == 0&&i_0==0)
        {
            WakeWwl0_();
        }


    }
    private void OnPause(object sender, MonitorableValue<float>.ValueChangedEventArgs e)
    {
        foreach (var item in GetComponentsInChildren<AudioSource>(true))
        {
            item.pitch = e.newValue;
        }
    }
    private void Update()
    {
        timeScale.Value = Time.timeScale;
    }

    private void ShutWwl()
    {
        transform.Find("Wwl").gameObject.SetActive(false);
        isplay = false;


        List<Transform> lst = new List<Transform>();
        foreach (Transform child in transform)
        {
            lst.Add(child);
        }
        for (int i = 0; i < lst.Count; i++)
        {
            lst[i].gameObject.SetActive(false);
        }

    }
    private void WakeWwl0_()
    {
        if (isplay == true)
        {
            Invoke("WakeWwl0_", 1f);
        }
        else
        {
            if (ifFirst0_[i_0] == true)
            {
                Debug.Log(transform);
                transform.Find("Wwl").gameObject.SetActive(true);
                transform.Find("voice0_" + i_0).gameObject.SetActive(true);
                Invoke("ShutWwl", time0_[i_0]);
                ifFirst0_[i_0] = false;
                i_0++;
                isplay = true;

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
                ifFirst1_[i_1] = false;
                i_1++;
                isplay = true;


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
                ifFirst0_[i_2] = false;
                i_2++;
                isplay = true;


            }
        }

    }



    private void Voice0_(object sender, MonitorableValue<int>.ValueChangedEventArgs e)
    {
        if (e.newValue == i_0)
        {
            WakeWwl0_();
        }        

    }
    private void Voice1_(object sender, MonitorableValue<int>.ValueChangedEventArgs e)
    {
        if (e.newValue == 1)
        {
            WakeWwl1_();
        }

    }
    private void Voice2_(object sender, MonitorableValue<int>.ValueChangedEventArgs e)
    {
        if (e.newValue == 1 || e.newValue == 3 || e.newValue == 4 || e.newValue == 5)
        {
            WakeWwl2_();
        }

    }

    private void OnDestroy()
    {
        Manager.state.onMyValueChanged -= Voice0_;
        Exp_1.state.onMyValueChanged -= Voice1_;
        Exp_2.state.onMyValueChanged -= Voice2_;
    }


}
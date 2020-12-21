using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Exp_2 : MonoBehaviour
{
    public static double[] Experiment2_length = new double[100];
    public static double[] Experiment2_width = new double[100];
    public static double[] Experiment2_height = new double[100];

    public static double A_User = 0;//使用者计算的长度的平均值
    public static double UA_User = 0;//使用者计算的长度的不确定度
    public static double A_Real = 0;//书籍的真实长度,不存在不确定度
    public static double B_User = 0;
    public static double UB_User = 0;
    public static double B_Real = 0;
    public static double C_User = 0;
    public static double UC_User = 0;
    public static double C_Real = 0;
    public static double V_User = 0;
    public static double UV_User = 0;
    public static double V_Real = 0;

    public static int Move_able = 1;
    public static double score = 0;


    public static MonitorableValue<int> state = new MonitorableValue<int>(0);//含义如下
    /*
    0代表未开始，1代表正在实验，2代表结束实验




    */





    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        
    }
         
}



public class MonitorableValue<T> where T : IEquatable<T>
{
    public MonitorableValue()
    {

    }
    public MonitorableValue(T value)
    {
        _value = value;
    }
    private T _value;
    public T Value
    {
        get => _value;
        set
        {
            if (!_value.Equals(value))
                WhenValueChange(value);
            _value = value;
        }
    }
    public delegate void ValueChanged(object sender, ValueChangedEventArgs e);
    public event ValueChanged onMyValueChanged;
    private void WhenValueChange(T value)
    {
        if (onMyValueChanged != null)
            onMyValueChanged(this, new ValueChangedEventArgs()
            {
                oldValue = _value,
                newValue = value
            });
    }
    public override bool Equals(object obj)
    {
        return Value.Equals(obj);
    }
    public override string ToString()
    {
        return Value.ToString();
    }
    public class ValueChangedEventArgs : EventArgs
    {
        public T oldValue { get; set; }
        public T newValue { get; set; }
    }
}

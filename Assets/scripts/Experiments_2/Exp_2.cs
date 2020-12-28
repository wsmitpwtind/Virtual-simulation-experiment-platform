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
    public static double A_Real = 247.8f;//书籍的真实长度,不存在不确定度
    public static double B_User = 0;
    public static double UB_User = 0;
    public static double B_Real = 161.2f;
    public static double C_User = 0;
    public static double UC_User = 0;
    public static double C_Real = 27.7f;
    public static double V_User = 0;
    public static double UV_User = 0;
    public static double V_Real = 0;
    public static double score = 0;


    public static MonitorableValue<int> state = new MonitorableValue<int>(0);//含义如下
    /*
    0代表未开始
    1代表填长度的实验数据
    2代表填宽度的实验数据
    3代表填高度的实验数据
    4代表数据处理
    5代表实验完成
    */

    void Start()
    {
        //赋初值
        Initialize();
    }

    public static void Initialize()
    {
        Exp_2.state.Value = Manager.record.Exp2_state;
        Experiment2_length = Manager.record.Exp2_length;
        Experiment2_width = Manager.record.Exp2_width;
        Experiment2_height = Manager.record.Exp2_height;
        A_User = Manager.record.Exp2_A_User;
        UA_User = Manager.record.Exp2_UA_User;
        A_Real = Manager.record.Exp2_A_Real;
        B_User = Manager.record.Exp2_B_User;
        UB_User = Manager.record.Exp2_UB_User;
        B_Real = Manager.record.Exp2_B_Real;
        C_User = Manager.record.Exp2_C_User;
        UC_User = Manager.record.Exp2_UC_User;
        C_Real = Manager.record.Exp2_C_Real;
        V_User = Manager.record.Exp2_V_User;
        UV_User = Manager.record.Exp2_UV_User;
        V_Real = Manager.record.Exp2_V_Real;
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

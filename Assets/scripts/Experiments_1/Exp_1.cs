using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Exp_1 : MonoBehaviour
{
    public static double[] Experiment1_measure = new double[1000];
    public static double A_thickness = 0;
    public static double B_thickness = 0;
    public static double A_real_thickness = 0;
    public static double B_real_thickness = 0;
    public static int Move_able = 1;
    public static double score_1 = 0;
    public static int state_1 = 0;//0代表未开始，1代表正在实验，2代表结束实验

    public static MonitorableValue<int> state = new MonitorableValue<int>(0);

    // Start is called before the first frame update
    void Start()
    {
        // 订阅事件
        state.onMyValueChanged += (sender, e) => {
            Debug.Log($"State Value has changed from {e.oldValue} to {e.newValue}");
            // State Value has changed from 0 to 1
        };
        // 更改值
        state.Value = 1;
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
            if (_value.Equals(value))
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
    public class ValueChangedEventArgs : EventArgs
    {
        public T oldValue { get; set; }
        public T newValue { get; set; }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wwlplay : MonoBehaviour
{
    private float[] time = {10f,100f};//每段音频的时间
    private bool[] IfnotFirst = new bool[10]; 
    private void Update()
    {
        if (Exp_1.state_1 == 1&&IfnotFirst[0]==false)
        {
            transform.Find("Wwl").gameObject.SetActive(true);
            transform.Find("voice2").gameObject.SetActive(true);
            IfnotFirst[0] = true;
            Invoke("ShutWwl", time[0]);
        }
        if (Exp_1.state_1 == 0 && IfnotFirst[1] == false)
        {
            transform.Find("Wwl").gameObject.SetActive(true);
            transform.Find("voice1").gameObject.SetActive(true);
            IfnotFirst[1] = true;
            Invoke("ShutWwl", time[1]);
        }

    }

    private void ShutWwl()
    {
        transform.Find("Wwl").gameObject.SetActive(false);
    }


}
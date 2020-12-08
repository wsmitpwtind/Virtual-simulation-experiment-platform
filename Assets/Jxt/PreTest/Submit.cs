using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 提交按钮处理程序
/// </summary>
public class Submit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(GameObject.Find("Canvas").GetComponent<LoadQuestion>().SubmitListener);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

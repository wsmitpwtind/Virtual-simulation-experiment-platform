using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 下一页按钮处理程序
/// </summary>
public class NextQuestion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(GameObject.Find("Canvas").GetComponent<LoadQuestion>().NextListener);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

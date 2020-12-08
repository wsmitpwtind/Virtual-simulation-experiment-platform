using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 关闭未作答提示框的脚本。
/// </summary>
public class CloseInputEmpty : MonoBehaviour
{
    private FadeAnimate aniInstant = null;
    private GameObject inputEmpty = null;

    // Start is called before the first frame update
    void Start()
    {
        inputEmpty = GameObject.Find("InputEmpty");
        aniInstant = GameObject.Find("Canvas").GetComponent<FadeAnimate>();
        GetComponent<Button>().onClick.AddListener(Close);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Close()
    {
        aniInstant.Hide(inputEmpty, 200);
    }
}

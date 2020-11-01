using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public static int state = 0; //0代表未开始，1代表正在实验，2代表结束实验
    private GameObject player;
    private GameObject Canvas;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        Canvas = GameObject.Find("Canvas");
        state = 0;
    }

    // Update is called once per frame
    void Update()
    {
        If_bug();

        if (state == 1)
        {
            
            Debug.Log("正在试验");
            if (Input.GetKey(KeyCode.Q))
            {
                Debug.Log("退出试验");
                state = 0;
                Quit_the_experienment();
            }
        }
        
    }


    void If_bug()
    {
        if(player.transform.position.y < 0f)
        {
            SceneManager.LoadScene("Bug");
        }
    }

    void Quit_the_experienment()
    {
        Canvas.transform.Find("Dropdown").gameObject.SetActive(false);
    }
}

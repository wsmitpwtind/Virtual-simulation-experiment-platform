using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{    
    private GameObject Player;
    private GameObject Camera;
    private GameObject Canvas;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        Camera = GameObject.Find("MainCamera");
        Canvas = GameObject.Find("Canvas");
        
    }

    // Update is called once per frame
    void Update()
    {
        If_bug();
        

        if (Exp_1.state_1 == 1)
        {
                        
            if (Input.GetKey(KeyCode.Q))
            {
                Debug.Log("退出试验");
                Exp_1.state_1 = 0;
                Quit_the_experienment();
            }
        }
        
    }


    void If_bug()
    {
        if(Player.transform.position.y < 0f)
        {
            Exp_1.state_1 = 0;
            Quit_the_experienment();
            SceneManager.LoadScene("Bug");
        }
    }
    

    void Quit_the_experienment()
    {
        Canvas.transform.Find("Dropdown").gameObject.SetActive(false);
        Camera.GetComponent<Look>().enabled = true;
        Exp_1.Move_able = 1;
    }
}

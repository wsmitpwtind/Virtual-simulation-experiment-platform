using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Open_door : MonoBehaviour
{
    private bool Is_open = false;
    private GameObject Door;
    private GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Door = GameObject.Find("Door_hint");
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        If_end();
    }
    void OnTriggerEnter(Collider other)
    {
        Open_classroom_door();
        Is_open = true;
    }
    void OnTriggerExit(Collider other)
    {
        Close_classroom_door();
        Is_open = false;
    }

    void Open_classroom_door()
    {
        if (Player.transform.position.z < 3.7f)
        {
            Door.transform.localEulerAngles = new Vector3(0, -75, 0);
        }else
        {
            Door.transform.localEulerAngles = new Vector3(0, 75, 0);
        }
        
    }
    void Close_classroom_door()
    {
        Door.transform.localEulerAngles = new Vector3(0, 0, 0);
    }
    void If_end()
    {
        if (Is_open && Player.transform.position.z > 4.5f)
        {
            SceneManager.LoadScene("End");
        }
    }

}

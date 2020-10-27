using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Open_door : MonoBehaviour
{
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
        
    }
    void OnTriggerEnter(Collider other)
    {
        Open_classroom_door();
    }
    void OnTriggerExit(Collider other)
    {
        Close_classroom_door();
    }

    void Open_classroom_door()
    {
        if (Player.transform.position.z < 3.2f)
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start_experiment : MonoBehaviour
{
    private GameObject Canvas;
    private GameObject Camera;
    private GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Canvas = GameObject.Find("Canvas");
        Camera = GameObject.Find("MainCamera");
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.E))
        {
            Exp_1.state_1 = 1;//开始实验
            Start_the_experienment();
        }
    }
    void Start_the_experienment()
    {
        Canvas.transform.Find("Dropdown").gameObject.SetActive(true);
        Camera.GetComponent<Look>().enabled = false;

        Player.transform.rotation = Quaternion.Euler(new Vector3(0f, -90f, 0f));
        Camera.transform.rotation = Quaternion.Euler(new Vector3(30f, -90f, 0f));
    }
}

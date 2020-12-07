using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start_experiment : MonoBehaviour
{
    private GameObject Canvas;
    private GameObject Camera;
    private GameObject Player;
    private bool enan=true;
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
        if (Input.GetKeyDown(KeyCode.D)&& Exp_1.state_1 == 1)
        {
            Camera.GetComponent<Look>().enabled = false;
            print(Camera.GetComponent<Look>().enabled);
        }
        if (Input.GetKeyDown(KeyCode.F) && Exp_1.state_1 == 1)
        {
            Camera.GetComponent<Look>().enabled = true;
            print(Camera.GetComponent<Look>().enabled);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.E))
        {
            Exp_1.state_1 = 1;//开始实验
            Start_the_experienment();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Exp_1.state_1 != 1)
            GameObject.Find("Canvas").GetComponent<Indicator>().ShowIndicate("E", "开始实验");
    }

    private void OnTriggerExit(Collider other)
    {
        if (Exp_1.state_1 != 1)
            GameObject.Find("Canvas").GetComponent<Indicator>().HideIndicate();
    }

    void Start_the_experienment()
    {
        GameObject.Find("Canvas").GetComponent<Indicator>().HideIndicate();
        Canvas.transform.Find("Dropdown").gameObject.SetActive(true);
        Exp_1.Move_able = 0;
        Player.transform.position = new Vector3(1.18f, 1.6f, -1.58f);
        Player.transform.rotation = Quaternion.Euler(new Vector3(0f, -90f, 0f));
        Camera.transform.rotation = Quaternion.Euler(new Vector3(30f, -90f, 0f));
        Camera.GetComponent<Look>().enabled = true;
        GameObject.Find("Canvas").GetComponent<Indicator>().ShowIndicate("D", "锁定视角");
        GameObject.Find("Canvas2").GetComponent<Indicator>().ShowIndicate("F", "解锁视角");
        if (Input.GetKey(KeyCode.F))
        {
            Camera.GetComponent<Look>().enabled = !Camera.GetComponent<Look>().enabled;
        }
    }
}

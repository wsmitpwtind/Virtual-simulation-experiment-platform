using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start_experiment : MonoBehaviour
{
    private GameObject Canvas;
    private GameObject Camera;
    private GameObject Player;
    IndicatorManager indicatorManager;

    // Start is called before the first frame update
    void Start()
    {
        Canvas = GameObject.Find("Canvas");
        Camera = GameObject.Find("MainCamera");
        Player = GameObject.Find("Player");

        indicatorManager = GameObject.Find("Indicator").GetComponent<IndicatorManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D)&& Exp_1.state_1 == 1)
        {
            Camera.GetComponent<Look>().enabled = false;
            print(Camera.GetComponent<Look>().enabled);
            indicatorManager.Indicator1.ShowIndicate("F", "解锁视角");
        }
        if (Input.GetKeyDown(KeyCode.F) && Exp_1.state_1 == 1)
        {
            Camera.GetComponent<Look>().enabled = true;
            print(Camera.GetComponent<Look>().enabled);
            indicatorManager.Indicator1.ShowIndicate("D", "锁定视角");
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
            indicatorManager.Indicator1.ShowIndicate("E", "开始实验");
    }

    private void OnTriggerExit(Collider other)
    {
        if (Exp_1.state_1 != 1)
            indicatorManager.Indicator1.HideIndicate();

    }

    void Start_the_experienment()
    {
        indicatorManager.HideAllIndicator();
        Canvas.transform.Find("Dropdown").gameObject.SetActive(true);
        Exp_1.Move_able = 0;
        Player.transform.position = new Vector3(1.18f, 1.6f, -1.58f);
        Player.transform.rotation = Quaternion.Euler(new Vector3(0f, -90f, 0f));
        Camera.transform.rotation = Quaternion.Euler(new Vector3(30f, -90f, 0f));
        Camera.GetComponent<Look>().enabled = false;
        indicatorManager.Indicator1.ShowIndicate("F", "解锁视角");
        if (Input.GetKey(KeyCode.F))
        {
            Camera.GetComponent<Look>().enabled = !Camera.GetComponent<Look>().enabled;
        }
    }
}

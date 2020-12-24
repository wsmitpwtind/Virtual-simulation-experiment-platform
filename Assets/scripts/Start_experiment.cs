using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start_experiment : MonoBehaviour {
    private GameObject Canvas;
    private GameObject Camera;
    private GameObject Player;
    IndicatorManager indicatorManager;
    Look cam_look;

    // Start is called before the first frame update
    void Start() {
        Canvas = GameObject.Find("Canvas");
        Camera = GameObject.Find("MainCamera");
        Player = GameObject.Find("Player");
        cam_look = Camera.GetComponent<Look>();
        indicatorManager = GameObject.Find("Indicator").GetComponent<IndicatorManager>();
    }

    // Update is called once per frame
    void Update() {
        if(Input.GetKeyDown(KeyCode.D) && Manager.state.Equals(1)) {
            cam_look.enabled = false;
            //print(cam_look.enabled);
            indicatorManager.Indicator1.ShowIndicate("F", "解锁视角");
        }
        if(Input.GetKeyDown(KeyCode.F) && Manager.state.Equals(1)) {
            cam_look.enabled = true;
            //print(cam_look.enabled);
            indicatorManager.Indicator1.ShowIndicate("D", "锁定视角");
        }
    }

    void OnTriggerStay(Collider other) {
        if(Input.GetKey(KeyCode.E)) {
            Manager.state.Value=1;//开始实验
            Start_the_experienment();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(Manager.state.Equals(0))
            indicatorManager.Indicator1.ShowIndicate("E", "开始实验");
    }

    private void OnTriggerExit(Collider other) {
        if(Exp_2.state.Equals(0))
            indicatorManager.Indicator1.HideIndicate();

    }

    public void Start_the_experienment() {
        
        if (Manager.state.Value == 0)
        {
            Manager.state.Value = 1;
        }
        indicatorManager.HideAllIndicator();
        Canvas.transform.Find("Dropdown").gameObject.SetActive(true);
        Manager.Move_able = 0;
        Player.transform.position = new Vector3(1.18f, 1.6f, -1.58f);
        Player.transform.rotation = Quaternion.Euler(new Vector3(0f, -90f, 0f));
        Camera.transform.rotation = Quaternion.Euler(new Vector3(30f, -90f, 0f));
        GameObject CAI = GameObject.Find("Camera_invisiable");
        CAI.transform.rotation = Quaternion.Euler(new Vector3(30f, -90f, 0f));
        cam_look.enabled = false;
        indicatorManager.Indicator1.ShowIndicate("F", "解锁视角");
        if(Input.GetKey(KeyCode.F)) {
            cam_look.enabled = cam_look.enabled;
        }
    }
}

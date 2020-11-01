using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start_experiment : MonoBehaviour
{
    private GameObject Canvas;
    // Start is called before the first frame update
    void Start()
    {
        Canvas = GameObject.Find("Canvas");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.E))
        {
            Manager.state = 1;//开始实验
            Start_the_experienment();
        }
    }
    void Start_the_experienment()
    {
        Canvas.transform.Find("Dropdown").gameObject.SetActive(true);
    }
}

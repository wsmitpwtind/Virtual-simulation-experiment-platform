using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Show_place : MonoBehaviour
{
    // Start is called before the first frame update

    Camera mCamera;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.B))
        {
            GameObject RuC = GameObject.Find("Camera");
            this.transform.position = RuC.transform.position;
            this.transform.rotation = RuC.transform.rotation;
            mCamera = gameObject.GetComponent<Camera>();
            mCamera.fieldOfView = 30.0f;
        }
    }
}

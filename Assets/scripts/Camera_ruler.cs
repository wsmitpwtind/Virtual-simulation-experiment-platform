using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_ruler : MonoBehaviour
{
    GameObject Cam2 = GameObject.Find("Camera_ruler");
    GameObject ruler = GameObject.Find("ruler(Clone)");
    // Start is called before the first frame update
    void Start()
    {
        GameObject Cam2 = GameObject.Find("Camera_ruler");
        Cam2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }
}

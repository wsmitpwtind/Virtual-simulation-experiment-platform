using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Player_speed = 5f;
    public float Sensitivety_mouse_wheel = 10f;
    
    void Update()
    {
        Move();
        Lookaround();
        
       
    }
    void Move()
    {
        if (Input.GetKey(KeyCode.W) | Input.GetKey(KeyCode.UpArrow)) //前
        {
            this.transform.Translate(Vector3.forward * Player_speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S) | Input.GetKey(KeyCode.DownArrow)) //后
        {
            this.transform.Translate(Vector3.forward * -Player_speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A) | Input.GetKey(KeyCode.LeftArrow)) //左
        {
            this.transform.Translate(Vector3.right * -Player_speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D) | Input.GetKey(KeyCode.RightArrow)) //右
        {
            this.transform.Translate(Vector3.right * Player_speed * Time.deltaTime);
        }
    }
    void Lookaround()
    {
        //滚轮实现镜头缩进和拉远
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            GetComponent<Camera>().fieldOfView = GetComponent<Camera>().fieldOfView - Input.GetAxis("Mouse ScrollWheel") * Sensitivety_mouse_wheel;
        }

    }
}

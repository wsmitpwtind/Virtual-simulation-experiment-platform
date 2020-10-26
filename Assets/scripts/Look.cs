using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour
{
    public float Sensitivety_mouse_wheel = 10f;
    private float Field_of_view;


    // Start is called before the first frame update
    void Start()
    {
        Field_of_view = GetComponent<Camera>().fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        Lookaround();

        
    }
    void Lookaround()
    {
        //滚轮实现镜头缩进和拉远
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            Field_of_view = Field_of_view - Input.GetAxis("Mouse ScrollWheel") * Sensitivety_mouse_wheel;
            if (Field_of_view < 10f)
            {
                Field_of_view = 10f;
            }
            else if(Field_of_view > 60f)
            {
                Field_of_view = 60f;
            }
            GetComponent<Camera>().fieldOfView = Field_of_view;

        }

        //视角随鼠标移动
        
    }
    
}

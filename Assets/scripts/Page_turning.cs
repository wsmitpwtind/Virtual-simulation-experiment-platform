using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Page_turning : MonoBehaviour
{
    // Start is called before the first frame update
    public Material material;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            material.SetFloat("_Angle", 0);
            Exam_left();
        }
        if (Input.GetKey(KeyCode.D))
        {
            material.SetFloat("_Angle", 180);
            Exam_right();
        }
    }

    void Exam_left()
    {
        if (material.GetFloat("_Angle") < 180)
        {
            Left_turning();
            Invoke("Exam_left", 0.1f);
        }
    }
    void Left_turning()
    {
        material.SetFloat("_Angle", material.GetFloat("_Angle")+1);
    }
    void Exam_right()
    {
        if (material.GetFloat("_Angle") >0)
        {
            Right_turning();
            Invoke("Exam_right", 0.1f);
        }
    }
    void Right_turning()
    {
        material.SetFloat("_Angle", material.GetFloat("_Angle") - 1);
    }
}

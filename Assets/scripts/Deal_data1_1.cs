using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Deal_data1_1 : MonoBehaviour
{
    private double Input = 0;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<InputField>().onEndEdit.AddListener(Accept);
    }

    // Update is called once per frame
    void Accept(string value)
    {
        if (double.TryParse(value, out Input) == true)
        {
            Exp_1.A_thickness = Input;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Answer_2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Text>().text =StaticMethods.BookVolume(Exp_2.Experiment2_length, Exp_2.Experiment2_width, Exp_2.Experiment2_height).Item3; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

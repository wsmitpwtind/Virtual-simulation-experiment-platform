using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    private GameObject Instrument;
    private GameObject Calculator;
    // Start is called before the first frame update
    void Start()
    {
        Instrument = GameObject.Find("Instrument");
        Calculator = GameObject.Find("Calculator");
        GameObject.Find("Dropdown").GetComponent<Dropdown>().onValueChanged.AddListener(ConsoleResult);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ConsoleResult(int value)
    {
        if (value == 0)
        {
            Instrument.transform.Find("buttons").gameObject.SetActive(false);
            Calculator.transform.Find("InputFields").gameObject.SetActive(false);
        }
        else if (value == 1)
        {
            Instrument.transform.Find("buttons").gameObject.SetActive(true);
            Calculator.transform.Find("InputFields").gameObject.SetActive(false);
        }
        else if (value == 2)
        {
            Calculator.transform.Find("InputFields").gameObject.SetActive(true);
            Instrument.transform.Find("buttons").gameObject.SetActive(false);
            
        }
    }
}

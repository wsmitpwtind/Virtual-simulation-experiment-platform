using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorManager : MonoBehaviour
{
    public Indicator Indicator1;
    public Indicator Indicator2;
    public Indicator Indicator3;

    // Start is called before the first frame update
    void Start()
    {
        Indicator1 = GameObject.Find("Indicator").GetComponent<Indicator>();
        Indicator2 = GameObject.Find("Indicator2").GetComponent<Indicator>();
        Indicator3 = GameObject.Find("Indicator3").GetComponent<Indicator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HideAllIndicator()
    {
        Indicator1.HideIndicate();
        Indicator2.HideIndicate();
        Indicator3.HideIndicate();
    }
}

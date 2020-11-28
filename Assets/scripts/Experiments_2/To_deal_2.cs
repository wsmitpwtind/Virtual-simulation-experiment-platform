using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class To_deal_2 : MonoBehaviour
{
    private GameObject Experiment2;
    // Start is called before the first frame update
    void Start()
    {
        Experiment2 = GameObject.Find("Experiment2");
        GetComponent<Button>().onClick.AddListener(end);
    }

    // Update is called once per frame
    void end()
    {
        if (Exp_1.Experiment1_measure[3]>0)
        {
            Experiment2.transform.Find("InputFields1").gameObject.SetActive(false);
            Experiment2.transform.Find("Deal_1").gameObject.SetActive(true);
        }
        
    }
}

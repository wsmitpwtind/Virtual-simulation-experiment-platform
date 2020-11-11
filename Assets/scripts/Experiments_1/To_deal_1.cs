using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class To_deal_1 : MonoBehaviour
{
    private GameObject Experiment1;
    // Start is called before the first frame update
    void Start()
    {
        Experiment1 = GameObject.Find("Experiment1");
        GetComponent<Button>().onClick.AddListener(end);
    }

    // Update is called once per frame
    void end()
    {
        if (Exp_1.Experiment1_measure[3]>0)
        {
            Experiment1.transform.Find("InputFields1").gameObject.SetActive(false);
            Experiment1.transform.Find("Deal_1").gameObject.SetActive(true);
        }
        
    }
}

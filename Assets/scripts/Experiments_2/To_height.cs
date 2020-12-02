using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class To_height : MonoBehaviour
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
        if (Exp_2.Experiment2_width[4] > 0)
        {
            Experiment2.transform.Find("InputFields2_2").gameObject.SetActive(false);
            Experiment2.transform.Find("InputFields2_3").gameObject.SetActive(true);
        }

    }
}

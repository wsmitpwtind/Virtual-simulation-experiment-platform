using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class To_result_1 : MonoBehaviour
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
        if (Exp_1.A_thickness > 0&& Exp_1.B_thickness > 0)
        {
            Exp_1.A_real_thickness = StaticMethods.Uncertain_A(Exp_1.Experiment1_measure);

            Experiment1.transform.Find("Result_1").gameObject.SetActive(true);
            Experiment1.transform.Find("Deal_1").gameObject.SetActive(false);
        }
    }
}

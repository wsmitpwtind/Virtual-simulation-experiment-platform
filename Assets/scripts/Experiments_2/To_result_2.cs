using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class To_result_2 : MonoBehaviour
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
        /*if (Exp_2.A_thickness > 0&& Exp_2.B_thickness > 0)
        {
            Exp_2.A_real_thickness = StaticMethods.Uncertain_A(Exp_2.Experiment2_measure);

            Experiment2.transform.Find("Result_2").gameObject.SetActive(true);
            Experiment2.transform.Find("Deal_2").gameObject.SetActive(false);            
        }*/
        if (Exp_2.A_User > 0 && Exp_2.UV_User > 0 && Exp_2.B_User > 0 && Exp_2.UB_User > 0 && Exp_2.C_User > 0 && Exp_2.UC_User > 0 && Exp_2.V_User > 0 && Exp_2.UV_User > 0)
        {
            Menu.Exp2_pagenumber = 4;
            Exp_2.state.Value = 5;
            Experiment2.transform.Find("Result_2").gameObject.SetActive(true);
            Experiment2.transform.Find("Deal_2").gameObject.SetActive(false);
        }
    }
}

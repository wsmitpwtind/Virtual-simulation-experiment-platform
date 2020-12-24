using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class To_width : MonoBehaviour
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
        if (Exp_2.Experiment2_length[4] > 0)
        {
            Menu.Exp2_pagenumber = 1;
            Experiment2.transform.Find("InputFields2_1").gameObject.SetActive(false);
            Experiment2.transform.Find("InputFields2_2").gameObject.SetActive(true);
        }

    }
}

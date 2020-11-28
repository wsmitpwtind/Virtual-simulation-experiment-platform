using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    FadeAnimate AniInstance = null;
    private GameObject Instrument;
    private GameObject Experiment1;
    private GameObject Experiment2;
    // Start is called before the first frame update
    void Start()
    {
        Instrument = GameObject.Find("Instrument");
        Experiment1 = GameObject.Find("Experiment1");
        Experiment2 = GameObject.Find("Experiment2");
        GameObject.Find("Dropdown").GetComponent<Dropdown>().onValueChanged.AddListener(menu);
        AniInstance = GameObject.Find("Canvas").GetComponent<FadeAnimate>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void menu(int value)
    {
        if (value == 0)
        {
//<<<<<<< HEAD
            Instrument.transform.Find("Buttons").gameObject.SetActive(false);
            Experiment1.transform.Find("InputFields1").gameObject.SetActive(false);
            Experiment1.transform.Find("Deal_1").gameObject.SetActive(false);
            Experiment1.transform.Find("Result_1").gameObject.SetActive(false);
//=======
            AniInstance.Hide(Instrument.transform.Find("Buttons").gameObject, 200);
            AniInstance.Hide(Experiment1.transform.Find("InputFields1").gameObject, 200);
            // Instrument.transform.Find("Buttons").gameObject.SetActive(false);
            // Experiment1.transform.Find("InputFields1").gameObject.SetActive(false);
//>>>>>>> a84ca237cdbd5c52d0357209827cf26456287f7b
        }
        else if (value == 1)
        {
            AniInstance.Show(Instrument.transform.Find("Buttons").gameObject, 200);
            AniInstance.Hide(Experiment1.transform.Find("InputFields1").gameObject, 200);
            // Instrument.transform.Find("Buttons").gameObject.SetActive(true);
            // Experiment1.transform.Find("InputFields1").gameObject.SetActive(false);
        }
        else if (value == 2)
        {
            AniInstance.Hide(Instrument.transform.Find("Buttons").gameObject, 200);
            AniInstance.Show(Experiment1.transform.Find("InputFields1").gameObject, 200);
            // Experiment1.transform.Find("InputFields1").gameObject.SetActive(true);
            // Instrument.transform.Find("Buttons").gameObject.SetActive(false);
        }
    }
}

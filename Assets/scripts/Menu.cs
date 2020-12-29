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
    public static int Exp1_pagenumber = 0;
    public static int Exp2_pagenumber = 0;
    private GameObject[] Exp1_page = new GameObject[5];
    private GameObject[] Exp2_page = new GameObject[5];

    // Start is called before the first frame update
    void Start()
    {
        //初始化数据
        Exp1_pagenumber = Manager.record.Exp1_pagenumber;
        Exp2_pagenumber = Manager.record.Exp2_pagenumber;


        Instrument = GameObject.Find("Instrument");
        Experiment1 = GameObject.Find("Experiment1");
        Experiment2 = GameObject.Find("Experiment2");
        GameObject.Find("Dropdown").GetComponent<Dropdown>().onValueChanged.AddListener(menu);
        AniInstance = GameObject.Find("Canvas").GetComponent<FadeAnimate>();


        //确定实验数据记录到哪里
        Exp1_page[0] = Experiment1.transform.Find("InputFields1").gameObject;
        Exp1_page[1] = Experiment1.transform.Find("Deal_1").gameObject;
        Exp1_page[2] = Experiment1.transform.Find("Result_1").gameObject;
        Exp2_page[0] = Experiment2.transform.Find("InputFields2_1").gameObject;
        Exp2_page[1] = Experiment2.transform.Find("InputFields2_2").gameObject;
        Exp2_page[2] = Experiment2.transform.Find("InputFields2_3").gameObject;
        Exp2_page[3] = Experiment2.transform.Find("Deal_2").gameObject;
        Exp2_page[4] = Experiment2.transform.Find("Result_2").gameObject;


    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void menu(int value)
    {
        if (value == 0)
        {

            Instrument.transform.Find("Buttons").gameObject.SetActive(false);
            Exp1_page[0].SetActive(false);
            Exp1_page[1].SetActive(false);
            Exp1_page[2].SetActive(false);
            Exp2_page[0].SetActive(false);
            Exp2_page[1].SetActive(false);
            Exp2_page[2].SetActive(false);
            Exp2_page[3].SetActive(false);
            Exp2_page[4].SetActive(false);


            /*AniInstance.Hide(Instrument.transform.Find("Buttons").gameObject, 200);
            AniInstance.Hide(Exp1_page[0], 200);
            AniInstance.Hide(Exp1_page[1], 200);
            AniInstance.Hide(Exp1_page[2], 200);
            AniInstance.Hide(Exp2_page[0], 200);
            AniInstance.Hide(Exp2_page[1], 200);
            AniInstance.Hide(Exp2_page[2], 200);
            AniInstance.Hide(Exp2_page[3], 200);
            AniInstance.Hide(Exp2_page[4], 200);*/



            // Instrument.transform.Find("Buttons").gameObject.SetActive(false);
            // Experiment1.transform.Find("InputFields1").gameObject.SetActive(false);

        }
        else if (value == 1)
        {
            if (Manager.state.Value == 1)
            {
                Manager.state.Value = 2;
            }


            AniInstance.Show(Instrument.transform.Find("Buttons").gameObject, 200);
            //AniInstance.Hide(Exp1_page[Exp1_pagenumber], 200);
            Exp1_page[Exp1_pagenumber].SetActive(false);
            //AniInstance.Hide(Exp2_page[Exp2_pagenumber], 200);
            Exp2_page[Exp2_pagenumber].SetActive(false);
            // Instrument.transform.Find("Buttons").gameObject.SetActive(true);
            // Experiment1.transform.Find("InputFields1").gameObject.SetActive(false);
        }
        else if (value == 2)
        {
            AniInstance.Hide(Instrument.transform.Find("Buttons").gameObject, 200);
            Instrument.transform.Find("Buttons").gameObject.SetActive(false);
            //AniInstance.Show(Exp1_page[Exp1_pagenumber], 200);
            Exp1_page[Exp1_pagenumber].SetActive(true);
            //AniInstance.Hide(Exp2_page[Exp2_pagenumber], 200);
            Exp2_page[Exp2_pagenumber].SetActive(false);
            // Experiment1.transform.Find("InputFields1").gameObject.SetActive(true);
            // Instrument.transform.Find("Buttons").gameObject.SetActive(false);
        }
        else if (value == 3)
        {
            if (Exp_2.state.Value == 0)
            {
                Exp_2.state.Value = 1;
            }


            AniInstance.Hide(Instrument.transform.Find("Buttons").gameObject, 200);
            Instrument.transform.Find("Buttons").gameObject.SetActive(false);
            //AniInstance.Hide(Exp1_page[Exp1_pagenumber], 200);
            Exp1_page[Exp1_pagenumber].SetActive(false);
            //AniInstance.Show(Exp2_page[Exp2_pagenumber], 200);
            Exp2_page[Exp2_pagenumber].SetActive(true);
            // Experiment1.transform.Find("InputFields1").gameObject.SetActive(true);
            // Instrument.transform.Find("Buttons").gameObject.SetActive(false);
        }
    }
}

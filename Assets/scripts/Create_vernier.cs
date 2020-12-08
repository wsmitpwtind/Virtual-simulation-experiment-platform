using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Create_vernier : MonoBehaviour
{
    public GameObject My_vernier;
    private GameObject Now_vernier;
    private GameObject Now_vernier1;
    private GameObject Now_vernier2;
    private Vector3 Vv1;
    private Vector3 Vv2;
    Vector3 position = new Vector3(0, 2f, -2.5f);
    Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, 0));
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(Vernier);
        Now_vernier = GameObject.Find("Vernier");
        Now_vernier1 = GameObject.Find("MeasureBody");
        Now_vernier2 = GameObject.Find("MeasureHead");
        Vv1 = Now_vernier1.transform.position- Now_vernier.transform.position;
        Vv2 = Now_vernier2.transform.position- Now_vernier.transform.position;
        print(Vv1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Vernier()
    {
        Now_vernier = GameObject.Find("Vernier");
        BoxCollider Bc = Now_vernier.GetComponent<BoxCollider>();
        GameObject V1 = GameObject.Find("Vernier");
        Move_vernier v1=V1.GetComponent<Move_vernier>();
        v1.move1 = true;
        GameObject V2 = GameObject.Find("Vernier");
        Move_vernier v2 = V2.GetComponent<Move_vernier>();
        v2.move2 = true;
        if (Now_vernier.transform.position.x-9f>0)
        {
            Now_vernier.transform.position = position;
            GameObject.Find("Canvas3").GetComponent<Indicator>().ShowIndicate("P", "进行夹持");

        }
        else
        {
            Now_vernier.transform.position= new Vector3(10.2f, 2f, -2.5f);
            Now_vernier1.transform.position = Vv1+ Now_vernier.transform.position;
            Now_vernier2.transform.position = Vv2+ Now_vernier.transform.position;
            GameObject.Find("Canvas3").GetComponent<Indicator>().HideIndicate();


        }

        GameObject.Find("Dropdown").GetComponent<Dropdown>().value = 0;
    }
}

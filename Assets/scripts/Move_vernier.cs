using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Move_vernier : MonoBehaviour
{
    private float distance = 0.5f;
    public bool move1 = true;
    public bool move2 = true;
    private Vector3 Temp = new Vector3(0.0f, 0.0f, 0.0f); 
    // Start is called before the first frame update

    void Start()
    {
        
    }
   
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.P))
        {
            //GameObject V1 = GameObject.Find("MeasureBody");
            //V1.AddComponent<BoxCollider>();
            //BoxCollider bc1 = V1.GetComponent<BoxCollider>();
            //bc1.isTrigger = true;
            //GameObject V2 = GameObject.Find("MeasureHead");
            //V2.AddComponent<BoxCollider>();
            //BoxCollider bc2 = V2.GetComponent<BoxCollider>();
            //bc2.isTrigger = true;
            Code();
        }
    }
    void Code()
    {
        //print("qwqw");
        GameObject V1 = GameObject.Find("Real_Vernier");
        GameObject V2 = GameObject.Find("MeasureHead");
        if (move1 && move2)
        {
            V1.transform.position += 0.002f * (V2.transform.position - V1.transform.position).normalized;
            V2.transform.position -= 0.004f * (V2.transform.position - V1.transform.position).normalized;
        }
        else if (move1 && !move2)
        {
            Temp = V2.transform.position;
            V1.transform.position += 0.002f * (V2.transform.position - V1.transform.position).normalized;
            V2.transform.position = Temp;
        }
        else if (!move1 && move2)
        {
            V2.transform.position -= 0.002f * (V2.transform.position - V1.transform.position).normalized;
        }
        if ((V1.transform.position - V2.transform.position).magnitude <= 0.003f)
        {
            V2.transform.position = V1.transform.position;
            move1 = false;
            move2 = false;
        }
    }
    
}

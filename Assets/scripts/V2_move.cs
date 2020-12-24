using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V2_move : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "book")
        {
            GameObject V2 = GameObject.Find("Real_Vernier");
            //print("2");
            V2.GetComponent<Move_vernier>().move2 = false;
        }
       
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

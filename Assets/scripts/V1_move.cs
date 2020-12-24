using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V1_move : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        print(other.name);
        if (other.tag== "book")
        {
            GameObject V1 = GameObject.Find("Real_Vernier");
            print(other.name);
            V1.GetComponent<Move_vernier>().move1=false;
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

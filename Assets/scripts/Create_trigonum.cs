using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Create_trigonum : MonoBehaviour
{
    public GameObject My_trigonum;
    private GameObject Now_trigonum;
    Vector3 position = new Vector3(0, 1.94f, -3f);
    Quaternion rotation = Quaternion.Euler(new Vector3(-90f, 0, 0));
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(Trigonum);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Trigonum()
    {
        Now_trigonum = GameObject.Find("ruler");
        if (Now_trigonum.transform.position.x - 9f > 0)
        {
            Now_trigonum.transform.position = position;
        }
        else
        {
            Now_trigonum.transform.position = new Vector3(13.2f, 2f, -2.5f);
           
        }

        GameObject.Find("Dropdown").GetComponent<Dropdown>().value = 0;
    }
}

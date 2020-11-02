using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Create_trigonum : MonoBehaviour
{
    public GameObject My_trigonum;
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
        GameObject trigonum = GameObject.Instantiate(My_trigonum, position, rotation) as GameObject;
    }
}

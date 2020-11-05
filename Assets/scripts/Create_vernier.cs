using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Create_vernier : MonoBehaviour
{
    public GameObject My_vernier;
    Vector3 position = new Vector3(0, 2f, -2.5f);
    Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, 0));
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(Vernier);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Vernier()
    {
        GameObject trigonum = GameObject.Instantiate(My_vernier, position, rotation) as GameObject;
        trigonum.transform.localScale = new Vector3(2, 2, 2);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Create_vernier : MonoBehaviour
{
    public GameObject My_vernier;
    private GameObject Now_vernier;
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
        Now_vernier = GameObject.Find("Vernier(Clone)");

        if (Now_vernier == null)
        {
            GameObject trigonum = GameObject.Instantiate(My_vernier, position, rotation) as GameObject;
            trigonum.transform.localScale = new Vector3(2, 2, 2);
            trigonum.AddComponent<Move_book>();
            trigonum.AddComponent<MoveRuler>();
            trigonum.AddComponent<Rigidbody>();
        }
        else
        {
            Destroy(Now_vernier);
        }

        GameObject.Find("Dropdown").GetComponent<Dropdown>().value = 0;
    }
}

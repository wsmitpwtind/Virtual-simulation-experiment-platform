using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shut_explain : MonoBehaviour
{
    private GameObject Explain;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(Shut);
        Explain = GameObject.Find("Explain");
    }

    // Update is called once per frame
    void Shut()
    {
        // Explain.SetActive(false);
        GameObject.Find("Canvas").GetComponent<FadeAnimate>().Hide(Explain, 200);
    }

}

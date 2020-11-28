using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Open_explain : MonoBehaviour
{
    private GameObject Canvas;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(Open);
        Canvas = GameObject.Find("Canvas");
    }

    // Update is called once per frame
    void Open()
    {
        GameObject.Find("Canvas").GetComponent<FadeAnimate>().Show(Canvas.transform.Find("Explain").gameObject, 200);
        // Canvas.transform.Find("Explain").gameObject.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DimFixer : MonoBehaviour
{
    public Material dim;
    public Material transparent;
    public GameObject toMonitor;

    private bool isDim = true;
    void Start()
    {
        if (toMonitor == null)
        {
            toMonitor = GameObject.Find("Wwl");
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (toMonitor == null)
            return;
        if (toMonitor.activeSelf)
        {
            if (isDim)
            {
                gameObject.GetComponent<Image>().material = transparent;
                isDim = false;
            }
        }
        else if (!isDim)
        {
            gameObject.GetComponent<Image>().material = dim;
            isDim = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Open_explain : MonoBehaviour
{
    public bool Show;
    private GameObject tips;
    // Start is called before the first frame update
    void Start()
    {
        var Canvas = GameObject.Find("Canvas");
        tips = Canvas.transform.Find("Explain").gameObject;
        tips.SetActive(true);
        tips.transform.localScale = new Vector3(0, 0, 0);
        GetComponent<Button>().onClick.AddListener(Open);
    }

    // Update is called once per frame
    void Open()
    {
        if (Show)
        {
            var animate = tips.transform.DOScale(new Vector3(1, 1, 1), .5f);
            animate.SetEase(Ease.OutExpo);
        }
        else
        {
            var animate = tips.transform.DOScale(new Vector3(0, 0, 0), .5f);
            animate.SetEase(Ease.InOutExpo);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SwitchPanel : MonoBehaviour
{
    public GameObject panel;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(SwitchPanelHandler);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SwitchPanelHandler()
    {
        if (panel.GetComponent<RectTransform>().localScale.x < 0.3)
            ShowPanel();
        else
            HidePanel();
    }
    void ShowPanel()
    {
        var animate = panel.GetComponent<RectTransform>().DOScale(1, 0.5f);
        animate.startValue = new Vector3(0, 0, 0);
        animate.SetEase(Ease.OutExpo);
    }
    void HidePanel()
    {
        var animate = panel.GetComponent<RectTransform>().DOScale(0, 0.5f);
        animate.startValue = new Vector3(1, 1, 1);
        animate.SetEase(Ease.InOutExpo);
    }
}
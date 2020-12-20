using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowC2 : MonoBehaviour
{
    private GameObject ControlObject = null;
    private System.Timers.Timer bigTimer = null;
    private System.Timers.Timer smallTimer = null;
    private Image DimImage = null;

    private int ChangeCount = 0;
    private readonly double step = 0.1, duration = 1000, DesOpacity = 4.5;

    private bool canChangeOpacity = false, canOpenControl = false;

    // Start is called before the first frame update
    private void Start()
    {
        ControlObject = GameObject.Find("InstructionObject");
        ControlObject.SetActive(false);
        DimImage = GameObject.Find("DimImage").GetComponent<Image>();
        DimImage.material.SetFloat("_Size", 0);
        bigTimer = new System.Timers.Timer();
        bigTimer.Interval = 1500;
        bigTimer.Elapsed += new System.Timers.ElapsedEventHandler(StartSmallTimer);
        bigTimer.Start();
    }

    private void StartSmallTimer(object sender, System.Timers.ElapsedEventArgs e)
    {
        bigTimer.Stop();
        smallTimer = new System.Timers.Timer();
        smallTimer.Interval = duration / (DesOpacity / step);
        smallTimer.Elapsed += new System.Timers.ElapsedEventHandler(OnSmallTimerEnter);
        smallTimer.Start();
    }

    private void OnSmallTimerEnter(object sender, System.Timers.ElapsedEventArgs e)
    {
        smallTimer.Stop();
        canChangeOpacity = true;
        if (ChangeCount * step > DesOpacity)
        {
            smallTimer.Stop();
            canChangeOpacity = false;
            canOpenControl = true;
            return;
        }
        ChangeCount++;
        smallTimer.Start();
    }

    // Update is called once per frame
    private void Update()
    {
        if (canChangeOpacity)
        {
            Material material = DimImage.material;
            material.SetFloat("_Size", (float)(ChangeCount * step + step));
            DimImage.material = material;
        }

        if (canOpenControl)
        {
            GameObject.Find("Canvas2").GetComponent<FadeAnimate>().Show(ControlObject);
            canOpenControl = false;
        }
    }
}

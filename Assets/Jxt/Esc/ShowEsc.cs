using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;
using DG.Tweening;

public class ShowEsc : MonoBehaviour
{
    public GameObject EscObject;
    private FadeAnimate AniInstance = null;
    private GameObject EscInstance = null;

    private bool canHandleEsc = true;
    private Timer timer = new Timer();
    private bool LastCameraEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        AniInstance = GameObject.Find("Canvas").GetComponent<FadeAnimate>();
        timer.Interval = 500;
        timer.Elapsed += Timer_Elapsed;
    }

    private void Timer_Elapsed(object sender, ElapsedEventArgs e)
    {
        timer.Stop();
        canHandleEsc = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canHandleEsc && Input.GetKey(KeyCode.Escape))
        {
            SwitchEscMenu();
            canHandleEsc = false;
            timer.Start();
        }
    }

    public void SwitchEscMenu()
    {
        EscInstance = GameObject.Find("Esc(Clone)");
        if (EscInstance == null)
        {
            var Position = new Vector3(0, 0, 0);
            var Rotation = new Quaternion(0, 0, 0, 0);
            EscInstance = Instantiate(EscObject, Position, Rotation);
            var animate = GameObject.Find("TweenObject").GetComponent<RectTransform>().DOLocalMoveY(0, 0.5f);
            animate.SetEase(Ease.OutExpo);
            animate.startValue = new Vector3(0, 400, 0);
            animate.SetUpdate(true);
            Time.timeScale = 0;
            try
            {
                if (GameObject.Find("MainCamera").GetComponent<Look>() != null)
                    LastCameraEnabled = GameObject.Find("MainCamera").GetComponent<Look>().enabled;
                GameObject.Find("MainCamera").GetComponent<Look>().enabled = false;
            }
            catch { }
        }
        else
        {
            var animate = GameObject.Find("TweenObject").GetComponent<RectTransform>().DOLocalMoveY(400, 0.5f);
            animate.SetEase(Ease.OutExpo);
            animate.startValue = new Vector3(0, 0, 0);
            animate.SetUpdate(true);
            Time.timeScale = 1;
            Invoke("Delay", 0.15f);
        }
    }

    void Delay()
    {
        Destroy(EscInstance);
        try
        {
            GameObject.Find("MainCamera").GetComponent<Look>().enabled = LastCameraEnabled;
        }
        catch { }
    }

    public void Restart()
    {
        Invoke("_Restart", 0.5f);
    }
    void _Restart()
    {
        SceneManager.LoadScene("Start");
    }
}

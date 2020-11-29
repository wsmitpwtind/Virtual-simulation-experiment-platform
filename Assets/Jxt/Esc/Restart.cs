using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Restart : MonoBehaviour
{
    FadeAnimate AniInstance = null;
    ShowEsc ShowEscInstance = null;
    RawImage Transition = null;

    void Start()
    {
        AniInstance = GameObject.Find("Canvas").GetComponent<FadeAnimate>();
        var tmp = GameObject.Find("Canvas").GetComponentsInChildren<RawImage>(true);
        if (tmp.Length != 0)
            Transition = tmp[tmp.Length - 1];
        ShowEscInstance = GameObject.Find("Canvas").GetComponent<ShowEsc>();
        GetComponent<Button>().onClick.AddListener(RestartGame);
    }

    void RestartGame()
    {
        ShowEscInstance.SwitchEscMenu();
        Debug.Log(AniInstance == null);
        Debug.Log(Transition == null);
        if (AniInstance != null && Transition != null)
        {
            AniInstance.Show(new List<Graphic>() { Transition }, 400);
            Invoke(nameof(Delay), (float)0.5);
        }
        else
            Delay();
    }

    void Delay()
    {
        SceneManager.LoadScene("Start");
    }
}

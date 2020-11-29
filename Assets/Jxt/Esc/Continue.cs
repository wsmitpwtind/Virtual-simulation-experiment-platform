using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Continue : MonoBehaviour
{
    FadeAnimate AniInstance = null;
    ShowEsc ShowEscInstance = null;

    void Start()
    {
        AniInstance = GameObject.Find("Canvas").GetComponent<FadeAnimate>();
        ShowEscInstance = GameObject.Find("Canvas").GetComponent<ShowEsc>();
        GetComponent<Button>().onClick.AddListener(RestartGame);
    }

    void RestartGame()
    {
        ShowEscInstance.SwitchEscMenu();
    }
}

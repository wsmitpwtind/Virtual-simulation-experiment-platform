using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Transitions : MonoBehaviour
{
    FadeAnimate AniInstance = null;
    GameObject Transition = null;

    private void Awake()
    {
        var tmp = gameObject.GetComponentsInChildren<RawImage>(true);
        Transition = tmp[tmp.Length - 1].gameObject;
        Transition.SetActive(true);
        AniInstance = GameObject.Find("Canvas").GetComponent<FadeAnimate>();
        AniInstance.Hide(Transition, 400);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

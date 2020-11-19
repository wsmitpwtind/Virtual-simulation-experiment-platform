using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class To_test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(Totest);
    }

    // Update is called once per frame
    void Totest()
    {
        SceneManager.LoadScene("Pre_test");
    }
}

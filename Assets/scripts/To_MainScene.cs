using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class To_MainScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(Tomainscene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Tomainscene()
    {
        
        SceneManager.LoadScene("MainScene");
    }
}

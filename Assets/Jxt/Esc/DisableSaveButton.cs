using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class DisableSaveButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var sceneName = SceneManager.GetActiveScene().name;
        if ("MainScene".Equals(sceneName) || "Bug".Equals(sceneName))
        {
            gameObject.GetComponent<Button>().interactable = true;
        }
        else
        {
            gameObject.GetComponent<Button>().interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ContinueButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (RecordManager.RecordContains(RecordManager.currentRecordId))
        {
            GetComponent<Button>().onClick.AddListener(conti);
        }
        else
        {
            GetComponent<Image>().color = new Color(50, 50, 50);
            GetComponent<Button>().interactable = false;
        }
    }
    void conti()
    {
        Manager.UpdateRecord();
        SceneManager.LoadScene("MainScene");
    }
}

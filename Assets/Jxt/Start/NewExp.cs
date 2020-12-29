using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewExp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(setRecord);
    }
    void setRecord()
    {
        RecordManager.currentRecordId = RecordManager.GetFirstNone();
        Manager.UpdateRecord();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class RecordScript : MonoBehaviour
{
    public Text _title;
    public Text _time;
    public int recordId { get; set; }
    public string title
    {
        get => _title.text;
        set => _title.text = value;
    }
    public string time
    {
        get => _time.text;
        set => _time.text = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (var item in GetComponentsInChildren<Button>(true))
        {
            if (item.gameObject.name.Equals("LoadRecordButton"))
                item.onClick.AddListener(LoadRecord);
            if (item.gameObject.name.Equals("DeleteRecordButton"))
                item.onClick.AddListener(DeleteRecord);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetRecordInfo(RecordInfo recordInfo)
    {
        this.recordId = recordInfo.recordId;
        this.time = recordInfo.timeString;
        this.title = recordInfo.title;
    }

    void LoadRecord()
    {
        RecordManager.currentRecordId = recordId;
        Time.timeScale = 1;
        SceneManager.LoadScene("MainScene");
    }

    void DeleteRecord()
    {
        RecordManager.DeleteRecord(recordId);
    }
}

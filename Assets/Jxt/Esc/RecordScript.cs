using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class RecordScript : MonoBehaviour
{
    private Text _title = null;
    private Text _time = null;
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
        foreach (var item in GetComponentsInChildren<Text>(true))
        {
            if (item.gameObject.name.Equals("RecordTitle"))
                _title = item;
            if (item.gameObject.name.Equals("RecordTime"))
                _time = item;
        }
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
    void SetRecordInfo(RecordInfo recordInfo)
    {
        this.recordId = recordInfo.recordId;
        this.time = recordInfo.timeString;
        this.title = recordInfo.title;
    }

    void LoadRecord()
    {
        throw new NotImplementedException();
    }

    void DeleteRecord()
    {
        GetComponentInParent<RecordManager>().DeleteRecord(recordId);
    }
}

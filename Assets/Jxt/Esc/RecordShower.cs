using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordShower : MonoBehaviour
{
    public GameObject RecordObj;
    public RecordManager manager;
    private List<RecordScript> showedRecords = new List<RecordScript>();
    void Start()
    {
        manager.onRecordUpdate += RefreshRecord;
        RefreshRecord();
    }
    private void RefreshRecord()
    {
        foreach (var item in showedRecords)
            Destroy(item.gameObject);
        showedRecords.Clear();
        var list = manager.records;
        foreach (var item in list)
        {
            var recordScript = Instantiate<GameObject>(RecordObj, gameObject.transform).GetComponent<RecordScript>();
            recordScript.SetRecordInfo(item);
            recordScript.recordManager = manager;
            showedRecords.Add(recordScript);
        }
    }
}

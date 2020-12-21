using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RecordManager : MonoBehaviour
{
    public GameObject RecordObj;
    public delegate void RecordUpdate();
    public event RecordUpdate onRecordUpdate;
    private List<RecordScript> showedRecords = new List<RecordScript>();
    public List<RecordInfo> records
    {
        get => GetRecords();
    }
    public Record this[int Id]
    {
        get => GetRecord(Id);
        set => SaveRecord(value);
    }
    // Start is called before the first frame update
    void Start()
    {
        onRecordUpdate += ShowRecord;
        ShowRecord();
    }
    private void ShowRecord()
    {
        foreach (var item in showedRecords)
            Destroy(item.gameObject);
        showedRecords.Clear();
        var list = GetRecords();
        foreach (var item in list)
        {
            var recordScript = Instantiate<GameObject>(RecordObj, gameObject.transform).GetComponent<RecordScript>();
            recordScript.SetRecordInfo(item);
            recordScript.recordManager = this;
            showedRecords.Add(recordScript);
        }
    }
    public void SaveRecord(Record record)
    {
        var list = Storage.CommonStorage.GetStorage<List<RecordInfo>>("RecordInfo");
        int index = list.FindIndex(x => record.recordInfo.recordId.Equals(x.recordId));
        Storage recordStorage = new Storage(record.recordInfo.recordId);
        if (index == -1)
            list.Add(record.recordInfo);
        else
            list[index] = record.recordInfo;
        recordStorage.SetStorage("defaultRecord", record);
        Storage.CommonStorage.SetStorage("RecordInfo", list);
        onRecordUpdate();
    }
    public List<RecordInfo> GetRecords()
    {
        return Storage.CommonStorage.GetStorage<List<RecordInfo>>("RecordInfo");
    }
    public Record GetRecord(int Id)
    {
        var list = Storage.CommonStorage.GetStorage<List<RecordInfo>>("RecordInfo");
        var item = list.Find(x => Id.Equals(x.recordId));
        if (item == null)
            return null;
        Storage recordStorage = new Storage(Id);
        return recordStorage.GetStorage<Record>("defaultRecord");
    }
    public void DeleteRecord(int Id)
    {
        var list = Storage.CommonStorage.GetStorage<List<RecordInfo>>("RecordInfo");
        int index = list.FindIndex(x => Id.Equals(x.recordId));
        if (index != -1)
            list.RemoveAt(index);
        Storage.CommonStorage.SetStorage("RecordInfo", list);
        onRecordUpdate();
    }
    public int GetFirstNone()
    {
        var records = this.records;
        for (int i = 1; ; i++)
        {
            if (records.FindIndex(x => i.Equals(x.recordId)) == -1)
                return i;
        }
    }
}

public class RecordInfo
{
    public int recordId { get; set; }
    public string title { get; set; }
    public DateTime time { get; set; }
    public string timeString { get => time.ToLocalTime().ToString("F"); }
}

public class Record
{
    public RecordInfo recordInfo { get; set; }
    public int someValue { get; set; }

    public RecordInfo GenRecordInfo(int id, string title)
    {
        return new RecordInfo()
        {
            recordId = id,
            title = title,
            time = DateTime.Now
        };
    }
}
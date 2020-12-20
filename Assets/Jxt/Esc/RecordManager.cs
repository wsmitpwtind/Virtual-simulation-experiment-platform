using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RecordManager : MonoBehaviour
{
    public GameObject RecordObj;
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

    }
    public void SaveRecord(Record record)
    {
        throw new NotImplementedException();
    }
    public List<RecordInfo> GetRecords()
    {
        throw new NotImplementedException();
    }
    public Record GetRecord(int Id)
    {
        throw new NotImplementedException();
    }
    public void DeleteRecord(int Id)
    {
        throw new NotImplementedException();
    }
}

public class RecordInfo
{
    public int recordId { get; set; }
    public string title { get; set; }
    public DateTime time { get; set; }
    public string timeString { get => time.ToString("U"); }
}

public class Record
{
    public RecordInfo recordInfo { get; set; }
    public int someValue { get; set; }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/// <summary>
/// 游戏存档管理器
/// 不同档位的存档由存档ID来区分
/// 每个存档ID包含一个默认存档，命名为"defaultRecord"
/// 每个存档ID还可包含其它附件
/// </summary>
public class RecordManager : MonoBehaviour
{
    public delegate void RecordUpdate();
    /// <summary>
    /// 存档信息有更新时触发
    /// </summary>
    public event RecordUpdate onRecordUpdate;
    /// <summary>
    /// 存档信息列表
    /// </summary>
    public List<RecordInfo> records
    {
        get => GetRecords();
    }
    /// <summary>
    /// 获取默认存档
    /// </summary>
    /// <param name="Id">存档ID</param>
    public Record this[int Id]
    {
        get => GetRecord(Id);
        set => SaveRecord(value);
    }
    /// <summary>
    /// 保存由存档管理器收集默认存档内容
    /// </summary>
    /// <param name="recordInfo">存档信息</param>
    public void SaveRecord(RecordInfo recordInfo)
    {
        SaveRecord(CollectRecordData(recordInfo));
    }
    /// <summary>
    /// 保存已收集好的默认存档内容
    /// </summary>
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
    /// <summary>
    /// 获取存档信息列表
    public List<RecordInfo> GetRecords()
    {
        return Storage.CommonStorage.GetStorage<List<RecordInfo>>("RecordInfo");
    }
    /// <summary>
    /// 获取默认存档
    /// </summary>
    /// <param name="Id">存档ID</param>
    public Record GetRecord(int Id)
    {
        var list = Storage.CommonStorage.GetStorage<List<RecordInfo>>("RecordInfo");
        var item = list.Find(x => Id.Equals(x.recordId));
        if (item == null)
            return null;
        Storage recordStorage = new Storage(Id);
        return recordStorage.GetStorage<Record>("defaultRecord");
    }
    /// <summary>
    /// 将存档从存档信息中删除 不会实际删除文件
    /// </summary>
    /// <param name="Id">存档ID</param>
    public void DeleteRecord(int Id)
    {
        var list = Storage.CommonStorage.GetStorage<List<RecordInfo>>("RecordInfo");
        int index = list.FindIndex(x => Id.Equals(x.recordId));
        if (index != -1)
            list.RemoveAt(index);
        Storage.CommonStorage.SetStorage("RecordInfo", list);
        onRecordUpdate();
    }
    /// <summary>
    /// 获取第一个没有存档的ID
    /// </summary>
    public int GetFirstNone()
    {
        var records = this.records;
        for (int i = 1; ; i++)
        {
            if (records.FindIndex(x => i.Equals(x.recordId)) == -1)
                return i;
        }
    }
    /// <summary>
    /// 获取存档其他附件
    /// </summary>
    /// <typeparam name="T">数据模型类</typeparam>
    /// <param name="Id">存档ID</param>
    /// <param name="name">附件名称</param>
    public T GetAttachments<T>(int Id, string name) where T : new()
    {
        Storage recordStorage = new Storage(Id);
        return recordStorage.GetStorage<T>(name);
    }
    /// <summary>
    /// 保存其他附件
    /// </summary>
    /// <typeparam name="T">数据模型类</typeparam>
    /// <param name="Id">存档ID</param>
    /// <param name="name">附件名称</param>
    /// <param name="values">内容</param>
    public void SetAttachments<T>(int Id, string name, T values) where T : new()
    {
        Storage recordStorage = new Storage(Id);
        recordStorage.SetStorage(name, values);
    }
    /// <summary>
    /// 管理器内置默认存档数据收集器
    /// </summary>
    /// <param name="recordInfo">存档信息</param>
    private Record CollectRecordData(RecordInfo recordInfo)
    {
        var record = new Record { recordInfo = recordInfo };
        // 在此处收集存档信息
        return record;
    }
}

/// <summary>
/// 存档信息模型类
/// 用来管理所有存档
/// </summary>
public class RecordInfo
{
    public int recordId { get; set; }
    public string title { get; set; }
    public DateTime time { get; set; }
    public string timeString { get => time.ToLocalTime().ToString("F"); }
}

/// <summary>
/// 默认存档数据模型类
/// </summary>
public class Record
{
    public RecordInfo recordInfo { get; set; }
    public int someValue { get; set; }

    public static RecordInfo GenRecordInfo(int id, string title)
    {
        return new RecordInfo()
        {
            recordId = id,
            title = title,
            time = DateTime.Now
        };
    }
}
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
public static class RecordManager
{
    /// <summary>
    /// 存档信息有更新时触发
    /// </summary>
    public static event RecordUpdate onRecordUpdate;
    /// <summary>
    /// 存档信息列表
    /// </summary>
    public static List<RecordInfo> recordInfos
    {
        get => GetRecordInfos();
    }
    /// <summary>
    /// 获取默认存档
    /// </summary>
    /// <param name="Id">存档ID</param>
    public static RecordIndexor defaultRecord
    {
        get => new RecordIndexor();
    }
    /// <summary>
    /// 当前的存档Id
    /// </summary>
    public static int currentRecordId
    {
        get
        {
            if (_currentRecordId == -1)
                _currentRecordId = Storage.CommonStorage.GetStorage<HH>("currentRecordId").currentRecordId;
            return _currentRecordId;
        }
        set
        {
            _currentRecordId = value;
            Storage.CommonStorage.SetStorage("currentRecordId", new HH() { currentRecordId = value });
        }
    }
    public static Record currentDefaultRecord
    {
        get => GetRecord(currentRecordId);
    }
    public delegate void RecordUpdate();
    private static int _currentRecordId = -1;
    /// <summary>
    /// 保存由存档管理器收集默认存档内容
    /// </summary>
    /// <param name="recordInfo">存档信息</param>
    public static void SaveRecord(RecordInfo recordInfo)
    {
        SaveRecord(recordInfo, CollectRecordData(recordInfo));
    }
    /// <summary>
    /// 保存已收集好的默认存档内容
    /// </summary>
    public static void SaveRecord(RecordInfo recordInfo, Record record)
    {
        if (recordInfo == null)
            recordInfo = Record.GenRecordInfo(GetFirstNone());
        if (string.IsNullOrEmpty(recordInfo.title))
            recordInfo = Record.GenRecordInfo(recordInfo.recordId);
        var list = Storage.CommonStorage.GetStorage<List<RecordInfo>>("RecordInfo");
        int index = list.FindIndex(x => recordInfo.recordId.Equals(x.recordId));
        Storage recordStorage = new Storage(recordInfo.recordId);
        if (index == -1)
            list.Add(recordInfo);
        else
            list[index] = recordInfo;
        recordStorage.SetStorage("defaultRecord", record);
        Storage.CommonStorage.SetStorage("RecordInfo", list);
        onRecordUpdate();
    }
    /// <summary>
    /// 获取存档信息列表
    public static List<RecordInfo> GetRecordInfos()
    {
        return Storage.CommonStorage.GetStorage<List<RecordInfo>>("RecordInfo");
    }
    /// <summary>
    /// 获取存档信息列表
    public static RecordInfo GetRecordInfo(int Id)
    {
        return Storage.CommonStorage.GetStorage<List<RecordInfo>>("RecordInfo").Find(x => Id.Equals(x.recordId));
    }
    /// <summary>
    /// 获取默认存档
    /// </summary>
    /// <param name="Id">存档ID</param>
    public static Record GetRecord(int Id)
    {
        if (!RecordContains(Id))
            return null;
        Storage recordStorage = new Storage(Id);
        return recordStorage.GetStorage<Record>("defaultRecord");
    }
    /// <summary>
    /// 将存档从存档信息中删除 不会实际删除文件
    /// </summary>
    /// <param name="Id">存档ID</param>
    public static void DeleteRecord(int Id)
    {
        var list = Storage.CommonStorage.GetStorage<List<RecordInfo>>("RecordInfo");
        int index = list.FindIndex(x => Id.Equals(x.recordId));
        if (index != -1)
            list.RemoveAt(index);
        Storage.CommonStorage.SetStorage("RecordInfo", list);
        Storage storage = new Storage(Id);
        storage.DeleteStorage();
        onRecordUpdate();
    }
    /// <summary>
    /// 获取第一个没有存档的ID
    /// </summary>
    public static int GetFirstNone()
    {
        var _records = recordInfos;
        for (int i = 1; ; i++)
        {
            if (_records.FindIndex(x => i.Equals(x.recordId)) == -1)
                return i;
        }
    }
    /// <summary>
    /// 获取存档其他附件
    /// </summary>
    /// <typeparam name="T">数据模型类</typeparam>
    /// <param name="Id">存档ID</param>
    /// <param name="name">附件名称</param>
    public static T GetAttachments<T>(int Id, string name) where T : new()
    {
        if (!RecordContains(Id))
            return default(T);
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
    public static void SetAttachments<T>(int Id, string name, T values) where T : new()
    {
        Storage recordStorage = new Storage(Id);
        recordStorage.SetStorage(name, values);
    }
    /// <summary>
    /// 管理器内置默认存档数据收集器
    /// </summary>
    /// <param name="recordInfo">存档信息</param>
    private static Record CollectRecordData(RecordInfo recordInfo)
    {
        var record = new Record();
        // 在此处收集存档信息
        record.Manager_state = Manager.state.Value;
        record.Exp1_state = Exp_1.state.Value;
        record.Exp2_state = Exp_2.state.Value;
        record.Exp2_length = Exp_2.Experiment2_length;
        record.Exp2_width = Exp_2.Experiment2_width;
        record.Exp2_height = Exp_2.Experiment2_height;
        record.ifFirst0_ = Wwlplay.ifFirst0_;
        record.ifFirst1_ = Wwlplay.ifFirst1_;
        record.ifFirst2_ = Wwlplay.ifFirst2_;
        record.i_0 = Wwlplay.i_0;
        record.i_1 = Wwlplay.i_1;
        record.i_2 = Wwlplay.i_2;
        record.Exp1_pagenumber = Menu.Exp1_pagenumber;
        record.Exp2_pagenumber = Menu.Exp2_pagenumber;

        return record;
    }
    /// <summary>
    /// 是否存在指定ID的存档
    /// </summary>
    /// <param name="Id">存档ID</param>
    public static bool RecordContains(int Id)
    {
        var list = Storage.CommonStorage.GetStorage<List<RecordInfo>>("RecordInfo");
        int index = list.FindIndex(x => Id.Equals(x.recordId));
        if (index == -1)
            return false;
        return true;
    }
    public class RecordIndexor
    {
        public Record this[int Id]
        {
            get => GetRecord(Id);
            set => SaveRecord(GetRecordInfo(Id), value);
        }
    }
    private class HH
    {
        public int currentRecordId { get; set; }
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
    //状态
    public int someValue { get; set; }
    public int Manager_state { get; set; }
    public int Exp1_state { get; set; }
    public int Exp2_state { get; set; }
    //实验二
    public double[] Exp2_length { get; set; } = new double[100];
    public double[] Exp2_width { get; set; } = new double[100];
    public double[] Exp2_height { get; set; } = new double[100];
    public double Exp2_A_User { get; set; }
    public double Exp2_UA_User { get; set; }
    public double Exp2_A_Real { get; set; }
    public double Exp2_B_User { get; set; }
    public double Exp2_UB_User { get; set; }
    public double Exp2_B_Real { get; set; }
    public double Exp2_C_User { get; set; }
    public double Exp2_UC_User { get; set; }
    public double Exp2_C_Real { get; set; }
    public double Exp2_V_User { get; set; }
    public double Exp2_UV_User { get; set; }
    public double Exp2_V_Real { get; set; }
    //wwl视频
    public bool[] ifFirst0_ { get; set; } = { true, true, true };
    public bool[] ifFirst1_ { get; set; } = { true, true, true };
    public bool[] ifFirst2_ { get; set; } = { true, true, true };
    public int i_0 { get; set; } = 0;
    public int i_1 { get; set; } = -1;
    public int i_2 { get; set; } = -1;
    //数据记录位置
    public int Exp1_pagenumber { get; set; } = 0;
    public int Exp2_pagenumber { get; set; } = 0;


    public static RecordInfo GenRecordInfo(int id, string title)
    {
        return new RecordInfo()
        {
            recordId = id,
            title = title,
            time = DateTime.Now
        };
    }

    public static RecordInfo GenRecordInfo(int id)
    {
        return GenRecordInfo(id, $"存档 {id}");
    }
}
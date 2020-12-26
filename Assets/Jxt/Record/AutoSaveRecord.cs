using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSaveRecord : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int curId = RecordManager.GetFirstNone();
        RecordManager.currentRecordId = curId;
        RecordManager.SaveRecord(Record.GenRecordInfo(curId, $"自动保存的 {curId}"));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSaveRecord : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RecordManager.SaveRecord(Record.GenRecordInfo(RecordManager.currentRecordId, $"自动保存的 {RecordManager.currentRecordId}"));
    }
}

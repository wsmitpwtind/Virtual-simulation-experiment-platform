using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordShower : MonoBehaviour
{
    public GameObject RecordObj;
    public GameObject EmptyObj;
    private GameObject emptyObj;
    private List<RecordScript> showedRecords = new List<RecordScript>();
    void Start()
    {
        RecordManager.onRecordUpdate += RefreshRecord;
        RefreshRecord();
    }
    private void RefreshRecord()
    {
        foreach (var item in showedRecords)
            Destroy(item.gameObject);
        showedRecords.Clear();
        if (emptyObj != null)
            Destroy(emptyObj);
        emptyObj = null;
        var list = RecordManager.recordInfos;
        if (list.Count == 0)
            emptyObj = Instantiate<GameObject>(EmptyObj, gameObject.transform);
        else
            foreach (var item in list)
            {
                var recordScript = Instantiate<GameObject>(RecordObj, gameObject.transform).GetComponent<RecordScript>();
                recordScript.SetRecordInfo(item);
                showedRecords.Add(recordScript);
            }
    }

    void OnDestroy()
    {
        RecordManager.onRecordUpdate -= RefreshRecord;
    }
}

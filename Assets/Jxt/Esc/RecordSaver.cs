using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordSaver : MonoBehaviour
{
    public GameObject panel;
    public SwitchPanel switchPanel;
    Dropdown dropdown = null;
    InputField inputField = null;
    Dictionary<int, int> recordIdTable = new Dictionary<int, int>();
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(SaveRecordHandler);
        dropdown = panel.GetComponentInChildren<Dropdown>();
        inputField = panel.GetComponentInChildren<InputField>();
        RecordManager.onRecordUpdate += SetRecordInfo;
        SetRecordInfo();
    }
    void SetRecordInfo()
    {
        dropdown.ClearOptions();
        recordIdTable.Clear();
        var options = new List<Dropdown.OptionData>();
        options.Add(new Dropdown.OptionData("保存到新的存档..."));
        recordIdTable.Add(0, RecordManager.GetFirstNone());
        var records = RecordManager.recordInfos;
        for (int i = 0; i < records.Count; i++)
        {
            options.Add(new Dropdown.OptionData($"{records[i].title}\t保存于{records[i].timeString}"));
            recordIdTable.Add(i + 1, records[i].recordId);
        }
        dropdown.options = options;
        dropdown.GetComponentInChildren<ScrollRect>(true).gameObject.SetActive(false);
        dropdown.onValueChanged.AddListener(DropdownValueChanged);
    }
    void DropdownValueChanged(int value)
    {
        if (value == 0)
            return;
        inputField.text = RecordManager.recordInfos.Find(x => recordIdTable[value].Equals(x.recordId)).title;
    }
    void SaveRecordHandler()
    {
        var name = inputField.text.Trim();
        int id = recordIdTable[dropdown.value];
        var recordInfo = Record.GenRecordInfo(id, string.IsNullOrEmpty(name) ? $"存档 {id}" : name);
        RecordManager.SaveRecord(recordInfo);
        switchPanel.SwitchPanelHandler();
    }
    void OnDestroy()
    {
        RecordManager.onRecordUpdate -= SetRecordInfo;
    }
}

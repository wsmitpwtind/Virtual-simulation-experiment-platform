using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordSaver : MonoBehaviour
{
    public GameObject panel;
    public RecordManager manager;
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
        manager.onRecordUpdate += SetRecordInfo;
        SetRecordInfo();
    }
    void SetRecordInfo()
    {
        dropdown.ClearOptions();
        recordIdTable.Clear();
        var options = new List<Dropdown.OptionData>();
        options.Add(new Dropdown.OptionData("保存到新的存档..."));
        recordIdTable.Add(0, manager.GetFirstNone());
        var records = manager.records;
        for (int i = 0; i < records.Count; i++)
        {
            options.Add(new Dropdown.OptionData($"{records[i].title}\t保存于{records[i].timeString}"));
            recordIdTable.Add(i + 1, records[i].recordId);
        }
        dropdown.options = options;
        dropdown.GetComponentInChildren<ScrollRect>(true).gameObject.SetActive(false);
        dropdown.onValueChanged.AddListener(DropdownValueChanged);
    }
    // Update is called once per frame
    void Update()
    {

    }

    void DropdownValueChanged(int value)
    {
        if (value == 0)
            return;
        inputField.text = manager.records.Find(x => recordIdTable[value].Equals(x.recordId)).title;
    }

    void SaveRecordHandler()
    {
        var record = new Record();
        var name = inputField.text.Trim();
        record.recordInfo = record.GenRecordInfo(recordIdTable[dropdown.value], string.IsNullOrEmpty(name) ? "存档" : name);
        manager.SaveRecord(record);
        switchPanel.SwitchPanelHandler();
    }
}

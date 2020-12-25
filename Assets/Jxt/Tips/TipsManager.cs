using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipsManager : MonoBehaviour
{
    [SerializeField]
    private TipsScript tipsComponent;
    [SerializeField]
    [Header("Tips预置类型")]
    private TipsClass tipsClass;
    [SerializeField]
    [TextArea]
    [Header("文本")]
    private string text;
    private List<string> tipsText = new List<string>()
    {
        "",
        "欢迎来到绪论实验虚拟实验室！\n1.要开始实验，请先走到椅子旁，并按【E】开始实验。\n2.开始实验后，您随时可以按【Q】或【ESC】结束实验。\n3.未保存的进度将不会被自动保存。",
        "本实验的任务测量书本的体积。\n1.测量所需仪器在左上角的菜单栏中。\n2.每次测量完毕后，您需要自行记录您的数据。\n3.全部测量完毕后，请在左上角菜单栏中提交您的测量数据，并计算不确定度，给出最终的结果表达。我们将对您的结果进行评价。",
        "您已退出本次实验，如需重新开始请走到椅子旁按【E】，如退出请走到门口按提示操作。"
    };
    public enum TipsClass
    {
        [InspectorName("采用输入的文本")]
        Input,
        [InspectorName("刚刚进入实验")]
        AtStart,
        [InspectorName("刚刚开始实验")]
        AtEntryExperiment,
        [InspectorName("刚刚结束实验")]
        AtQuitExperiment
    }
    // Start is called before the first frame update
    void OnEnable()
    {
        if (tipsClass == TipsClass.Input)
            tipsComponent.ShowTips(text);
        else
            tipsComponent.ShowTips(tipsText[(int)tipsClass]);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnDisable()
    {
        if (tipsComponent != null)
            tipsComponent.HideTips();
    }
}

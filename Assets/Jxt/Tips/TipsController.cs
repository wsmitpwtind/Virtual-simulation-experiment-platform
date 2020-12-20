using System;
using System.Dynamic;
using System.Reflection;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TipsController : MonoBehaviour
{
    private bool shown = false;
    private bool hideTaskHangUp = false;
    private string messageToShow = "";
    // Start is called before the first frame update
    void Start()
    {
        messageToShow = Tips.AtStart;
        CancelInvoke();
        Invoke("ShowTipsAuto", 1);
        Invoke("HideTipsAuto", 11);
    }
    int state_1 = 0;

    // Update is called once per frame
    void Update()
    {
        if (Exp_1.state_1 == 1 && state_1 == 0)
        {
            HideTips();
            messageToShow = Tips.AtEntranceOfExp;
            CancelInvoke();
            Invoke("ShowTipsAuto", 1);
            Invoke("HideTipsAuto", 11);
        } else if(Exp_1.state_1 == 0 && state_1 == 1){
            HideTips();
            messageToShow = Tips.AtExitOfExp;
            CancelInvoke();
            Invoke("ShowTipsAuto", 1);
            Invoke("HideTipsAuto", 6);
        }
        state_1 = Exp_1.state_1;
    }

    private void ShowTipsAuto()
    {
        ShowTips(messageToShow);
        hideTaskHangUp = true;
    }
    private void HideTipsAuto()
    {
        if (hideTaskHangUp)
            HideTips();
    }

    public void ShowTips(string message)
    {
        if (shown)
            HideTips();
        gameObject.GetComponentInChildren<Text>(true).text = message;
        var animate = gameObject.GetComponent<RectTransform>().DOLocalMoveX(250, .5f);
        animate.SetEase(Ease.OutExpo);
        shown = true;
        hideTaskHangUp = false;
    }

    public void HideTips()
    {
        var animate = gameObject.GetComponent<RectTransform>().DOLocalMoveX(530, .5f);
        animate.SetEase(Ease.OutExpo);
        shown = false;
    }
    private static class Tips
    {
        internal static string AtStart = "欢迎来到绪论实验虚拟实验室！\n1.要开始实验，请先走到椅子旁，并按【E】开始实验。\n2.开始实验后，您随时可以按【Q】或【ESC】结束实验。\n3.未保存的进度将不会被自动保存。";
        internal static string AtEntranceOfExp = "本实验的任务测量书本的体积。\n1.测量所需仪器在左上角的菜单栏中。\n2.每次测量完毕后，您需要自行记录您的数据。\n3.全部测量完毕后，请在左上角菜单栏中提交您的测量数据，并计算不确定度，给出最终的结果表达。我们将对您的结果进行评价。";
        internal static string AtExitOfExp = "您已退出本次实验，如需重新开始请走到椅子旁按【E】，如退出请走到门口按提示操作。";
    }
}

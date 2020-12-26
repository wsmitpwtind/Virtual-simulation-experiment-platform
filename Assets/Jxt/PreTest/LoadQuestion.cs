using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// 测试题目渲染程序
/// </summary>
public class LoadQuestion : MonoBehaviour
{
    #region 私有字段
    /// <summary>
    /// 当前已展示题目个数
    /// </summary>
    private int cnt = 0;
    /// <summary>
    /// 功能页面对象，用于展示/隐藏页面
    /// </summary>
    private List<GameObject> pages = new List<GameObject>();
    /// <summary>
    /// 功能页面抽象模型
    /// </summary>
    private List<ITestPage> testPages = new List<ITestPage>();
    /// <summary>
    /// 未作答提示对象
    /// </summary>
    private GameObject inputEmpty = null;

    private FadeAnimate aniInstant = null;
    private QuestionManager questionManager = null;
    private IEnumerator<Question> enumerator = null;
    private ITestPage curTestPage = null;
    private Button nextButton = null;
    private Text keyLabelRed = null;
    private Text keyLabelGreen = null;
    #endregion

    // Start is called before the first frame update
    private void Start()
    {
        #region 获取Unity GameObject 和 Components
        inputEmpty = GameObject.Find("InputEmpty");
        aniInstant = GetComponent<FadeAnimate>();
        nextButton = GameObject.Find("NextButton").GetComponent<Button>();
        keyLabelRed = GameObject.Find("KeyLabelRed").GetComponent<Text>();
        keyLabelGreen = GameObject.Find("KeyLabelGreen").GetComponent<Text>();
        pages.Add(GameObject.Find("TextPage"));
        pages.Add(GameObject.Find("RadioPage"));
        pages.Add(GameObject.Find("CheckboxPage"));
        pages.Add(GameObject.Find("ScorePage"));
        testPages.Add(new TextTestPage(GameObject.Find("TextTitle").GetComponent<Text>(),
            GameObject.Find("TextQuestion").GetComponent<Text>(), GameObject.Find("TextInput").GetComponent<InputField>()));
        testPages.Add(new RadioTestPage(GameObject.Find("RadioTitle").GetComponent<Text>(),
            GameObject.Find("RadioQuestion").GetComponent<Text>(), GameObject.Find("ToggleGroup").GetComponent<ToggleGroup>()));
        testPages.Add(new CheckboxTestPage(GameObject.Find("CheckboxTitle").GetComponent<Text>(),
            GameObject.Find("CheckboxQuestion").GetComponent<Text>(), new List<Toggle>() {
                GameObject.Find("CheckboxToggle1").GetComponent<Toggle>(),
                GameObject.Find("CheckboxToggle2").GetComponent<Toggle>(),
                GameObject.Find("CheckboxToggle3").GetComponent<Toggle>(),
                GameObject.Find("CheckboxToggle4").GetComponent<Toggle>(),
                GameObject.Find("CheckboxToggle5").GetComponent<Toggle>()
            }));
        questionManager = GetComponent<QuestionManager>();
        #endregion

        #region 设置GameObject为不可见
        foreach (var item in pages)
            item.SetActive(false);
        nextButton.gameObject.SetActive(false);
        keyLabelRed.gameObject.SetActive(false);
        keyLabelGreen.gameObject.SetActive(false);
        #endregion
    }

    /// <summary>
    /// QuestionManager加载题库完成后回调方法
    /// </summary>
    public void BeginCallback()
    {
        var questions = questionManager.GetQuestions(10);
        enumerator = questions.GetEnumerator();
        enumerator.MoveNext();
        LoadOne(enumerator.Current);
    }

    // Update is called once per frame
    private void Update()
    {

    }

    /// <summary>
    /// 显示题目到屏幕
    /// </summary>
    /// <param name="question">题目</param>
    private void LoadOne(Question question)
    {
        cnt++;
        foreach (var item in pages)
            item.SetActive(false);
        pages[(int)question.type - 1].SetActive(true);
        ITestPage testPage = testPages[(int)question.type - 1];
        curTestPage = testPage;
        testPage.SetQuestion(question, cnt);
    }

    /// <summary>
    /// 显示最终得分
    /// </summary>
    private void ShowScore()
    {
        foreach (var item in pages)
            item.SetActive(false);
        pages[3].SetActive(true);
        GameObject.Find("Pre_Test_Score").GetComponent<Text>().text += $"{questionManager.score}";
    }

    /// <summary>
    /// 下一页按钮的监听器
    /// </summary>
    public void NextListener()
    {
        if (enumerator.MoveNext())
            LoadOne(enumerator.Current);
        else
            ShowScore();
        nextButton.gameObject.SetActive(false);
        keyLabelRed.gameObject.SetActive(false);
        keyLabelGreen.gameObject.SetActive(false);
    }

    /// <summary>
    /// 提交按钮的监听器
    /// </summary>
    public void SubmitListener()
    {
        string answer = curTestPage.GetAnswer();
        if (answer.Trim().Length == 0)
        {
            DOTween.Clear();
            float yyy = inputEmpty.GetComponent<RectTransform>().localPosition.y;
            Debug.Log(yyy);
            if (yyy < 200)
                yyy = 400;
            inputEmpty.GetComponent<RectTransform>().DOLocalMoveY(150, 0.5f).SetEase(Ease.OutExpo);
            inputEmpty.GetComponent<RectTransform>().DOLocalMoveY(yyy, 0.5f).SetEase(Ease.OutExpo).SetDelay(2);
        }
        else
        {
            bool isRight = questionManager.SubmitQuestion(enumerator.Current, answer);
            nextButton.gameObject.SetActive(true);
            // 显示正确答案
            if (isRight)
            {
                keyLabelGreen.gameObject.SetActive(true);
                keyLabelGreen.text = $"正确答案：{string.Join(" | ", enumerator.Current.key)}";
            }
            else
            {
                keyLabelRed.gameObject.SetActive(true);
                keyLabelRed.text = $"正确答案：{string.Join(" | ", enumerator.Current.key)}";
            }
        }
    }
}
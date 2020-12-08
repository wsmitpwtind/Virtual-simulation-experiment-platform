using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 小测页面抽象模型接口
/// </summary>
public interface ITestPage
{
    /// <summary>
    /// 获取用户答案
    /// </summary>
    /// <returns>答案</returns>
    string GetAnswer();

    /// <summary>
    /// 设置题目到屏幕
    /// </summary>
    /// <param name="question">题目</param>
    /// <param name="count">第几道题目</param>
    void SetQuestion(Question question, int count);
}

/// <summary>
/// 小测页面的基类
/// </summary>
public class TestPage : ITestPage
{
    /// <summary>
    /// 小测页面的标题
    /// </summary>
    protected internal Text title { get; set; }
    /// <summary>
    /// 小测页面的题干
    /// </summary>
    protected internal Text question { get; set; }

    /// <summary>
    /// 基础构造函数
    /// </summary>
    /// <param name="title">标题组件</param>
    /// <param name="question">题干组件</param>
    public TestPage(Text title, Text question)
    {
        this.title = title;
        this.question = question;
    }

    public virtual void SetQuestion(Question question, int count)
    {
        string[] reflector = { "填空", "单选", "多选" };
        title.text = $"课前测试 第{count}题 {reflector[(int)question.type - 1]}";
        this.question.text = $"{count}. {question.question}";
    }

    public virtual string GetAnswer()
    {
        return null;
    }
}

/// <summary>
/// 填空类小测页面
/// </summary>
public class TextTestPage : TestPage
{
    private InputField input { get; }

    public TextTestPage(Text title, Text question, InputField input) : base(title, question)
    {
        this.input = input;
    }

    public override string GetAnswer()
    {
        return input.text;
    }
}

/// <summary>
/// 单选类小测页面
/// </summary>
public class RadioTestPage : TestPage
{
    /// <summary>
    /// 单选框
    /// </summary>
    private SortedList<string, Toggle> toggles = new SortedList<string, Toggle>();
    /// <summary>
    /// 单选框的组
    /// </summary>
    private ToggleGroup toggleGroup { get; }

    public RadioTestPage(Text title, Text question, ToggleGroup toggleGroup) : base(title, question)
    {
        this.toggleGroup = toggleGroup;
        foreach (var item in toggleGroup.GetComponentsInChildren<Toggle>(true))
        {
            toggles.Add(item.gameObject.name, item);
            item.isOn = false;
        }
    }

    public override void SetQuestion(Question question, int count)
    {
        base.SetQuestion(question, count);
        toggleGroup.SetAllTogglesOff();
        foreach (var item in toggles.Values)
            item.gameObject.SetActive(false);
        for (int i = 0; i < question.options.Count; i++)
        {
            toggles.Values[i].gameObject.SetActive(true);
            toggles.Values[i].GetComponentInChildren<Text>(true).text = $"{(char)(65 + i)}. {question.options[i]}";
        }
    }

    public override string GetAnswer()
    {
        foreach (var item in toggles.Values)
        {
            if (item.isOn && item.IsActive())
                return item.GetComponentInChildren<Text>().text[0].ToString();
        }
        return "";
    }
}

/// <summary>
/// 多选类小测页面
/// </summary>
public class CheckboxTestPage : TestPage
{
    /// <summary>
    /// 多选框
    /// </summary>
    private List<Toggle> toggles { get; } = new List<Toggle>();

    public CheckboxTestPage(Text title, Text question, List<Toggle> toggles) : base(title, question)
    {
        for (int i = toggles.Count - 1; i >= 0; i--)
        {
            toggles[i].gameObject.SetActive(false);
            toggles[i].isOn = false;
        }
        this.toggles = toggles;
    }

    public override void SetQuestion(Question question, int count)
    {
        base.SetQuestion(question, count);
        for (int i = toggles.Count - 1; i >= 0; i--)
        {
            toggles[i].gameObject.SetActive(false);
            toggles[i].isOn = false;
        }
        for (int i = 0; i < question.options.Count; i++)
        {
            var tmp = $"{(char)(65 + i)}. {question.options[i]}";
            toggles[i].GetComponentInChildren<Text>().text = tmp;
            toggles[i].gameObject.SetActive(true);
        }
    }

    public override string GetAnswer()
    {
        List<char> ret = new List<char>();
        foreach (var item in toggles)
        {
            if (item.isOn && item.IsActive())
                ret.Add(item.GetComponentInChildren<Text>().text[0]);
        }
        return string.Join("|", ret);
    }
}
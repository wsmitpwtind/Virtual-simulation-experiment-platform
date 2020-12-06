using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadQuestion : MonoBehaviour
{
    public Toggle toggle;

    private int cnt = 0;
    private QuestionManager QM = null;
    private IEnumerable<Question> questions = null;
    private IEnumerator<Question> enumerator = null;
    public List<GameObject> pages = new List<GameObject>();
    private List<ITestPage> testPages = new List<ITestPage>();
    private ITestPage curTestPage = null;


    // Start is called before the first frame update
    private void Start()
    {
        pages.Add(GameObject.Find("TextPage"));
        pages.Add(GameObject.Find("RadioPage"));
        pages.Add(GameObject.Find("CheckboxPage"));
        pages.Add(GameObject.Find("ScorePage"));
        testPages.Add(new TextTestPage(GameObject.Find("TextTitle").GetComponent<Text>(),
            GameObject.Find("TextQuestion").GetComponent<Text>(), GameObject.Find("TextInput").GetComponent<InputField>()));
        testPages.Add(new RadioTestPage(GameObject.Find("RadioTitle").GetComponent<Text>(),
            GameObject.Find("RadioQuestion").GetComponent<Text>(), GameObject.Find("RadioDropdown").GetComponent<Dropdown>()));
        testPages.Add(new CheckboxTestPage(GameObject.Find("CheckboxTitle").GetComponent<Text>(),
            GameObject.Find("CheckboxQuestion").GetComponent<Text>()));
        foreach (var item in pages)
            item.SetActive(false);
        QM = GetComponent<QuestionManager>();
    }

    public void BeginCallback()
    {
        questions = QM.GetQuestions(10);
        enumerator = questions.GetEnumerator();
        enumerator.MoveNext();
        LoadOne(enumerator.Current);
    }

    // Update is called once per frame
    private void Update()
    {

    }

    private void LoadOne(Question question)
    {
        cnt++;
        ITestPage testPage = ShowPage(question.type);
        curTestPage = testPage;
        testPage.SetQuestion(question.question, cnt);
        testPage.SetOptions(question.options);
    }

    private void ShowScore()
    {
        foreach (var item in pages)
            item.SetActive(false);
        pages[3].SetActive(true);
        GameObject.Find("Pre_Test_Score").GetComponent<Text>().text += QM.score;
    }

    private ITestPage ShowPage(Question.QuestionType type)
    {
        foreach (var item in pages)
            item.SetActive(false);
        pages[(int)type - 1].SetActive(true);
        return testPages[(int)type - 1];
    }

    public void NextListener()
    {
        QM.SubmitQuestion(enumerator.Current, curTestPage.GetAnswer());
        if (enumerator.MoveNext())
            LoadOne(enumerator.Current);
        else
            ShowScore();
    }
}

public interface ITestPage
{
    string GetAnswer();
    void SetQuestion(string question, int count);
    void SetOptions(List<string> options);
}

public class TestPage : ITestPage
{
    protected internal Text title { get; set; }
    protected internal Text question { get; set; }

    public TestPage(Text title, Text question)
    {
        this.title = title;
        this.question = question;
    }

    public virtual string GetAnswer()
    {
        return null;
    }

    public virtual void SetOptions(List<string> options)
    {
        return;
    }

    public void SetQuestion(string question, int count)
    {
        title.text = $"课前测试 第{count}题";
        this.question.text = $"{count}. {question}";
    }
}

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

public class RadioTestPage : TestPage
{
    private Dropdown input { get; }

    public RadioTestPage(Text title, Text question, Dropdown input) : base(title, question)
    {
        this.input = input;
    }

    public override void SetOptions(List<string> options)
    {
        input.ClearOptions();
        List<string> list = new List<string>();
        for (int i = 0; i < options.Count; i++)
            list.Add($"{(char)(65 + i)}. {options[i]}");
        input.AddOptions(list);
    }

    public override string GetAnswer()
    {
        return ((char)(input.value + 65)).ToString();
    }
}

public class CheckboxTestPage : TestPage
{
    private List<Toggle> toggles { get; } = new List<Toggle>();

    public CheckboxTestPage(Text title, Text question) : base(title, question)
    {

    }

    public override void SetOptions(List<string> options)
    {
        for (int i = toggles.Count - 1; i >= 0; i--)
        {
            GameObject.Destroy(toggles[i].gameObject);
            toggles.RemoveAt(i);
        }
        Toggle ToggleObj = GameObject.Find("Canvas").GetComponent<LoadQuestion>().toggle;
        for (int i = 0; i < options.Count; i++)
        {
            var tmp = $"{(char)(65 + i)}. {options[i]}";
            var position = new Vector3(0, 15 - i * 30, 0);
            var quaternion = new Quaternion(0, 0, 0, 0);
            Toggle toggle = GameObject.Instantiate<Toggle>(ToggleObj, position, quaternion);
            toggle.transform.SetParent(GameObject.Find("Canvas").GetComponent<LoadQuestion>().pages[2].transform);
            toggle.transform.localPosition = position;
            toggle.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 680);
            toggle.GetComponentInChildren<Text>().text = tmp;
            toggles.Add(toggle);
        }
    }

    public override string GetAnswer()
    {
        List<char> ret = new List<char>();
        foreach (var item in toggles)
        {
            if (item.isOn)
                ret.Add(item.GetComponentInChildren<Text>().text[0]);
        }
        return string.Join("|", ret);
    }
}
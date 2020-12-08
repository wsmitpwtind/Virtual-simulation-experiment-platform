using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadQuestion : MonoBehaviour
{
    private int cnt = 0;
    private FadeAnimate aniInstant = null;
    private QuestionManager questionManager = null;
    private IEnumerator<Question> enumerator = null;
    private List<GameObject> pages = new List<GameObject>();
    private List<ITestPage> testPages = new List<ITestPage>();
    private ITestPage curTestPage = null;
    private GameObject inputEmpty = null;
    private Button nextButton = null;
    private Text keyLabelRed = null;
    private Text keyLabelGreen = null;


    // Start is called before the first frame update
    private void Start()
    {
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
        foreach (var item in pages)
            item.SetActive(false);
        inputEmpty.SetActive(false);
        nextButton.gameObject.SetActive(false);
        keyLabelRed.gameObject.SetActive(false);
        keyLabelGreen.gameObject.SetActive(false);
        questionManager = GetComponent<QuestionManager>();
    }

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

    private void LoadOne(Question question)
    {
        cnt++;
        ITestPage testPage = ShowPage(question.type);
        curTestPage = testPage;
        testPage.SetQuestion(question, cnt);
    }

    private ITestPage ShowPage(Question.QuestionType type)
    {
        foreach (var item in pages)
            item.SetActive(false);
        pages[(int)type - 1].SetActive(true);
        return testPages[(int)type - 1];
    }

    private void ShowScore()
    {
        foreach (var item in pages)
            item.SetActive(false);
        pages[3].SetActive(true);
        GameObject.Find("Pre_Test_Score").GetComponent<Text>().text += $"{questionManager.score}";
    }

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

    public void SubmitListener()
    {
        string answer = curTestPage.GetAnswer();
        if (answer.Trim().Length == 0)
            aniInstant.Show(inputEmpty);
        else
        {
            bool isRight = questionManager.SubmitQuestion(enumerator.Current, answer);
            nextButton.gameObject.SetActive(true);
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
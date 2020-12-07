using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using Newtonsoft.Json;

public class QuestionManager : MonoBehaviour
{
    public double score = 0;
    public List<Question> right = new List<Question>();
    public List<Question> wrong = new List<Question>();
    private List<Question> allQuestions = null;

    // Start is called before the first frame update
    void Start()
    {
        allQuestions = JsonHelper.LoadJsonFromDataFile<List<Question>>(Application.dataPath + "/StreamingAssets/QuestionLibrary.json");
        GetComponent<LoadQuestion>().BeginCallback();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool SubmitQuestion(Question question, string answer)
    {
        if (question.Judge(answer))
        {
            score += 0.1;
            right.Add(question);
            return true;
        }
        else
        {
            wrong.Add(question);
            return false;
        }
    }

    public IEnumerable<Question> GetQuestions(int count)
    {
        try
        {
            if (count == allQuestions.Count)
            {
                Question[] questions = new Question[allQuestions.Count];
                allQuestions.CopyTo(questions);
                return questions;
            }
            else if (count > allQuestions.Count)
                return null;
            else
            {
                Question[] questions = new Question[count];
                List<int> cur = new List<int>();
                System.Random r = new System.Random();
                while (cur.Count < count)
                {
                    int tmp = r.Next(0, allQuestions.Count);
                    if (cur.Contains(tmp))
                        continue;
                    allQuestions.CopyTo(tmp, questions, cur.Count, 1);
                    cur.Add(tmp);
                }
                return questions;
            }
        }
        catch
        {
            return null;
        }
    }
}

public static class JsonHelper
{
    public static T LoadJsonFromDataFile<T>(string filePath) where T : class
    {
        if (!File.Exists(filePath))
        {
            Debug.LogError($"找不到文件{filePath}");
            return null;
        }

        using (StreamReader sr = new StreamReader(filePath))
        {
            if (sr == null)
                return null;
            string json = sr.ReadToEnd();
            if (json.Length > 0)
                return JsonConvert.DeserializeObject<T>(json);
            return null;
        }
    }
}

[Serializable]
public class Question
{
    public int id { get; set; }

    public QuestionType type { get; set; }

    public string question { get; set; }

    public List<string> options { get; set; }

    public List<string> key { get; set; }

    [Serializable]
    public enum QuestionType
    {
        Text = 1,
        Radio = 2,
        Mutiple = 3
    }

    public bool Judge(string answer)
    {
        answer = answer.Trim();
        if (type == QuestionType.Text)
        {
            foreach (var item in key)
                if (item.Equals(answer, StringComparison.OrdinalIgnoreCase))
                    return true;
            return false;
        }
        else if (type == QuestionType.Radio)
            return key[0].Equals(answer, StringComparison.OrdinalIgnoreCase);
        else
        {
            var ansarr = answer.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            if (ansarr.Length != key.Count)
                return false;
            int cnt = 0;
            foreach (string item in ansarr)
            {
                if (key.Contains(item))
                    cnt++;
                else
                    return false;
            }
            return true;
        }
    }
}
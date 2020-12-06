using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    public double score = 0;
    private List<Question> allQuestions = null;

    // Start is called before the first frame update
    void Start()
    {
        allQuestions = JsonHelper.LoadJsonFromDataFile<List<Question>>("/StreamingAssets/QuestionLibrary.json");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerable<Question> GetQuestions(int count)
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
}

public class JsonHelper
{
    public static T LoadJsonFromDataFile<T>(string filePath) where T : class
    {
        if (!File.Exists(Application.dataPath + filePath))
        {
            Debug.LogError($"找不到文件{filePath}");
            return null;
        }

        using (StreamReader sr = new StreamReader(Application.dataPath + filePath))
        {
            if (sr == null)
                return null;
            string json = sr.ReadToEnd();
            if (json.Length > 0)
                return JsonUtility.FromJson<T>(json);
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

    public enum QuestionType
    {
        Text = 1,
        Radio = 2,
        Mutiple = 3
    }

    public bool Judge(string answer)
    {
        return answer.Equals(key);
    }
}
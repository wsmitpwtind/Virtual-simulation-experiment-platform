using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;


public class AnimateObject
{
    public Graphic gameObj { get; set; } = null;
    public bool canSetOpacity { get; set; } = false;
    public bool canSetActive { get; set; } = false;
    public bool active { get; set; } = false;
    public bool setted { get; set; } = false;
    public bool done { get; set; } = false;
    private double _opacity = 0;
    public double opacity
    {
        get
        {
            if (_opacity >= 0 && _opacity <= 1)
                return _opacity;
            else if (_opacity < 0)
                return 0;
            else
                return 1;
        }
        private set => _opacity = value;
    }

    private Timer timer = new Timer();
    private readonly double step = 0.01;
    private int ChangeCount = 0;

    public AnimateObject(Graphic gameObj, bool active, double duration)
    {
        this.gameObj = gameObj;
        this.active = active;
        if (active)
        {
            opacity = 0;
            canSetActive = true;
        }
        else
            opacity = 1;
        canSetOpacity = true;
        timer.Interval = duration * step;
        timer.Elapsed += Timer_Elapsed;
        timer.Start();
    }

    private void Timer_Elapsed(object sender, ElapsedEventArgs e)
    {
        timer.Stop();
        if (ChangeCount > 1 / step)
        {
            canSetActive = true;
            canSetOpacity = false;
            done = true;
            timer.Stop();
        }
        if (active)
            opacity = ChangeCount * step;
        else
            opacity = 1 - ChangeCount * step;
        ChangeCount++;
        timer.Start();
    }
}

public class FadeAnimate : MonoBehaviour
{
    List<AnimateObject> animateObjects = new List<AnimateObject>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = animateObjects.Count - 1; i >= 0; i--)
        {
            var item = animateObjects[i];
            if (item.done)
            {
                item.gameObj.gameObject.SetActive(item.active);
                animateObjects.RemoveAt(i);
                continue;
            }
            if (item.canSetOpacity && item.gameObj.name != "Mask")
            {
                Color color = item.gameObj.color;
                color.a = (float)item.opacity;
                item.gameObj.color = color;
            }
            if ((!item.setted) && item.canSetActive)
            { 
                item.gameObj.gameObject.SetActive(item.active);
                item.setted = true;
            }
        }
    }

    public void Show(IList<Graphic> gameObjs, double duration)
    {
        foreach (var item in gameObjs)
            animateObjects.Add(new AnimateObject(item, true, duration));
    }

    public void Show(IList<Graphic> gameObjs)
    {
        foreach (var item in gameObjs)
            animateObjects.Add(new AnimateObject(item, true, 500));
    }

    public void Show(GameObject gameObj)
    {
        gameObj.SetActive(true);
        List<Graphic> arr = new List<Graphic>();
        arr.AddRange(gameObj.GetComponentsInChildren<Graphic>(true));
        foreach (var item in arr)
            animateObjects.Add(new AnimateObject(item, true, 500));
    }

    public void Show(GameObject gameObj, double duration)
    {
        gameObj.SetActive(true);
        List<Graphic> arr = new List<Graphic>();
        arr.AddRange(gameObj.GetComponentsInChildren<Graphic>(true));
        foreach (var item in arr)
            animateObjects.Add(new AnimateObject(item, true, duration));
    }

    public void Hide(IList<Graphic> gameObjs, double duration)
    {
        foreach (var item in gameObjs)
            animateObjects.Add(new AnimateObject(item, false, duration));
    }

    public void Hide(IList<Graphic> gameObjs)
    {
        foreach (var item in gameObjs)
            animateObjects.Add(new AnimateObject(item, false, 500));
    }

    public void Hide(GameObject gameObj)
    {
        List<Graphic> arr = new List<Graphic>();
        arr.AddRange(gameObj.GetComponentsInChildren<Graphic>(true));
        foreach (var item in arr)
            animateObjects.Add(new AnimateObject(item, false, 500));
    }

    public void Hide(GameObject gameObj, double duration)
    {
        List<Graphic> arr = new List<Graphic>();
        arr.AddRange(gameObj.GetComponentsInChildren<Graphic>(true));
        foreach (var item in arr)
            animateObjects.Add(new AnimateObject(item, false, duration));
    }
}


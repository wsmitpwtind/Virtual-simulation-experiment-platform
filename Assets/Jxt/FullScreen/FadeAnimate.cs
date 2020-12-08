using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 渐显渐隐动画的实例对象类
/// </summary>
public class AnimateObject
{
    /// <summary>
    /// 托管的GameObject
    /// </summary>
    public Graphic gameObj { get; set; } = null;
    public bool canSetOpacity { get; set; } = false;
    public bool canSetActive { get; set; } = false;
    /// <summary>
    /// 已激活
    /// </summary>
    public bool active { get; set; } = false;
    /// <summary>
    /// 已设置过激活属性
    /// </summary>
    public bool setted { get; set; } = false;
    /// <summary>
    /// 动画已完成
    /// </summary>
    public bool done { get; set; } = false;
    /// <summary>
    /// 计时器
    /// </summary>
    private Timer timer = new Timer();
    /// <summary>
    /// 每次调节步长
    /// </summary>
    private readonly double step = 0.01;
    /// <summary>
    /// 已调整次数
    /// </summary>
    private int ChangeCount = 0;

    private double _opacity = 0;
    /// <summary>
    /// 透明度
    /// </summary>
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

    /// <summary>
    /// 启动动画实例
    /// </summary>
    /// <param name="gameObj">动画托管对象</param>
    /// <param name="active">Active属性的目标值</param>
    /// <param name="duration">动画持续时间</param>
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

/// <summary>
/// 渐显渐隐动画处理程序
/// </summary>
public class FadeAnimate : MonoBehaviour
{
    /// <summary>
    /// 正在播放动画的GameObject
    /// </summary>
    List<AnimateObject> animateObjects = new List<AnimateObject>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 循环设置透明度
        for (int i = animateObjects.Count - 1; i >= 0; i--)
        {
            var item = animateObjects[i];
            if (item.done)
            {
                item.gameObj.gameObject.SetActive(item.active);
                if (item.active && item.gameObj.name != "Mask")
                {
                    Color color = item.gameObj.color;
                    color.a = 1;
                    item.gameObj.color = color;
                }
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

    /// <summary>
    /// 渐显效果显示
    /// </summary>
    /// <param name="gameObj">要显示的对象</param>
    public void Show(GameObject gameObj)
    {
        gameObj.SetActive(true);
        List<Graphic> arr = new List<Graphic>();
        arr.AddRange(gameObj.GetComponentsInChildren<Graphic>(true));
        foreach (var item in arr)
            animateObjects.Add(new AnimateObject(item, true, 500));
    }

    /// <summary>
    /// 渐显效果显示
    /// </summary>
    /// <param name="gameObj">要显示的对象</param>
    /// <param name="duration">动画持续时间</param>
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

    /// <summary>
    /// 渐隐效果隐藏
    /// </summary>
    /// <param name="gameObj">要隐藏的对象</param>
    public void Hide(GameObject gameObj)
    {
        List<Graphic> arr = new List<Graphic>();
        arr.AddRange(gameObj.GetComponentsInChildren<Graphic>(true));
        foreach (var item in arr)
            animateObjects.Add(new AnimateObject(item, false, 500));
    }

    /// <summary>
    /// 渐隐效果隐藏
    /// </summary>
    /// <param name="gameObj">要隐藏的对象</param>
    /// <param name="duration">动画持续时间</param>
    public void Hide(GameObject gameObj, double duration)
    {
        List<Graphic> arr = new List<Graphic>();
        arr.AddRange(gameObj.GetComponentsInChildren<Graphic>(true));
        foreach (var item in arr)
            animateObjects.Add(new AnimateObject(item, false, duration));
    }
}


using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Move_vernier : MonoBehaviour
{
    private float distance = 0.5f;
    public bool move1 = true;
    public bool move2 = true;
    // Start is called before the first frame update

    void Start()
    {
        
    }
    void OnMouseDrag()
    {
        //获取到鼠标的位置(鼠标水平的输入和竖直的输入以及距离)
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        //物体的位置，屏幕坐标转换为世界坐标
        Vector3 objectPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        //把鼠标位置传给物体
        print("qwq");
        this.transform.position = objectPosition;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.P))
        {
            //GameObject V1 = GameObject.Find("MeasureBody");
            //V1.AddComponent<BoxCollider>();
            //BoxCollider bc1 = V1.GetComponent<BoxCollider>();
            //bc1.isTrigger = true;
            //GameObject V2 = GameObject.Find("MeasureHead");
            //V2.AddComponent<BoxCollider>();
            //BoxCollider bc2 = V2.GetComponent<BoxCollider>();
            //bc2.isTrigger = true;
            Code();
        }
    }
    void Code()
    {
        print("qwqw");
        GameObject V1 = GameObject.Find("MeasureBody");
        GameObject V2 = GameObject.Find("MeasureHead");
        if (move1)
        {
            V1.transform.position = new Vector3(V1.transform.position.x, V1.transform.position.y, V1.transform.position.z + 0.002f);
        }
        if (move2)
        {
        V2.transform.position = new Vector3(V2.transform.position.x, V2.transform.position.y, V2.transform.position.z - 0.002f);
        }
    }
    
}

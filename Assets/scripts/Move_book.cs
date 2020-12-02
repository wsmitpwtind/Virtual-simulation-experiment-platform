using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_book : MonoBehaviour
{
    private float distance = 0.5f;
    private Vector3 dist;
    private float posX;
    private float posY;
    private Vector3 AOTIScreen; //用来获取物体在屏幕的坐标
    private Vector3 MouseStartScreen; //用来获取鼠标按下时在屏幕的位置
    private Vector3 MouseNewScreen; //用来获取鼠标按下时每帧的位置
    private Vector3 valueScreen; //用来获取偏移量
    private float OffsetX = 0;
    private float OffsetY = 0;
    public float speed = 6f;//旋转速度
    private bool IsGet = false;
    private bool Ismove = false;


    private void OnMouseEnter()
    {
        IsGet = true;
    }
    private void OnMouseExit()
    {
        IsGet = false;
        if (!Input.GetMouseButton(1))
        {
            Ismove = false;
        }
    }
    private void Move()
    {
        //1.首先将物体从世界坐标转为屏幕坐标
        AOTIScreen = Camera.main.WorldToScreenPoint(transform.position);
        //2.获取鼠标在屏幕坐标的偏移量
        if (Input.GetMouseButtonDown(0))
        {
            MouseStartScreen = new Vector3(Input.mousePosition.x, Input.mousePosition.y, AOTIScreen.z);
        }
        MouseNewScreen = new Vector3(Input.mousePosition.x, Input.mousePosition.y, AOTIScreen.z);
        if (MouseNewScreen != MouseStartScreen)
        {
            valueScreen = MouseNewScreen - MouseStartScreen;
            //3.将偏移量给物体
            transform.position = Camera.main.ScreenToWorldPoint(AOTIScreen + valueScreen);
        }
        MouseStartScreen = MouseNewScreen;
    }

    void Update()
    {
        if (Input.GetMouseButton(1) && (IsGet || Ismove))
        {
            Ismove = true;
            OffsetX = Input.GetAxis("Mouse X");//获取鼠标x轴的偏移量
            OffsetY = Input.GetAxis("Mouse Y");//获取鼠标y轴的偏移量
            transform.Rotate(new Vector3(OffsetY, -OffsetX, 0) * speed, Space.World);
        }
        //if (!Input.GetMouseButton(1))
        //{
        //    if (!Input.GetMouseButton(0))
        //    {
        //        print(transform.rotation.x);
        //        if (transform.rotation.x > 150 && transform.rotation.x < 210)
        //        {
        //            print("okk");
        //            transform.rotation = Quaternion.Euler(new Vector3(180, transform.rotation.y, transform.rotation.z));
        //        }
        //    }
        //}
    }

    //private void OnMouseDown()
    //{
    //    dist = Camera.main.WorldToScreenPoint(transform.position);
    //    posX = Input.mousePosition.x - dist.x;
    //    posY = Input.mousePosition.y - dist.y;
    //}
    //private void OnMouseDrag()
    //{
    //    Vector3 curPos = new Vector3(Input.mousePosition.x - posX, Input.mousePosition.y - posY, dist.z);
    //    Vector3 worldPos = Camera.main.WorldToScreenPoint(curPos);
    //    transform.position = worldPos;
    //}

    void OnMouseDrag()
    {
        //获取到鼠标的位置(鼠标水平的输入和竖直的输入以及距离)
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        //物体的位置，屏幕坐标转换为世界坐标
        Vector3 objectPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        //把鼠标位置传给物体
        transform.position = objectPosition;
    }

}

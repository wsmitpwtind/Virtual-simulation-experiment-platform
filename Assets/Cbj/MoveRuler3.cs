using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;


public class MoveRuler3 : MonoBehaviour {
    // Start is called before the first frame update
    protected GameObject ruler1, ruler2, ruler2_head, ruler2_body;
    protected Rigidbody ruler1_rigid, ruler2_head_rigid, ruler2_body_rigid;
    protected Camera cam;
    protected int flag = 0, current = 0;
    protected static readonly Vector3[][] pos = new Vector3[][] {
        new Vector3[]{new Vector3(-3,19.649f,-4),new Vector3(-3,19.649f,-3),new Vector3(-3,19.649f,-2),new Vector3(-3,19.649f,-1),new Vector3(-3,19.649f,0),new Vector3(-3,19.649f,1) },
        new Vector3[]{new Vector3(-3,19.649f,-2),new Vector3(-2,19.649f,-2),new Vector3(-1,19.649f,-2),new Vector3(-0,19.649f,-2),new Vector3(1,19.649f,-2),new Vector3(2,19.649f,-2) },
    };
    protected static readonly Quaternion[] ruler1_rot = new Quaternion[] {
        Quaternion.Euler(-90,0,0),Quaternion.Euler(-90,0,90)
    };
    protected static readonly Vector3[] ruler2_pos = new Vector3[] {
        new Vector3(-6,0,0),new Vector3(-3,0,0)
    };
    protected static readonly Vector3[] book_pos = new Vector3[] {
        new Vector3(-2,23,0),new Vector3(-2,23,1),new Vector3(-2,23,2),new Vector3(-2,23,-1),new Vector3(-2,23,-2),new Vector3(-2,19,0)
    };
    protected static readonly Quaternion[] book_rot = new Quaternion[] {
        Quaternion.Euler(0,0,0),Quaternion.Euler(0,0,90)
    };
    protected static readonly (Vector3 pos, Quaternion rot)[] cam_pos = new (Vector3 pos, Quaternion rot)[] {
        (new Vector3(0,35,-10),Quaternion.Euler(45,0,0)),
        (new Vector3(-2,30,-5),Quaternion.Euler(0,0,0)),
        (new Vector3(-2,26,0),Quaternion.Euler(90,0,0)),
        (new Vector3(-2,26,0),Quaternion.Euler(90,0,90))
    };
    private void Start() {
        ruler1 = GameObject.Find("rulerRescaled");
        ruler2 = GameObject.Find("VernierRescaled");
        ruler2_head = GameObject.Find("MeasureHead");
        ruler2_body = GameObject.Find("MeasureBody");
        ruler2_head_rigid = ruler2_head.GetComponent<Rigidbody>();
        ruler2_body_rigid = ruler2_body.GetComponent<Rigidbody>();
        cam = Camera.main;
        flag = 3;
        cam.transform.position = cam_pos[0].pos;
        cam.transform.rotation = cam_pos[0].rot;
        //ruler1.SetActive(false);
        //ruler2.SetActive(false);
    }

    // Update is called once per frame
    private void Update() {
        if(Input.GetKeyDown(KeyCode.Z)) {
            cam.transform.position = cam_pos[3].pos;
            cam.transform.rotation = cam_pos[3].rot;
            gameObject.transform.position = book_pos[5];
            gameObject.transform.rotation = book_rot[1];
            flag = 1;
            current = 0;
            ruler2.SetActive(false);
            //ruler2.hideFlags |= HideFlags.HideInInspector;
            ruler1.SetActive(true);
            //ruler1.hideFlags &= ~HideFlags.HideInInspector;
            ruler1.transform.rotation = ruler1_rot[0];
        }
        else if(Input.GetKeyDown(KeyCode.X)) {
            cam.transform.position = cam_pos[2].pos;
            cam.transform.rotation = cam_pos[2].rot;
            gameObject.transform.position = book_pos[5];
            gameObject.transform.rotation = book_rot[1];
            flag = 0;
            current = 0;
            ruler2.SetActive(false);
            //ruler2.hideFlags |= HideFlags.HideInInspector;
            ruler1.SetActive(true);
            //ruler1.hideFlags &= ~HideFlags.HideInInspector;
            ruler1.transform.rotation = ruler1_rot[1];
        }
        else if(Input.GetKeyDown(KeyCode.Y)) {
            cam.transform.position = cam_pos[1].pos;
            cam.transform.rotation = cam_pos[1].rot;
            gameObject.transform.position = book_pos[0];
            gameObject.transform.rotation = book_rot[0];
            flag = 2;
            current = 0;
            //ruler1.hideFlags |= HideFlags.HideInInspector;
            ruler1.SetActive(false);
            //ruler2.hideFlags &= ~HideFlags.HideInInspector;
            ruler2.SetActive(true);
        }
        else if(Input.GetKeyDown(KeyCode.P) && flag == 2) {
            ruler2_body_rigid.velocity = Vector3.right;
            ruler2_head_rigid.velocity = Vector3.left;
        }
        else if(Input.GetKeyDown(KeyCode.O)) {
            flag = 3;
            gameObject.transform.position = book_pos[5];
            gameObject.transform.rotation = book_rot[1];
            cam.transform.position = cam_pos[0].pos;
            cam.transform.rotation = cam_pos[0].rot;
            ruler1.SetActive(true);
            ruler2.SetActive(true);
            //ruler1.hideFlags &= ~HideFlags.HideInInspector;
            //ruler2.hideFlags &= ~HideFlags.HideInInspector;
        }
        else if(Input.GetKeyDown(KeyCode.L) && flag != 3) {
            current += 1;
            switch(flag) {
            case 0:
                ruler1.transform.position = pos[0][current % 5];
                break;
            case 1:
                ruler1.transform.position = pos[1][current % 5];
                break;
            case 2:
                ResetRulerPosition();
                gameObject.transform.position = book_pos[current % 5];
                break;
            default:
                break;
            }
        }
        else if(Input.GetKeyDown(KeyCode.K)) {
            Debug.Log(IOHelper.ReadData());
        }
        else if(Input.GetKeyDown(KeyCode.J)) {
            IOHelper.WriteData("cbj%%%");
        }
    }
    private void ResetRulerPosition() {
        ruler2_body_rigid.transform.localPosition = ruler2_pos[1];
        ruler2_head_rigid.transform.localPosition = ruler2_pos[0];
        ruler2_head_rigid.constraints &= ~RigidbodyConstraints.FreezePositionX;
        ruler2_body_rigid.constraints &= ~RigidbodyConstraints.FreezePositionX;
    }
    private void OnCollisionEnter(Collision collision) {
        if(collision.collider.attachedRigidbody == ruler2_head_rigid) {
            ruler2_head_rigid.constraints |= RigidbodyConstraints.FreezePositionX;
            ruler2_head_rigid.velocity = Vector3.zero;
        }
        else if(collision.collider.attachedRigidbody == ruler2_body_rigid) {
            ruler2_body_rigid.constraints |= RigidbodyConstraints.FreezePositionX;
            ruler2_body_rigid.velocity = Vector3.zero;
        }
    }
}

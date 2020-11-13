using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityScript.Lang;

public class MoveRuler2 : MonoBehaviour {
    // Start is called before the first frame update
    private float[] book_height = new float[10];
    private float def_height = 2.0f, def_width, def_length;
    private float growfactor = 0.3f;
    private GameObject head_obj, body_obj, cube_obj;
    private Rigidbody head_rigid, body_rigid;
    private BoxCollider head_collide, body_collide;
    private bool moving = false;
    private static readonly Vector3 head_pos0 = new Vector3(-6, 0, 0);
    private void Start() {
        body_obj = GameObject.Find("MeasureBody");
        head_obj = GameObject.Find("MeasureHead");
        cube_obj = GameObject.Find("Cube");
        def_width = cube_obj.transform.localScale.x;
        def_length = cube_obj.transform.localScale.z;
        body_rigid = body_obj.GetComponent<Rigidbody>();
        head_rigid = head_obj.GetComponent<Rigidbody>();
        body_collide = body_obj.GetComponent<BoxCollider>();
        head_collide = head_obj.GetComponent<BoxCollider>();
        for(int i = 0; i < 10; i++) {
            book_height[i] = growfactor * (i + 1) + Random.Range(-0.01f, 0.01f);
        }
    }

    // Update is called once per frame
    private void Update() {
        if(Input.GetKeyDown(KeyCode.P) && !moving) {
            moving = true;
            body_rigid.velocity = Vector3.up;
            head_rigid.velocity = Vector3.down;
        }
        else if(Input.GetKeyDown(KeyCode.L)) {
            head_rigid.velocity = Vector3.zero;
            body_rigid.velocity = Vector3.zero;
            moving = false;
        }
        else if(Input.GetKeyDown(KeyCode.O)) {
            Debug.Log(Mathf.Abs(body_collide.transform.position.y - head_collide.transform.position.y).ToString("0.000"));
            Debug.Log(moving);
        }
        else {
            if(!moving) {
                if(Input.GetKeyDown(KeyCode.R)) {
                    InitBook(def_height);
                }
                else if(Input.GetKeyDown(KeyCode.Alpha0)) {
                    InitBook(book_height[0]);
                }
                else if(Input.GetKeyDown(KeyCode.Alpha1)) {
                    InitBook(book_height[1]);
                }
                else if(Input.GetKeyDown(KeyCode.Alpha2)) {
                    InitBook(book_height[2]);
                }
                else if(Input.GetKeyDown(KeyCode.Alpha3)) {
                    InitBook(book_height[3]);
                }
                else if(Input.GetKeyDown(KeyCode.Alpha4)) {
                    InitBook(book_height[4]);
                }
                else if(Input.GetKeyDown(KeyCode.Alpha5)) {
                    InitBook(book_height[5]);
                }
                else if(Input.GetKeyDown(KeyCode.Alpha6)) {
                    InitBook(book_height[6]);
                }
                else if(Input.GetKeyDown(KeyCode.Alpha7)) {
                    InitBook(book_height[7]);
                }
                else if(Input.GetKeyDown(KeyCode.Alpha8)) {
                    InitBook(book_height[8]);
                }
                else if(Input.GetKeyDown(KeyCode.Alpha9)) {
                    InitBook(book_height[9]);
                }
            }
        }
    }
    private void ResetRulerPosition() {
        body_rigid.transform.localPosition = Vector3.zero;
        head_rigid.transform.localPosition = head_pos0;
    }
    private void OnCollisionEnter(Collision collision) {
        if(collision.collider.attachedRigidbody == head_rigid) {
            head_rigid.velocity = Vector3.zero;
        }
        else if(collision.collider.attachedRigidbody == body_rigid) {
            body_rigid.velocity = Vector3.zero;
        }
    }
    private void InitBook(float thick) {
        ResetRulerPosition();
        cube_obj.transform.localScale = new Vector3(def_width, thick, def_length);
    }
}

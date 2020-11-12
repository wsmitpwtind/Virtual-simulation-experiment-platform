using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;

public class MoveRuler : MonoBehaviour {
    private GameObject head_obj, body_obj;
    private Rigidbody head_rigid, body_rigid;
    private BoxCollider head_collide, body_collide;
    // Start is called before the first frame update
    private bool moving = false;
    private static readonly Vector3 fwd = Vector3.left * 0.01f, bck = Vector3.right * 0.01f;
    private void Start() {
        body_obj = GameObject.Find("MeasureBody");
        head_obj = GameObject.Find("MeasureHead");
        body_rigid = body_obj.GetComponent<Rigidbody>();
        head_rigid = head_obj.GetComponent<Rigidbody>();
        body_collide = body_obj.GetComponent<BoxCollider>();
        head_collide = head_obj.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    private void Update() {
        if(!moving && Input.GetKey(KeyCode.P)) {
            moving = true;
            body_rigid.velocity = Vector3.right;
            head_rigid.velocity = Vector3.left;
        }
        if(Input.GetKey(KeyCode.O)) {
            Debug.Log(Mathf.Abs(body_collide.transform.position.x - head_collide.transform.position.x).ToString("0.000"));
            Debug.Log(moving);
        }

        if(moving && Input.GetKey(KeyCode.L)) {
            head_rigid.velocity = Vector3.zero;
            body_rigid.velocity = Vector3.zero;
            moving = false;
        }

    }
    private void OnCollisionEnter(Collision collision) {
        if(collision.collider == head_rigid) {
            head_rigid.velocity = Vector3.zero;
            if(body_rigid.velocity == Vector3.zero) {
                moving = false;
            }

        }
        else if(collision.collider == body_rigid) {
            body_rigid.velocity = Vector3.zero;
            if(head_rigid.velocity == Vector3.zero) {
                moving = false;
            }
        }
    }
}

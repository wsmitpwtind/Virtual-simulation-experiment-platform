using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRuler3 : MonoBehaviour {
    // Start is called before the first frame update
    protected GameObject book, ruler1, ruler2;
    protected Camera cam;
    private void Start() {
        book = GameObject.Find("book_0001b");
        ruler1 = GameObject.Find("rulerRescaled");
        ruler2 = GameObject.Find("VernierRescaled");
        cam = Camera.main;
    }

    // Update is called once per frame
    private void Update() {
        if(Input.GetKeyDown(KeyCode.Z)) {

        }
        else if(Input.GetKeyDown(KeyCode.X)) {

        }
        else if(Input.GetKeyDown(KeyCode.C)) {

        }
    }
    private void MeasureX() {

    }
    private void MeasureY() {

    }
    private void MeasureZ() {

    }
    private void OnCollisionEnter(Collision collision) {

    }
}

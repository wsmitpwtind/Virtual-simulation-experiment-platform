﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testIO : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(hh);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void hh()
    {
        // GameObject obj =  IOHelper.AddPrefab();

        // GameObject.Instantiate<GameObject>(obj);
        Storage storage = new Storage(1);

        storage.SetStorage("button", new Model123 (){
            name = "WO",
            age = 122
        });
        Debug.Log(storage.GetStorage<Model123>("button").age);
        Debug.Log((storage["button123", typeof(Model123)] as Model123).name);
    }
    class Model123
    {
        public  string name { get; set; }
        public int age { get; set; }
    }
}

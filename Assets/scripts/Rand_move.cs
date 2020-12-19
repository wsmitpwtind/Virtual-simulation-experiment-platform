using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rand_move : MonoBehaviour
{
    private Vector3 start;
    private float speed_x;
    private float speed_y;
    private float speed_z;
    private float limit = 0.06f;
    private Vector3 rot=new Vector3();
    // Start is called before the first frame update
    void Start()
    {
        start = transform.position;
        speed_x = Random.Range(-5, 6) * 0.015f;
        speed_y = Random.Range(-5, 6) * 0.015f;
        speed_z = Random.Range(-5, 6) * 0.015f;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > start.x + limit || transform.position.x < start.x - limit)
        {
            speed_x = -speed_x;
            transform.position += new Vector3(1, 0, 0) * speed_x*Time.deltaTime;
        }
        else
        {
            transform.position += new Vector3(1, 0, 0) * speed_x* Time.deltaTime;
        }

        if (transform.position.y > start.y + limit || transform.position.y < start.y - limit)
        {
            speed_y = -speed_y;
            transform.position += new Vector3(0, 1, 0) * speed_y* Time.deltaTime;
        }
        else
        {
            transform.position += new Vector3(0, 1, 0) * speed_y* Time.deltaTime;
        }

        if (transform.position.z > start.z + limit || transform.position.z < start.z - limit)
        {
            speed_z = -speed_z;
            transform.position += new Vector3(0, 0, 1) * speed_z * Time.deltaTime;
        }
        else
        {
            transform.position += new Vector3(0, 0, 1) * speed_z * Time.deltaTime;
        }
    }
}

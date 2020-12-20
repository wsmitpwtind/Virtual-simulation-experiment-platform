using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Self_loc : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetMouseButton(1))
        {
            if (!Input.GetMouseButton(0))
            {
                if (transform.eulerAngles.x < 30 && transform.eulerAngles.x > 1)
                    transform.rotation = Quaternion.Euler(transform.eulerAngles.x - 1, transform.eulerAngles.y, transform.eulerAngles.z);
                if (transform.eulerAngles.x <= 1 && transform.eulerAngles.x > 0)
                    transform.rotation = Quaternion.Euler(360.0f, transform.eulerAngles.y, transform.eulerAngles.z);
                if (transform.eulerAngles.x > 330 && transform.eulerAngles.x < 359)
                    transform.rotation = Quaternion.Euler(transform.eulerAngles.x + 1, transform.eulerAngles.y, transform.eulerAngles.z);
                if (transform.eulerAngles.x >= 359)
                    transform.rotation = Quaternion.Euler(360.0f, transform.eulerAngles.y, transform.eulerAngles.z);

                if (transform.eulerAngles.x < 89 && transform.eulerAngles.x > 60)
                    transform.rotation = Quaternion.Euler(transform.eulerAngles.x + 1, transform.eulerAngles.y, transform.eulerAngles.z);
                if (transform.eulerAngles.x <= 90 && transform.eulerAngles.x >= 89)
                    transform.rotation = Quaternion.Euler(90.0f, transform.eulerAngles.y, transform.eulerAngles.z);

                if (transform.eulerAngles.x < 300 && transform.eulerAngles.x > 271)
                    transform.rotation = Quaternion.Euler(transform.eulerAngles.x - 1, transform.eulerAngles.y, transform.eulerAngles.z);
                if (transform.eulerAngles.x <= 271 && transform.eulerAngles.x >= 270)
                    transform.rotation = Quaternion.Euler(270.0f, transform.eulerAngles.y, transform.eulerAngles.z);



                if (transform.eulerAngles.y < 30 && transform.eulerAngles.y > 1)
                    transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y - 1, transform.eulerAngles.z);
                if (transform.eulerAngles.y <= 1)
                    transform.rotation = Quaternion.Euler(transform.eulerAngles.x, 0.0f, transform.eulerAngles.z);
                if (transform.eulerAngles.y > 330 && transform.eulerAngles.y < 359)
                    transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + 1, transform.eulerAngles.z);
                if (transform.eulerAngles.y >= 359)
                    transform.rotation = Quaternion.Euler(transform.eulerAngles.x, 360.0f, transform.eulerAngles.z);

                if (transform.eulerAngles.y < 89 && transform.eulerAngles.y > 60)
                    transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + 1, transform.eulerAngles.z);
                if (transform.eulerAngles.y <= 120 && transform.eulerAngles.y >= 91)
                    transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y - 1, transform.eulerAngles.z);
                if (transform.eulerAngles.y < 91 && transform.eulerAngles.y > 89)
                    transform.rotation = Quaternion.Euler(transform.eulerAngles.x, 90.0f, transform.eulerAngles.z);

                if (transform.eulerAngles.y < 179 && transform.eulerAngles.y > 150)
                    transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + 1, transform.eulerAngles.z);
                if (transform.eulerAngles.y <= 210 && transform.eulerAngles.y >= 181)
                    transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y - 1, transform.eulerAngles.z);
                if (transform.eulerAngles.y < 181 && transform.eulerAngles.y > 179)
                    transform.rotation = Quaternion.Euler(transform.eulerAngles.x, 180.0f, transform.eulerAngles.z);

                if (transform.eulerAngles.y < 269 && transform.eulerAngles.y > 240)
                    transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + 1, transform.eulerAngles.z);
                if (transform.eulerAngles.y <= 300 && transform.eulerAngles.y >= 271)
                    transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y - 1, transform.eulerAngles.z);
                if (transform.eulerAngles.y < 271 && transform.eulerAngles.y > 269)
                    transform.rotation = Quaternion.Euler(transform.eulerAngles.x, 270.0f, transform.eulerAngles.z);





                if (transform.eulerAngles.z < 30 && transform.eulerAngles.z > 1)
                    transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z - 1);
                if (transform.eulerAngles.z <= 1)
                    transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0.0f);
                if (transform.eulerAngles.z > 330 && transform.eulerAngles.z < 359)
                    transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + 1);
                if (transform.eulerAngles.z >= 359)
                    transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 360.0f);

                if (transform.eulerAngles.z < 89 && transform.eulerAngles.z > 60)
                    transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + 1);
                if (transform.eulerAngles.z <= 120 && transform.eulerAngles.z >= 91)
                    transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z - 1);
                if (transform.eulerAngles.z < 91 && transform.eulerAngles.z > 89)
                    transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 90.0f);

                if (transform.eulerAngles.z < 179 && transform.eulerAngles.z > 150)
                    transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + 1);
                if (transform.eulerAngles.z <= 210 && transform.eulerAngles.z >= 181)
                    transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z - 1);
                if (transform.eulerAngles.z < 181 && transform.eulerAngles.z > 179)
                    transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 180.0f);

                if (transform.eulerAngles.z < 269 && transform.eulerAngles.z > 240)
                    transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + 1);
                if (transform.eulerAngles.z <= 300 && transform.eulerAngles.z >= 271)
                    transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z - 1);
                if (transform.eulerAngles.z < 271 && transform.eulerAngles.z > 269)
                    transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 270.0f);

            }
        }
    }
}

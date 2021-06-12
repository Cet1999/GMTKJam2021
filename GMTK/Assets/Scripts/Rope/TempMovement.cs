using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempMovement : MonoBehaviour
{
    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;

    void Update()
    {
        if (Input.GetKeyDown("w"))
        {
            // Move translation along the object's z-axis
            transform.Translate(0, 0, speed *Time.deltaTime);
        }
        if (Input.GetKeyDown("s"))
        {
            // Move translation along the object's z-axis
            transform.Translate(0, 0, -speed * Time.deltaTime);
        }

        if (Input.GetKeyDown("d"))
        {
            // Move translation along the object's z-axis
            transform.Rotate(0, rotationSpeed*Time.deltaTime, 0);
        }
        if (Input.GetKeyDown("a"))
        {
            // Move translation along the object's z-axis
            transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0);
        }


    }
}
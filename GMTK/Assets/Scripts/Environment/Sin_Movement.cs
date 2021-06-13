using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sin_Movement : MonoBehaviour
{
    public float speedUpDown = 1;
    public float distanceUpDown = 1;
    public float startHeight = 0.5f;

    void Update()
    {
        //Vector3 mov = new Vector3(transform.position.x, Mathf.Sin(speedUpDown * Time.time) * distanceUpDown + startHeight, transform.position.z);
        Vector3 mov = new Vector3(transform.position.x, Mathf.Sin(speedUpDown * Time.time) * distanceUpDown + transform.position.y, transform.position.z);
        transform.position = mov;
    }

    //a function to change the variables
    public void ChangeMovement(float distUpNDwn)
    {
        distanceUpDown = distUpNDwn;

    }//end of change movement

}

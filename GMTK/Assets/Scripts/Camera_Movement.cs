using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Movement : MonoBehaviour
{
    public Transform Player1Reference;
    public Transform Player2Reference;
    public bool Smooth = true;
    //Camera angle (rotation.x)
    public float Angle = 70;
    //distance between camera & player
    public float HeightMin = 18;
    public float Height = 18;
    //
    public float SmoothTime = 0.3f;
    public Vector3 velocity = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        //Calculate the position of camera with a fixed Angle & Distance
        float yDiff = Mathf.Sin(Angle * Mathf.Deg2Rad) * Height;
        float zDiff = Mathf.Cos(Angle * Mathf.Deg2Rad) * Height;
        Vector3 newPosition = new Vector3();

        Height = Mathf.Max(HeightMin, HeightMin*(Vector3.Distance(Player1Reference.position, Player2Reference.position)/20));

        newPosition.x = (Player1Reference.position.x + Player2Reference.position.x) / 2;
        newPosition.y = (Player1Reference.position.y + Player2Reference.position.y) / 2 + yDiff;
        newPosition.z = (Player1Reference.position.z + Player2Reference.position.z) / 2 - zDiff;
        //Adjust camera transform
        if (Smooth)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(Angle, 0, 0), SmoothTime);
            transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, SmoothTime);
        }
        else
        {
            transform.rotation = Quaternion.Euler(Angle, 0, 0);
            transform.position = newPosition;
        }
    }
}
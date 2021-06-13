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
    private float HeightMin = 18;
    private float Height = 18;
    //
    public float SmoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;

    private bool Shaking = false;

    void Start()
    {
        //InvokeRepeating("ShakeCamera", 2.0f, 5.0f);
    }

    // Update is called once per frame
    void Update()
    {


        //Calculate the position of camera with a fixed Angle & Distance
        float yDiff = Mathf.Sin(Angle * Mathf.Deg2Rad) * Height;
        float zDiff = Mathf.Cos(Angle * Mathf.Deg2Rad) * Height;
        Vector3 newPosition = new Vector3();



        Height = Mathf.Max(HeightMin, HeightMin*(Vector3.Distance(Player1Reference.position, Player2Reference.position)/14));

        newPosition.x = (Player1Reference.position.x + Player2Reference.position.x) / 2;
        newPosition.y = (Player1Reference.position.y + Player2Reference.position.y) / 2 + yDiff;
        newPosition.z = (Player1Reference.position.z + Player2Reference.position.z) / 2 - zDiff;

        if (!Shaking) {
            UpdateCameraTransform(newPosition);
        }
    }

    private void UpdateCameraTransform(Vector3 newPosition)
    {
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

    public void ShakeCamera()
    {
        Debug.Log("Shake!");
        StartCoroutine(Shake(.15f, .4f));
    }

    IEnumerator Shake (float duration, float magnitude)
    {
        Shaking = true;
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float xrange = Random.Range(-0.2f, 0.2f) * magnitude;
            float yrange = Random.Range(-0.2f, 0.2f) * magnitude;

            transform.localPosition = new Vector3(transform.localPosition.x + xrange, transform.localPosition.y + yrange, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;

        Shaking = false;
    }

}
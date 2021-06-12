using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullForce : MonoBehaviour
{
    public Rigidbody Target;
    public bool player;
    public float force;
    public float MaxDist;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 attractionDir = transform.position - Target.transform.position;

        float AppliedForce =  Mathf.Min(2000f, Mathf.Max(0, force * (Vector3.Distance(transform.position, Target.transform.position) - MaxDist) / MaxDist));


        Target.AddForce(attractionDir * AppliedForce, ForceMode.Force);
    }
}

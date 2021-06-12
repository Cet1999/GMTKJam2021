///following this tutorial ---> <https://www.youtube.com/watch?v=Cecp4a7FsTU>
///code from ---> <https://gist.github.com/ditzel/ae6ebc115d767da9a5a1e1f70dee27e5>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceJoint3D : MonoBehaviour
{

    public Transform ConnectedRigidbody;
    public bool DetermineDistanceOnStart = true;
    public float Distance;
    public float Spring = 0.1f;
    public float Damper = 5f;

    protected Rigidbody Rigidbody;

    void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        if (DetermineDistanceOnStart && ConnectedRigidbody != null)
            Distance = Vector3.Distance(Rigidbody.position, ConnectedRigidbody.position);

        //get script ref of parent
        if(ConnectedRigidbody == null && transform.parent.GetComponent<DistanceJoint3D>() != null)
        {
            ConnectedRigidbody = transform.parent;
        }//end of get script ref
    }

    void FixedUpdate()
    {

        var connection = Rigidbody.position - ConnectedRigidbody.position;
        var distanceDiscrepancy = Distance - connection.magnitude;

        Rigidbody.position += distanceDiscrepancy * connection.normalized;

        var velocityTarget = connection + (Rigidbody.velocity + Physics.gravity * Spring);
        var projectOnConnection = Vector3.Project(velocityTarget, connection);
        Rigidbody.velocity = (velocityTarget - projectOnConnection) / (1 + Damper * Time.fixedDeltaTime);


    }

}//end of distance joint 3d script

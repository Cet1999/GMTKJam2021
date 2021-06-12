using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneManipulation : MonoBehaviour
{

    private bool parentIsPlayer;
    private bool childIsPlayer;
    public Transform t_Parent;
    public Transform t_Child;
    [Range(0.85f, 10.0f)]
    public float rangeBounce;
    public int speed_BounceBack;

    private Rigidbody rb_Player1;
    private Rigidbody rb_Player2;

    private float timer_StretchRope;
    private float time_StretchRope = 0.25f;

    private float timer_BounceBack;
    private float time_BounceBack = 0.15f;

    private float bonePositionPref_Parent = 1;
    private float bonePositionPref_Child = 1;

    // Start is called before the first frame update
    void Start()
    {
        //t_Parent = transform.parent;
        if(t_Parent.GetComponent<BoneManipulation>() == null)
        {
            parentIsPlayer = true;
            rb_Player1 = t_Parent.GetComponent<Rigidbody>();
            bonePositionPref_Parent = 1.5f;
            bonePositionPref_Child = 0.5f;
        }
        if(t_Child.GetComponent<BoneManipulation>() == null)
        {
            childIsPlayer = true;
            rb_Player2 = t_Child.GetComponent<Rigidbody>();
            bonePositionPref_Parent = 0.5f;
            bonePositionPref_Child = 1.5f;
        }

        timer_BounceBack = Time.time + 2.5f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Time.timeSinceLevelLoad < 1.25f) { print("wait time"); return; }

        transform.position = ((t_Parent.position * bonePositionPref_Parent) + (t_Child.position * bonePositionPref_Child)) / 2;
        transform.LookAt(t_Child);

        //if player is parent
        if(parentIsPlayer == true)
        {
            float dist = Vector3.Distance(t_Parent.position, transform.position);
            //print("parent Dist: " + dist);

            //if we move too far from range
            if (dist >= rangeBounce)
            {
                print("p1: rope is further than it should be");

                //if we have been stretching the rope too long
                if(Time.time > timer_StretchRope + time_StretchRope)
                {
                    //check if we just bounced back
                    if (Time.time > timer_BounceBack + timer_BounceBack)
                    {
                        print("p1: bouncing back");

                        t_Parent.LookAt(transform.position);
                        rb_Player1.AddForce(Vector3.forward * speed_BounceBack * (dist - rangeBounce)*5);
                        timer_BounceBack = Time.time;
                    }//end of timer

                    timer_StretchRope = Time.time;
                }//end of stretching rope too long
                else { print("p1: stretching rope"); }


            }
            else { timer_StretchRope = Time.time; }
        }//end of is parent

        //if player is child
        if (childIsPlayer == true)
        {
            float dist = Vector3.Distance(transform.position, t_Child.position);
            //print("parent Dist: " + dist);

            //if we move too far from range
            if (dist >= rangeBounce)
            {
                print("p2: rope is further than it should be");

                //if we have been stretching the rope too long
                if (Time.time > timer_StretchRope + time_StretchRope)
                {
                    //check if we just bounced back
                    if (Time.time > timer_BounceBack + timer_BounceBack)
                    {
                        print("p2 bouncing back");
                        
                        t_Child.LookAt(transform.position);
                        rb_Player2.AddForce(-Vector3.forward * speed_BounceBack * (dist - rangeBounce) * 5);
                        timer_BounceBack = Time.time;
                    }//end of timer

                    timer_StretchRope = Time.time;
                }//end of stretching rope too long
                else { print("p2 stretching rope"); }


            }
            else { timer_StretchRope = Time.time; }

        }// end of child is player

    }//end of update



}//end of bone manipulation script

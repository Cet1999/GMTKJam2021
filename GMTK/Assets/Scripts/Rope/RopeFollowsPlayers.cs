using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeFollowsPlayers : MonoBehaviour
{


    public Transform t_ropeParent1;
    public Transform t_ropeParent2;
    public Transform t_ropeRoot;
    public Transform t_ropeEnd;


    // Update is called once per frame
    void Update()
    {
        //if not null
        if(t_ropeParent1 || t_ropeRoot != null)
        {
            t_ropeRoot.position = t_ropeParent1.position;
        }

        if (t_ropeParent1 || t_ropeRoot != null)
        {
            t_ropeEnd.position = t_ropeParent2.position;
        }
        
    }
}

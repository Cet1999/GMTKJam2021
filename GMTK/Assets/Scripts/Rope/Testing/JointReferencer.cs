// from minionsart --> <https://www.patreon.com/posts/setting-up-using-38499638>
// bastebin: <https://pastebin.com/Dx3T2Q82>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointReferencer : MonoBehaviour
{
    private FixedJoint comp_FJ;
    private HingeJoint comp_HJ;
    private ConfigurableJoint comp_CJ;


    // Start is called before the first frame update
    void Start()
    {
        if(transform.GetComponent<FixedJoint>() != null)
        {
            comp_FJ = transform.GetComponent<FixedJoint>();

            if(transform.parent.GetComponent<FixedJoint>() != null)
            {
                comp_FJ.connectedBody = transform.parent.GetComponent<Rigidbody>();
            }
        }

        if (transform.GetComponent<HingeJoint>() != null)
        {
            comp_HJ = transform.GetComponent<HingeJoint>();

            if (transform.parent.GetComponent<HingeJoint>() != null)
            {
                comp_HJ.connectedBody = transform.parent.GetComponent<Rigidbody>();
            }
        }


        if (transform.GetComponent<ConfigurableJoint>() != null)
        {
            comp_CJ = transform.GetComponent<ConfigurableJoint>();

            if (transform.parent.GetComponent<ConfigurableJoint>() != null)
            {
                comp_CJ.connectedBody = transform.parent.GetComponent<Rigidbody>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

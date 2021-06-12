using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public GameObject SwitchBody;
    public bool On;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SwitchBody.transform.localRotation.normalized.x > 0f)
        {
            On = true;
            Debug.Log("Switched On!");
        } else
        {
            On = false;
            Debug.Log("Switched Off!");
        }
    }
}

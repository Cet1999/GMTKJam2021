using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationVFX : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(stateInfo.IsName("BigGuy_Flop"))
        {
            Camera.main.GetComponent<Camera_Movement>().ShakeCamera();
            animator.gameObject.transform.parent.GetComponent<BigPlayer_Movement>().DestroyDetection = true;
            animator.gameObject.transform.parent.GetComponent<BigPlayer_Movement>().DestroyObject();
        } else if (stateInfo.IsName("BigGuy_FlopBack"))
        {
            animator.gameObject.transform.parent.GetComponent<BigPlayer_Movement>().DestroyDetection = false;
            
        }
    }
}

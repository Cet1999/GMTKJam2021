using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainHandle : MonoBehaviour
{
    [SerializeField]
    [Tooltip("If this script is on the small player: put the small player's transform in here...... If it's the handle, then put the child obj 'Player2_ChainLinkSpot' transform in here instead")]
    Transform player;
    [SerializeField]
    [Tooltip("If this script is on the small plauer: put the chain 'Handle' transform in here..... If it's on the handle, then put the chain's first link 'G_ChainLink_Single' transform in here")]
    Transform firstChain;
    float distance;

    [Range(-10.0f, 10.0f)]
    public float distanceOffset = 1.0f;
    [Range(-10.0f, 10.0f)]
    public float distanceOffsetFinal = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        if(player == null || firstChain == null) { print(transform.name + " : is missing references"); return; }

        distance = Vector3.Distance(transform.position, firstChain.position * distanceOffset);
    }
    void OnDrawGizmos()
    {
        if (player == null || firstChain == null) { print(transform.name + " : is missing references"); return; }
        Gizmos.color = Color.yellow;
        if (firstChain != null)
        {
            Gizmos.DrawWireSphere(firstChain.position, distance * distanceOffsetFinal);
        }
    }

    private void FixedUpdate()
    {
        if (player == null || firstChain == null) { print(transform.name + " : is missing references"); return; }
        // towards "player"
        Vector3 dir = player.transform.position - firstChain.transform.position;
        // clamp length
        dir = Vector3.ClampMagnitude(dir, distance * distanceOffsetFinal);
        // add clamped length
        transform.position = firstChain.transform.position + dir;
        // look towards movement
        if (distanceOffset == 1.0f)
        {
            transform.rotation = Quaternion.LookRotation(-dir);
        }
        
    }
}
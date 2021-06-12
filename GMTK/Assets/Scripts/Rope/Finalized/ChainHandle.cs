using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainHandle : MonoBehaviour
{
    [SerializeField]
    Transform player;
    [SerializeField]
    Transform firstChain;
    float distance;

    [Range(-10.0f, 10.0f)]
    public float distanceOffset = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        distance = Vector3.Distance(transform.position, firstChain.position * distanceOffset);
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if (firstChain != null)
        {
            Gizmos.DrawWireSphere(firstChain.position, distance);
        }
    }

    private void FixedUpdate()
    {
        // towards "player"
        Vector3 dir = player.transform.position - firstChain.transform.position;
        // clamp length
        dir = Vector3.ClampMagnitude(dir, distance);
        // add clamped length
        transform.position = firstChain.transform.position + dir;
        // look towards movement
        transform.rotation = Quaternion.LookRotation(-dir);
    }
}
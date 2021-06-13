using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnBox : MonoBehaviour
{
    public List<GameObject> Players;
    public List<GameObject> Points;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject closest = null;
        float shortestDist = 9999;
        foreach (GameObject p in Points)
        {
            //if (Players[0].transform.position.z - p.transform.position.z < shortestDist)
            if (Vector3.Distance(Players[0].transform.position, p.transform.position) < shortestDist)
            {
                shortestDist = Vector3.Distance(Players[0].transform.position, p.transform.position);
                closest = p;
            }
        }

        Players[0].transform.position = closest.transform.position;
        Players[1].transform.position = closest.transform.position;
        Players[2].transform.position = closest.transform.position;
    }
}

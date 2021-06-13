using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    // Start is called before the first frame update
    [Range(1.0f, 3.0f)]
    public float Volume = 1.0f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Destruction()
    {
        GameObject.FindGameObjectWithTag("Particle").transform.position = transform.position;
        GameObject.FindGameObjectWithTag("Particle").GetComponent<ParticleSystemRenderer>().material = GetComponent<MeshRenderer>().materials[0];

        ParticleSystem.MainModule ps = GameObject.FindGameObjectWithTag("Particle").GetComponent<ParticleSystem>().main;
        ps.startSize = new ParticleSystem.MinMaxCurve(0.5f * Volume, 2f * Volume);
        GameObject.FindGameObjectWithTag("Particle").GetComponent<ParticleSystem>().Play();


        Destroy(this.gameObject);
    }
}

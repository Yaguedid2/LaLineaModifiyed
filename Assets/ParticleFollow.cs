using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleFollow : MonoBehaviour
{
    Vector3 target;
    bool follow = false;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(follow)
        {
            transform.position = target;
        }
    }
    public void followObject(Vector3 destination)
    {
        follow = true;
        target = destination;
        GetComponent<ParticleSystem>().Play();
    }
    public void stop()
    {
        follow = false;
        GetComponent<ParticleSystem>().Stop();
    }

}

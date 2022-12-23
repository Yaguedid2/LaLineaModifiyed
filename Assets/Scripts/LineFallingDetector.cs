using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineFallingDetector : MonoBehaviour
{
   

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void OnCollisionEnter(Collision collision)
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("yes");
        if (other.tag == "line")
        {
           
            PlayerController.instance.fall = false;
        }
            
    }
}

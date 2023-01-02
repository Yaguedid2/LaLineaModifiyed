using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Players : MonoBehaviour
{
    void Start()
    {
        GetComponent<Animator>().SetFloat("Blend",Int32.Parse(gameObject.name)-1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

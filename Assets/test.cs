using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    LineRenderer lineRenderer;
    public Vector3 pos;
    public int index;
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
      
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(index, pos);
    }
}

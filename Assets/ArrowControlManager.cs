using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowControlManager : MonoBehaviour
{
   public static string selected="none";
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void select(string arrow)
    {
        selected = arrow;
    }
}

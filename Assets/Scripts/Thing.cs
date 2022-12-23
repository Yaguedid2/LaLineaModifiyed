using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thing : MonoBehaviour
{
    public List<behavior> behaviors = new List<behavior>();

    [System.Serializable]
    public class behavior
    {

        public string name;
        public bool on;
    }
  
}

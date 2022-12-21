using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    public string className;
    public int index;
  
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    bool oneTime = true;
    private void OnCollisionEnter(Collision collision)
    {
        if (oneTime)
        {
            oneTime = false;
            StartCoroutine(freeze());
        }
       
    }
    IEnumerator freeze()
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        yield return new WaitForSeconds(0.5f);
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ;
       

    }
    public void setClassName(string type)
    {
        className = type;
    }
    public void setIndex(int i)
    {
        index = i;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBehaviorManager : MonoBehaviour
{
    public static ObjectBehaviorManager instance;
    Animator animator;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void behave(Object objectToBeahveOn)
    {
      var categories= ObjectTypeHandler.instance.objects[objectToBeahveOn.index].objectCategories;
        if(categories.admire)
        {
            StartCoroutine(admire(objectToBeahveOn));
         
        }
    }
    IEnumerator admire(Object objectToBeahveOn)
    {
        SpeakManager.instance.Speak("What a beautiful " + objectToBeahveOn.className, false, 10);
        animator.Play("clap");
        yield return new WaitForSeconds(3f);
        PlayerController.instance.changeLinePosition(false);

    }
}

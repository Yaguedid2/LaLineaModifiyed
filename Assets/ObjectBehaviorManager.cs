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
        behaviourIncludeMovement = false;
        finishedCheckingAllBehaviours = false;
        var categories= ObjectTypeHandler.instance.objects[objectToBeahveOn.index].objectCategories;
        if(!categories.bigSize)
        {
            objectToBeahveOn.transform.localScale = new Vector3(2.66f, 2.66f, 1);
        }
        if(categories.admire)
        {
            StartCoroutine(admire(objectToBeahveOn));
         
        }
        if(categories.enter)
        {
            behaviourIncludeMovement = true;
            string nameOfParentObject = objectToBeahveOn.name;
            string nameOfChildObject = "realRight";

            string childLocation = "/" + nameOfParentObject + "/" + nameOfChildObject;
          
            GameObject rightCorner = GameObject.Find(childLocation);
            StartCoroutine(MoveAndEnter(rightCorner.transform.position.x,categories.sleep,categories.pray));
           

        }
        if(categories.eat)
        {
            behaviourIncludeMovement = true;
            string nameOfParentObject = objectToBeahveOn.name;
            string nameOfChildObject = "realRight";

            string childLocation = "/" + nameOfParentObject + "/" + nameOfChildObject;

            GameObject rightCorner = GameObject.Find(childLocation);
            StartCoroutine(eat(rightCorner.transform.position.x,objectToBeahveOn.gameObject));
        }
        if (categories.drive)
        {
            behaviourIncludeMovement = true;
            string nameOfParentObject = objectToBeahveOn.name;
            string nameOfChildObject = "realRight";
            string childLocation = "/" + nameOfParentObject + "/" + nameOfChildObject;
            GameObject rightCorner = GameObject.Find(childLocation);
             nameOfChildObject = "realLeft";
             childLocation = "/" + nameOfParentObject + "/" + nameOfChildObject;
            GameObject leftCorner = GameObject.Find(childLocation);
            nameOfChildObject = "top";
            childLocation = "/" + nameOfParentObject + "/" + nameOfChildObject;
            GameObject topCorner = GameObject.Find(childLocation);

            float disTopRight = Vector3.Distance(topCorner.transform.localPosition, rightCorner.transform.localPosition);
            float disTopLeft = Vector3.Distance(topCorner.transform.localPosition, leftCorner.transform.localPosition);
            float xToGoTo;
            if (disTopLeft <= disTopRight)
                xToGoTo = leftCorner.transform.position.x;
            else
            {
                objectToBeahveOn.transform.localScale = new Vector3(-11, 11, 1);
                xToGoTo = rightCorner.transform.position.x;
            }
           
           

            StartCoroutine(drive(xToGoTo+3, objectToBeahveOn.gameObject));
        }

        finishedCheckingAllBehaviours = true;
    }
    bool finishedCheckingAllBehaviours = false;
    bool behaviourIncludeMovement = false;
    IEnumerator admire(Object objectToBeahveOn)
    {
        SpeakManager.instance.Speak("What a beautiful " + objectToBeahveOn.className, false, 10);
        animator.Play("clap");
        yield return new WaitForSeconds(3f);
        StartCoroutine(movePlayerIfNotAlreadyMoving(objectToBeahveOn.gameObject));

    }
    IEnumerator MoveAndEnter(float x,bool sleep,bool pray)
    {
        yield return new WaitForSeconds(5f);
        Vector3 actualPosition = transform.position;
        Vector3 targetPosition = new Vector3(x, actualPosition.y, actualPosition.z);
        PlayerController.instance.changeLinePosition(false);

        while (transform.position.x > x)
        {
            yield return new WaitForEndOfFrame();
        }
       
            animator.Play("enter");


        showOrHidePlayer(false);
        if(sleep)
        {
            SpeakManager.instance.Speak("ZZZ ", false, 1);
            SpeakManager.instance.Speak("ZZZ ", false, 1);
            SpeakManager.instance.Speak("ZZZ ", false, 1);
            SpeakManager.instance.Speak("ZZZ ", false, 1);
        }
       if(pray)
        {
            SpeakManager.instance.Speak("God is Great!", false, 1);
            SpeakManager.instance.Speak("What a peace! ", false, 1);
            SpeakManager.instance.Speak("amen  ", false, 1);
         
        }
       
        yield return new WaitForSeconds(7f);
        showOrHidePlayer(true);
        PlayerController.instance.changeLinePosition(false);

    }

    void showOrHidePlayer(bool show)
    {
        var children = GetComponentsInChildren<Transform>(includeInactive: true);
        foreach (Transform child in children)
        {
            if(!show)
            child.gameObject.layer = 9;
            else
                child.gameObject.layer = 0;
        }
    }
    IEnumerator movePlayerIfNotAlreadyMoving(GameObject objectToBeahveOn )
    {
       while(!finishedCheckingAllBehaviours)
        {
            yield return new WaitForEndOfFrame();
        }
       if(!behaviourIncludeMovement)
            PlayerController.instance.changeLinePosition(false);
       

    }
    IEnumerator eat(float x,GameObject objectToBehaveOn)
    {
        Vector3 actualPosition = transform.position;
        Vector3 targetPosition = new Vector3(x, actualPosition.y, actualPosition.z);
        PlayerController.instance.changeLinePosition(false);

        while (transform.position.x > x)
        {
            yield return new WaitForEndOfFrame();
        }
        animator.Play("grab");
        SpeakManager.instance.isPrinting = false;
        
        yield return new WaitForSeconds(1f);
        objectToBehaveOn.transform.parent = PlayerController.instance.hand;
        objectToBehaveOn.transform.localPosition = new Vector3(0, 0, 0);
        objectToBehaveOn.transform.localRotation = Quaternion.Euler(0, -90, -90);
        objectToBehaveOn.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        yield return new WaitForSeconds(3f);
        Destroy(objectToBehaveOn.gameObject);
        animator.Play("happy");
        SpeakManager.instance.Speak("Emmmm!", false, 0);
        SpeakManager.instance.Speak("Delecious", false, 0);
        yield return new WaitForSeconds(1f);
        PlayerController.instance.changeLinePosition(false);


    }
    IEnumerator drive(float x, GameObject objectToBehaveOn)
    {
       
        PlayerController.instance.changeLinePosition(false);

        while (transform.position.x > x)
        {
            yield return new WaitForEndOfFrame();
        }
        animator.Play("waitForLine");

        yield return new WaitForSeconds(2f);
        objectToBehaveOn.GetComponent<Rigidbody>().useGravity = false;
        objectToBehaveOn.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
      yield return new WaitForSeconds(2f);
        foreach (BoxCollider collider in objectToBehaveOn.GetComponentsInChildren<BoxCollider>())
           collider.isTrigger = true;
        animator.Play("enterCar");
        yield return new WaitForSeconds(5f);

       
        indexOfLine = 0;
        StartCoroutine(moveCar(objectToBehaveOn));


    }
    int indexOfLine = 0;
    IEnumerator moveCar(GameObject objectToBehaveOn)
    {
        string nameOfParentObject = objectToBehaveOn.name;
        string nameOfChildObject = "bottom";
        string childLocation = "/" + nameOfParentObject + "/" + nameOfChildObject;
        GameObject bottom = GameObject.Find(childLocation);
        Vector3 offesetCar = objectToBehaveOn.transform.position - bottom.transform.position;
        Vector3 offset = transform.position - objectToBehaveOn.transform.position;

        Vector3 actualScale = objectToBehaveOn.transform.localScale;
        Vector3 targetScale = new Vector3(actualScale.x +0.5f, actualScale.y + 0.5f, actualScale.z + 0.5f);
        float timeForAnim=0.5f;
        int counter = 0;

        float timeInSec = 0;
        while(indexOfLine < LineManager.instance.line.positionCount - 4)
        {
            if (timeInSec > timeForAnim)
            {
                timeInSec = 0;
                counter++;
            }
            if(counter%2==0)
            {
                objectToBehaveOn.transform.localScale = Vector3.Lerp(actualScale, targetScale, timeInSec / timeForAnim);
            }else
            {
                objectToBehaveOn.transform.localScale = Vector3.Lerp(targetScale, actualScale, timeInSec / timeForAnim);
            }




            if (objectToBehaveOn.transform.position.x > LineManager.instance.line.GetPosition(indexOfLine).x)
            {
                
                Vector3 targetposition = LineManager.instance.lineTransform.TransformPoint(LineManager.instance.line.GetPosition(indexOfLine));
                targetposition = new Vector3(targetposition.x, targetposition.y + offesetCar.y, targetposition.z);
                objectToBehaveOn.transform.position = Vector3.MoveTowards(objectToBehaveOn.transform.position, targetposition, 0.2f);
            }
            else 
            {
                indexOfLine++;
              
              
            }
            transform.position = objectToBehaveOn.transform.position+ offset;
            timeInSec += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
       
        transform.position =  LineManager.instance.lineTransform.TransformPoint(LineManager.instance.line.GetPosition(indexOfLine-1));
        PlayerController.instance.indexOfLine = indexOfLine-1;
        PlayerController.instance.changeLinePosition(false);





    }

}

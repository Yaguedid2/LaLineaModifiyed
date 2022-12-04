using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator animator;
    
    public bool walkAnimationStarted=false;
    public float stepVelocity = 0.3f;
  
    Vector3 PositionToStopWalking;
    Vector3 EndOfLinePosition;
    public bool addline=false;
    [HideInInspector]
    public static PlayerController instance;
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        animator = GetComponent < Animator>();
        changeLinePosition(true);
        SpeakManager.instance.Speak("Baby, calm down, calm down");
        SpeakManager.instance.Speak("Girl, this your body e put my heart for lockdown");
        SpeakManager.instance.Speak("For lockdown, oh, lockdown");
        SpeakManager.instance.Speak("Girl, you sweet like Fanta, Fanta");
        SpeakManager.instance.Speak("If I tell you say I love you no dey form yanga, oh, yanga");

    }

    float timeInSec = 0;
    void Update()
    {
        timeInSec += Time.deltaTime;
        move();
       
    }
    int indexOfLine = 1;
    int indexOfpointOnDrawing = 0;
    public float diff;
  
    public void move()
    {
        if (walkAnimationStarted )
        {
            if(!ObjectManager.instance.drawLine)
            {
                    indexOfpointOnDrawing = 0;
                
                    if (transform.position.x > LineManager.instance.line.GetPosition(indexOfLine).x)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, LineManager.instance.line.GetPosition(indexOfLine), stepVelocity);

                        transform.LookAt(LineManager.instance.line.GetPosition(indexOfLine));
                    }
                    else
                    {
                        if(indexOfLine< LineManager.instance.line.positionCount-1)
                        indexOfLine++;
                        else
                        {
                           
                            //EndOfTheLIne
                            animator.Play("waitForLine");
                            ObjectManager.instance.drawLine = false;
                           
                            ObjectManager.instance.drawingArea.SetActive(true);
                            ObjectManager.instance.okButton.SetActive(true);
                            ObjectManager.instance.clearButton.SetActive(true);
                            ObjectManager.instance.drawLineButton.SetActive(true);
                        }
                        LineManager.instance.lineCurviness = Mathf.Abs(Mathf.Abs(LineManager.instance.line.GetPosition(indexOfLine - 1).y) - Mathf.Abs(LineManager.instance.line.GetPosition(indexOfLine).y)) + 1;

                        timeInSec = 0;
                    }
               
            }else
            {
                Debug.Log(transform.position);
                    transform.position = Vector3.MoveTowards(transform.position, ObjectManager.instance.listOfImagePointsToWalkOn[indexOfpointOnDrawing].transform.position, stepVelocity);
                    transform.LookAt(ObjectManager.instance.listOfImagePointsToWalkOn[indexOfpointOnDrawing].transform.position);
                
                    
            }

               
         }
           
         
           
        
           

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "imagePoint")
        {
            if (indexOfpointOnDrawing < ObjectManager.instance.listOfImagePointsToWalkOn.Count - 1)
                indexOfpointOnDrawing++;
            else
                ObjectManager.instance.drawLine = false;


        }
        
    }

    
  

    public void changeLinePosition(bool firstTime)
    {
        indexOfLine = 0;
        float lineEnd = LineManager.instance.line.gameObject.transform.position.x + LineManager.instance.line.GetPosition(LineManager.instance.line.positionCount-1).x;

        EndOfLinePosition = new Vector3(lineEnd, transform.position.y, transform.position.z);
        PositionToStopWalking = EndOfLinePosition;
        if(!firstTime)
        animator.Play("walk");
    }
    
}

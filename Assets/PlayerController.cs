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

    // Update is called once per frame
    void Update()
    {
       
        move();
       
    }
  
    public void move()
    {
        if (walkAnimationStarted)
        {
            
           if(transform.position.x>PositionToStopWalking.x)
            {
                transform.position -= new Vector3(stepVelocity, 0, 0);
            }
            else
            {
                //EndOfTheLIne
                animator.Play("waitForLine");
                ObjectManager.instance.drawingArea.transform.position = new Vector3(transform.position.x-7.5f, 0, 0);
                ObjectManager.instance.drawingArea.SetActive(true);
                ObjectManager.instance.okButton.SetActive(true);
                ObjectManager.instance.clearButton.SetActive(true);


            }
         
           
        }
           

    }
    public void changeLinePosition(bool firstTime)
    {
        float lineEnd = LineManager.instance.line.gameObject.transform.position.x + LineManager.instance.line.GetPosition(1).x;
        EndOfLinePosition = new Vector3(lineEnd, transform.position.y, transform.position.z);
        PositionToStopWalking = new Vector3(lineEnd + transform.localScale.x + 2, transform.position.y, transform.position.z);
        if(!firstTime)
        animator.Play("walk");
    }
    
}

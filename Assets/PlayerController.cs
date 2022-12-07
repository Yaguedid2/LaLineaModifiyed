using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator animator;
    
    public bool walkAnimationStarted=false;
    public float stepVelocity = 0.3f;
    public GameObject globalRef;
    Vector3 PositionToStopWalking;
    Vector3 EndOfLinePosition;
    public bool addline=false;
    [HideInInspector]
    public static PlayerController instance;
    public GameObject whereToSee;
    public bool fall = false;
    BoxCollider playerCollider, fallingFromLineCollider;
    public LayerMask layerMask;
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        animator = GetComponent < Animator>();
        changeLinePosition(true);
        walkAnimationStarted = false;

        SpeakManager.instance.Speak("zaml");
        playerCollider = GetComponents<BoxCollider>()[1];
        fallingFromLineCollider = GetComponents<BoxCollider>()[0];
    }

    float timeInSec = 0;
    void Update()
    {
        float rot=calcRotation();
        if (rot < -66 || rot>66)       
            fall = true;
        if(fall)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, -100, 0), 0.1f);
            playerCollider.enabled = false;
            fallingFromLineCollider.enabled = true;
       
            return;
        }
           
       
           
        timeInSec += Time.deltaTime;
        move();
        //controleAnimation();
        

    }
    int indexOfLine = 1;
    int indexOfpointOnDrawing = 0;
    public float diff;
  


    void controleAnimation()
    {
       
    }


    float calcRotation()
    {
        if (transform.eulerAngles.x > 180)
        {
            return transform.eulerAngles.x - 360;
        }
        else
        {
          return transform.eulerAngles.x;
        }
    }

    public void move()
    {
        if (walkAnimationStarted )
        {
            if(!ObjectManager.instance.drawLine)
            {
                    indexOfpointOnDrawing = 0;
                
                    if (transform.position.x > LineManager.instance.line.GetPosition(indexOfLine).x)
                    {
                    Vector3 targetposition = LineManager.instance.line.GetPosition(indexOfLine);
                   
                    Debug.Log(targetposition);
                    var targetRotation = Quaternion.LookRotation(LineManager.instance.line.GetPosition(indexOfLine) - transform.position);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5 * Time.deltaTime);
                    transform.position = Vector3.MoveTowards(transform.position, targetposition, stepVelocity);
                    }
                    else
                    {
                        if(indexOfLine< LineManager.instance.line.positionCount-1)
                        indexOfLine++;
                        else
                        {
                        Debug.Log(indexOfLine);
                            //EndOfTheLIne
                            animator.Play("waitForLine");
                        
                        showDrawingArea();
                        }
                        LineManager.instance.lineCurviness = Mathf.Abs(Mathf.Abs(LineManager.instance.line.GetPosition(indexOfLine - 1).y) - Mathf.Abs(LineManager.instance.line.GetPosition(indexOfLine).y)) + 1;

                        timeInSec = 0;
                    }
               
            }else
            {
                Vector3 actualposition = transform.position;
                Vector3 targetposition =ObjectManager.instance.listOfImagePointsToWalkOn[indexOfpointOnDrawing].transform.position;
                Debug.Log(targetposition);
                transform.position = Vector3.MoveTowards(actualposition, targetposition, stepVelocity);

                var targetRotation = Quaternion.LookRotation(ObjectManager.instance.listOfImagePointsToWalkOn[indexOfpointOnDrawing].transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5 * Time.deltaTime);

               


            }

               
         }
           
         
           
        
           

    }
    private static float WrapAngle(float angle)
    {
        angle %= 360;
        if (angle > 180)
            return angle - 360;

        return angle;
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
        walkAnimationStarted = true;
    }
    void showDrawingArea()
    {
        ObjectManager.instance.drawLine = false;
        ObjectManager.instance.drawingArea.SetActive(true);
        ObjectManager.instance.okButton.SetActive(true);
        ObjectManager.instance.clearButton.SetActive(true);
        ObjectManager.instance.drawLineButton.SetActive(true);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag=="line")
        {
            fall = false;
            Debug.Log("9lwa");
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX;
            transform.rotation = Quaternion.Euler(0, -90, 0);
            GetComponent<Animator>().Play("hang");
           
        }
      
    }
    private void FixedUpdate()
    {
      
    }

}

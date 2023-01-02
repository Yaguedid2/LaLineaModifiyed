using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Animator animator;

    public bool walkAnimationStarted = false;
    public float stepVelocity = 0.3f;
    public GameObject globalRef;
    Vector3 PositionToStopWalking;
    public Transform hand;
    Vector3 EndOfLinePosition;
    public bool addline = false;
    [HideInInspector]
    public static PlayerController instance;
    public GameObject whereToSee;
    public bool fall = false;
    public Cloth flag;
    CapsuleCollider playerCollider;
    public BoxCollider fallingFromLineCollider;
    public LayerMask layerMask;

    private void Awake()
    {
        instance = this;
    }
    Vector3 offsetBetweenBodyAndHead;
    void Start()
    {
        if(DontDestroy.playerPosition !=Vector3.zero)
        {
            transform.position = DontDestroy.playerPosition;
            transform.rotation = DontDestroy.playerRotation;
        }

        StartCoroutine(resetClothes());
        SpeakManager.instance.Speak("Oh Shit ", false,2);
        SpeakManager.instance.Speak("Here We Go ", false,2);
        SpeakManager.instance.Speak("Again ", false,2);
        oneTime = true;

       

    }
    void firstStep()
    {
        animator = GetComponent<Animator>();
        changeLinePosition(true);
        walkAnimationStarted = false;
        offsetBetweenBodyAndHead = transform.position - fallingFromLineCollider.transform.position;
        animator.Play("walk");
        playerCollider = GetComponent<CapsuleCollider>();

    }
    bool oneTime = true;


    float timeInSec = 0;
    Vector3 oldPlayerPsoition;
    float rot;
    void Update()
    {
        if (oneTime && GameManager.instance.GameLoaded)
        {
            oneTime = false;
            StartCoroutine(firstWalk());
        }
        else if(!GameManager.instance.GameLoaded)
            return;
        if (!fall)
            rot = calcRotation();
        if (rot < -66 || rot > 66 && !fall)
        {
            fall = true;
            oldPlayerPsoition = transform.position;




        }
        if (fall)
        {
            if (rot < -66)
                Fall(true);
            if (rot > 66)
                Fall(false);
            return;
        }




        timeInSec += Time.deltaTime;
        move();



    }
    public int indexOfLine = 1, indexOfCorrLine = 0;

    int indexOfpointOnDrawing = 0;
    public float diff;



    void Fall(bool fallRight)
    {
        GetComponent<Animator>().Play("fall");
        if (fallRight)
        {
            Debug.Log("right");
            transform.rotation = Quaternion.Euler(new Vector3(-42.615f, 76.811f, -152.447f));
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x + 10, -100, 0), 0.1f);
        }

        else
        {
            Debug.Log("left");
            transform.rotation = Quaternion.Euler(new Vector3(-42.615f, -76.811f, 152.447f));
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(PositionToStopWalking.x, -100, 0), 0.1f);
        }


        playerCollider.enabled = false;
        fallingFromLineCollider.enabled = true;
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
        if (walkAnimationStarted)
        {
            if (!ObjectManager.instance.drawLine)
            {


                if (transform.position.x > LineManager.instance.line.GetPosition(indexOfLine).x)
                {
                    Vector3 targetposition = LineManager.instance.lineTransform.TransformPoint(LineManager.instance.line.GetPosition(indexOfLine));


                    var targetRotation = Quaternion.LookRotation(targetposition - transform.position);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5 * Time.deltaTime);
                    transform.position = Vector3.MoveTowards(transform.position, targetposition, stepVelocity);
                }
                else
                {
                    if (indexOfLine < LineManager.instance.line.positionCount - 1)
                        indexOfLine++;
                    else
                    {

                        //EndOfTheLIne
                        animator.Play("waitForLine");
                        SpeakManager.instance.isPrinting = false;
                        ObjectManager.instance.drawAnObject.SetActive(true);
                        ObjectManager.instance.drawLineButton.SetActive(true);
                        ObjectManager.instance.panelOfDrawingChoices.SetActive(true);
                        Cursor.lockState = CursorLockMode.None;
                        //showDrawingArea();
                    }
                    LineManager.instance.lineCurviness = Mathf.Abs(Mathf.Abs(LineManager.instance.line.GetPosition(indexOfLine - 1).y) - Mathf.Abs(LineManager.instance.line.GetPosition(indexOfLine).y)) + 1;

                    timeInSec = 0;
                }

            } else
            {
                if (indexOfCorrLine<4 &&  transform.position.x > ObjectManager.instance.corrLine.GetPosition(indexOfCorrLine).x)
                {
                    Vector3 targetpositionInCOrr = LineManager.instance.lineTransform.TransformPoint(ObjectManager.instance.corrLine.GetPosition(indexOfCorrLine));


                    var targetRotationInCorr = Quaternion.LookRotation(targetpositionInCOrr - transform.position);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotationInCorr, 5 * Time.deltaTime);
                    transform.position = Vector3.MoveTowards(transform.position, targetpositionInCOrr, stepVelocity);
                } else if (indexOfCorrLine < 4)
                {
                    indexOfCorrLine += 3;
                } else
                {
                    
                    Vector3 actualposition = transform.position;
                    Vector3 targetposition = ObjectManager.instance.listOfImagePointsToWalkOn[indexOfpointOnDrawing].transform.position;

                    transform.position = Vector3.MoveTowards(actualposition, targetposition, stepVelocity);

                    var targetRotation = Quaternion.LookRotation(ObjectManager.instance.listOfImagePointsToWalkOn[indexOfpointOnDrawing].transform.position - transform.position);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5 * Time.deltaTime);
                }





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
    IEnumerator firstWalk()
    {
        if(DontDestroy.playerPosition!=Vector3.zero)
        yield return new WaitForSeconds(3f);
        firstStep();
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
        if(other.tag=="Question")
        {


            other.GetComponent<BoxCollider>().enabled = false;
          
            DontDestroy.linePositions = LineManager.instance.listOfLinePositions.ToArray();
            DontDestroy.indexOfline = indexOfLine;
            DontDestroy.playerPosition = transform.position;
            DontDestroy.playerRotation = transform.rotation;
           
          

            SceneManager.LoadScene("miniGame", LoadSceneMode.Single);
        }


    }

    IEnumerator resetClothes()
    {
        flag.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.03f);
        flag.gameObject.SetActive(true);
    }


    public void changeLinePosition(bool firstTime)
    {
        ///hna
        if (DontDestroy.indexOfline != -1)
            indexOfLine = DontDestroy.indexOfline;
        else
        indexOfLine = 0;
        indexOfCorrLine = 0;
        indexOfpointOnDrawing = 0;
        fallingFromLineCollider.enabled = false;
        float lineEnd = LineManager.instance.line.gameObject.transform.position.x + LineManager.instance.line.GetPosition(LineManager.instance.line.positionCount - 1).x;

        EndOfLinePosition = new Vector3(lineEnd, transform.position.y, transform.position.z);
        PositionToStopWalking = EndOfLinePosition;
        if (!firstTime)
            animator.Play("walk");
        walkAnimationStarted = true;
    }
    void showDrawingArea()
    {
        ObjectManager.instance.drawLine = false;
        ObjectManager.instance.drawingArea.SetActive(true);
        ObjectManager.instance.drawAnObject.SetActive(true);
        ObjectManager.instance.drawLineButton.SetActive(true);
       ObjectManager.instance.duplicateOfDrawArea.SetActive(true);
        Hand.instance.showHand(true);
        foreach(Object g in FindObjectsOfType<Object>())
            Destroy(g.gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "line" && !alreadyHang)
            StartCoroutine(fallingAndStanding(collision.contacts[0].point, rot < -66));

    }

    bool alreadyHang = false;
    public Vector3 positionWhereToStandAfterFall;
    IEnumerator fallingAndStanding(Vector3 collisionPoint, bool fallRight)
    {
        alreadyHang = true;
        positionWhereToStandAfterFall = collisionPoint;
        fall = false;
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        transform.rotation = Quaternion.Euler(0, -78, 0);

        transform.position = collisionPoint + offsetBetweenBodyAndHead;
        GetComponent<Animator>().Play("hang");
        yield return new WaitForSeconds(3);
        GetComponent<Animator>().Play("standAfterHang");
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationY;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ;

        yield return new WaitForSeconds(3);
        playerCollider.enabled = true;
       
        if (fallRight)
        {
            if(jumpOrErase)
            {
                Vector3 targetposition = LineManager.instance.lineTransform.TransformPoint(LineManager.instance.line.GetPosition(0));
                transform.position = transform.position + new Vector3(0, 2, 0);
                GetComponent<Animator>().Play("jump");

                yield return new WaitForSeconds(1.1f);
                transform.position = targetposition;
                ObjectManager.instance.drawLine = false;
                changeLinePosition(false);
            }
            else
            {
                LineManager.instance.eraseAndReDraw();
                showDrawingArea();
            }
            //you can JUm Also
           
        } else
        {
            ObjectManager.instance.drawLine = false;
            changeLinePosition(false);

        }
        alreadyHang = false;



    }
    bool jumpOrErase = true;
       public void _jump()
        {
        jumpOrErase = true;
        }
    
}

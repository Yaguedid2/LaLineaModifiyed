using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ObjectManager : MonoBehaviour
{
    public GameObject DrawingFrame;
    public GameObject okButton, clearButton, drawLineButton;
    public static ObjectManager instance;
    public GameObject drawingArea,realDrawingArea;
    public Sprite drawingImage;
    public PhysicsMaterial2D PhysicsMaterial2D;
     public bool drawLine = false;
    public Vector2[] drawingPoints;
    public LineRenderer correspandence;
    public List<GameObject> listOfImagePointsToWalkOn = new List<GameObject>();
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    int nbrOfSpawns = 1;
    public void instantiateObject(List<string> listOfPossiblities)
    {
        listOfImagePointsToWalkOn.Clear();
        AssetDatabase.Refresh();
        GameObject image = new GameObject();
        image.transform.position =new Vector3(realDrawingArea.transform.position.x, realDrawingArea.transform.position.y,0) ;
        image.AddComponent<SpriteRenderer>();

       
       
        image.AddComponent<EdgeCollider2D>();
        //build road to walk on
        foreach(Vector2 point in DrawingToJson.instance.imagePoints)
        {
            GameObject imagepoint = new GameObject();
            imagepoint.transform.parent = image.transform;
            imagepoint.AddComponent<BoxCollider>();
            imagepoint.GetComponent<BoxCollider>().isTrigger = true;
            imagepoint.GetComponent<BoxCollider>().size = new Vector3(0.05f, 0.05f,0.05f);
            imagepoint.tag = "imagePoint";
            imagepoint.transform.localPosition = point;
            listOfImagePointsToWalkOn.Add(imagepoint);

        }
        listOfImagePointsToWalkOn.Reverse();
        //
        image.GetComponent<EdgeCollider2D>().points = DrawingToJson.instance.imagePoints.ToArray();
        drawingPoints = DrawingToJson.instance.imagePoints.ToArray();
        GameObject leftCorner = new GameObject();

        leftCorner.transform.parent = image.transform;
        GameObject rightCorner = new GameObject();
        rightCorner.transform.parent = image.transform;
        leftCorner.name = "leftCorner";

        rightCorner.name = "rightCorner";
        if (DrawingToJson.instance.imagePoints.ToArray()[0].x< DrawingToJson.instance.imagePoints.ToArray()[DrawingToJson.instance.imagePoints.ToArray().Length - 1].x)
        {
            leftCorner.transform.localPosition = DrawingToJson.instance.imagePoints.ToArray()[0];
            rightCorner.transform.localPosition = DrawingToJson.instance.imagePoints.ToArray()[DrawingToJson.instance.imagePoints.ToArray().Length - 1];
            
        }
        else
        {
            leftCorner.transform.localPosition = DrawingToJson.instance.imagePoints.ToArray()[DrawingToJson.instance.imagePoints.ToArray().Length - 1];
            rightCorner.transform.localPosition = DrawingToJson.instance.imagePoints.ToArray()[0];
            listOfImagePointsToWalkOn.Reverse();
        }
        
      

        image.GetComponent<EdgeCollider2D>().sharedMaterial = PhysicsMaterial2D;
       
       image.transform.localScale = new Vector3(10, 10, 1);
        image.GetComponent<SpriteRenderer>().sprite = drawingImage;
       
      //  image.transform.position = new Vector3(image.transform.position.x, image.transform.position.y, 0);
        //if draw LIne
        if(drawLine)
        {

            LineRenderer corr = Instantiate(correspandence);
            corr.SetPosition(0, PlayerController.instance.gameObject.transform.position);
            corr.SetPosition(1, rightCorner.transform.position);
            Destroy(corr, 7);
            LineManager.instance.start = leftCorner.transform.position;
        
        }
        else
        {
            LineManager.instance.start = PlayerController.instance.gameObject.transform.position;
            image.AddComponent<Rigidbody2D>();
        }

        //
        //PlayerController.instance.gameObject.transform.position = LineManager.instance.line.GetPosition(LineManager.instance.line.positionCount - 1);
        DrawingToJson.instance.imagePoints.Clear();
       
        nbrOfSpawns++;
        
    }
    public void _drawLine()
    {
        drawLine = true;
        drawLineButton.SetActive(false);
    }
}

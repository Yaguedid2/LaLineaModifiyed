using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class ObjectManager : MonoBehaviour
{
    public GameObject DrawingFrame;
    public Material lineDrawingMaterial;
    public GameObject okButton, clearButton, drawLineButton;
    public static ObjectManager instance;
    public GameObject drawingArea,realDrawingArea;
    public Sprite drawingImage;
    public PhysicsMaterial2D PhysicsMaterial2D;
     public bool drawLine = false;
    public Vector3[] drawingPoints;
    public LineRenderer correspandence;
    public List<GameObject> listOfImagePointsToWalkOn = new List<GameObject>();
    public Transform drawingTransform;
    public LineRenderer corrLine;
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
    public void instantiateObject(string type,int index)
    {
        listOfImagePointsToWalkOn.Clear();
       
        GameObject image = new GameObject();
        image.name = "Object";
        drawingTransform = image.transform;
        image.transform.position =new Vector3(realDrawingArea.transform.position.x, realDrawingArea.transform.position.y,0);    

        //image.AddComponent<EdgeCollider2D>();
        //build road to walk on
        
        int i = 0;
       
        GameObject line = new GameObject();
        line.name = "line";
        line.transform.parent = image.transform;
        line.AddComponent<LineRenderer>();
       

        for (int c=1;c< DrawingToJson.instance.strokeIndexes.Count;c++)
        {
            GameObject subLine = new GameObject();
            subLine.name = "subLine";
            subLine.transform.parent = image.transform;
            subLine.AddComponent<LineRenderer>();
            
            subLine.GetComponent<LineRenderer>().useWorldSpace = false;
            subLine.GetComponent<LineRenderer>().material = lineDrawingMaterial;
           
            if (c< DrawingToJson.instance.strokeIndexes.Count-1)
            subLine.GetComponent<LineRenderer>().positionCount = DrawingToJson.instance.strokeIndexes[c+1]- DrawingToJson.instance.strokeIndexes[c];
            else
                subLine.GetComponent<LineRenderer>().positionCount =  DrawingToJson.instance.strokeIndexes[c];
        }
        line.GetComponent<LineRenderer>().positionCount = DrawingToJson.instance.strokeIndexes[1];
       
        int counter = 0;
        int indexOfStroke = 1;
        int positionInStroke = 0;
        foreach (Vector2 point in DrawingToJson.instance.imagePoints)
        {
            if (counter >= DrawingToJson.instance.strokeIndexes[indexOfStroke])
            {
                indexOfStroke++;
                positionInStroke = 0;
            }
            GameObject imagepoint = new GameObject();
            imagepoint.name = "point";
            imagepoint.transform.parent = image.transform;
            imagepoint.AddComponent<BoxCollider>();
            if(drawLine)
            imagepoint.GetComponent<BoxCollider>().isTrigger = true;
            else
                imagepoint.GetComponent<BoxCollider>().isTrigger = false;
            imagepoint.GetComponent<BoxCollider>().size = new Vector3(0.05f, 0.05f,0.05f);
            imagepoint.tag = "imagePoint";
            imagepoint.transform.localPosition = point;
            listOfImagePointsToWalkOn.Add(imagepoint);
            if (drawLine)
                image.GetComponentsInChildren<LineRenderer>()[indexOfStroke - 1].startWidth = 0.8f;
            else
                image.GetComponentsInChildren<LineRenderer>()[indexOfStroke - 1].startWidth = 0.3f;
            image.GetComponentsInChildren<LineRenderer>()[indexOfStroke-1].SetPosition(positionInStroke,imagepoint.transform.position);

            line.GetComponent<LineRenderer>().useWorldSpace = false;
            line.GetComponent<LineRenderer>().material = lineDrawingMaterial;
           
            positionInStroke++;
            counter++;
        }
        listOfImagePointsToWalkOn.Reverse();
        //
        //image.GetComponent<EdgeCollider2D>().points = DrawingToJson.instance.imagePoints.ToArray();

        //image.GetComponent<EdgeCollider2D>().sharedMaterial = PhysicsMaterial2D;

        drawingPoints = DrawingToJson.instance.imagePoints.ToArray();
        GameObject leftCorner = new GameObject();

        leftCorner.transform.parent = image.transform;
        GameObject rightCorner = new GameObject();
        

        rightCorner.transform.parent = image.transform;
        leftCorner.name = "leftCorner";

        rightCorner.name = "rightCorner";
        GameObject top = new GameObject();
        top.name = "top";
        top.transform.parent = image.transform;
        top.transform.localPosition = DrawingToJson.instance.imagePoints.ToArray()[DrawingToJson.instance.indexOfMaxY];
        GameObject realRight = new GameObject();
        realRight.name = "realRight";
        realRight.transform.parent = image.transform;
        realRight.transform.localPosition = DrawingToJson.instance.imagePoints.ToArray()[DrawingToJson.instance.indexOfMaxX];
        GameObject realLeft = new GameObject();
        realLeft.name = "realLeft";
        realLeft.transform.parent = image.transform;
        realLeft.transform.localPosition = DrawingToJson.instance.imagePoints.ToArray()[DrawingToJson.instance.indexOfMinX];
        GameObject bottom = new GameObject();
        bottom.name = "bottom";
        bottom.transform.parent = image.transform;
        bottom.transform.localPosition = DrawingToJson.instance.imagePoints.ToArray()[DrawingToJson.instance.indexOfMinY];

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
             
       image.transform.localScale = new Vector3(11, 11, 1);
        //image.GetComponent<SpriteRenderer>().sprite = drawingImage;


        ObjectManager.instance.okButton.SetActive(false);
        ObjectManager.instance.clearButton.SetActive(false);
        ObjectManager.instance.drawingArea.SetActive(false);
        ObjectManager.instance.drawLineButton.SetActive(false);
        DrawingToJson.instance.imagePoints.Clear();
        DrawingToJson.instance.indexOfMaxX = 0;
        DrawingToJson.instance.indexOfMaxY = 0;
        DrawingToJson.instance.indexOfMinX = 0;
        DrawingToJson.instance.indexOfMinY = 0;
        DrawingToJson.instance.canUpdateMinMax = true;
        StrokeManager.instance.clearAll();
        
        //  image.transform.position = new Vector3(image.transform.position.x, image.transform.position.y, 0);
        //if draw LIne
        if (drawLine)
        {

            LineRenderer corr = Instantiate(correspandence);
            corrLine = corr;
            Vector3 e = corr.gameObject.transform.InverseTransformPoint(rightCorner.transform.position);

            Vector3 s = LineManager.instance.listOfLineEnds[LineManager.instance.listOfLineEnds.Count - 1];

            corr.SetPosition(0, new Vector3(s.x, s.y,s.z));
            corr.SetPosition(1, new Vector3(s.x, s.y, s.z));
            corr.SetPosition(2, new Vector3(s.x , s.y, s.z));
            corr.SetPosition(3, new Vector3(e.x, e.y, e.z));
            
            //corr.gameObject.GetComponent<MeshCollider>().convex = true;
            
            // Destroy(corr, 7);
            LineManager.instance.start = leftCorner.transform.localPosition;
            image.name = "line";
        }
        else
        {
            Vector3 spawnPos = Camera.main.transform.position;
            image.transform.position = new Vector3(PlayerController.instance.gameObject.transform.position.x-5,spawnPos.y+10, PlayerController.instance.gameObject.transform.position.z);

            image.AddComponent<Rigidbody>();
            image.AddComponent<Object>();
            image.name = type+nbrOfSpawns;
            image.GetComponent<Object>().setClassName(type);
            image.GetComponent<Object>().setIndex(index);
            ObjectBehaviorManager.instance.behave(image.GetComponent<Object>());
        }
        if (!drawLine && ObjectTypeHandler.instance.objects[image.GetComponent<Object>().index].objectCategories.drive)
            LineManager.instance.drawLine(false,150) ;
        else
            LineManager.instance.drawLine(false, 30);
        nbrOfSpawns++;
       
        
    }
    public void _drawLine()
    {
        drawLine = true;
        drawLineButton.SetActive(false);
    }
}

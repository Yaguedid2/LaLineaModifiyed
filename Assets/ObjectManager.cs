using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

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
        image.transform.position =new Vector3(realDrawingArea.transform.position.x, realDrawingArea.transform.position.y,0);    
        //image.AddComponent<EdgeCollider2D>();
        //build road to walk on
        
        int i = 0;
       
        GameObject line = new GameObject();
        line.name = "line";
        line.transform.parent = image.transform;
        line.AddComponent<LineRenderer>();
        line.GetComponent<LineRenderer>().positionCount = DrawingToJson.instance.imagePoints.Count;
        foreach (Vector2 point in DrawingToJson.instance.imagePoints)
        {
            GameObject imagepoint = new GameObject();
            imagepoint.name = "point";
            imagepoint.transform.parent = image.transform;
            imagepoint.AddComponent<BoxCollider>();
            imagepoint.GetComponent<BoxCollider>().isTrigger = true;
            imagepoint.GetComponent<BoxCollider>().size = new Vector3(0.05f, 0.05f,0.05f);
            imagepoint.tag = "imagePoint";
            imagepoint.transform.localPosition = point;
            listOfImagePointsToWalkOn.Add(imagepoint);
            line.GetComponent<LineRenderer>().SetPosition(i,imagepoint.transform.position);
            line.GetComponent<LineRenderer>().useWorldSpace = false;
            line.GetComponent<LineRenderer>().material = lineDrawingMaterial;
            line.GetComponent<LineRenderer>().startWidth = 0.2f;
            i++;
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
             
       image.transform.localScale = new Vector3(10, 10, 1);
        //image.GetComponent<SpriteRenderer>().sprite = drawingImage;
      
        
       
      //  image.transform.position = new Vector3(image.transform.position.x, image.transform.position.y, 0);
        //if draw LIne
        if(drawLine)
        {
            Debug.Log(rightCorner.transform.localPosition);
            LineRenderer corr = Instantiate(correspandence);
            Vector3 e = corr.gameObject.transform.InverseTransformPoint(rightCorner.transform.position);
            Debug.Log(e);
            Vector3 s = LineManager.instance.listOfLineEnds[LineManager.instance.listOfLineEnds.Count - 1];
            Debug.Log(s);
            corr.SetPosition(0, new Vector3(s.x, -4.92f,s.z));
            corr.SetPosition(1, new Vector3(s.x,-4.92f, s.z));
            corr.SetPosition(2, new Vector3(s.x , -4.92f, s.z));
            corr.SetPosition(3, new Vector3(e.x, -4.92f,e.z));
            
            //corr.gameObject.GetComponent<MeshCollider>().convex = true;
            
            // Destroy(corr, 7);
            LineManager.instance.start = leftCorner.transform.position;
        
        }
        else
        {
            LineManager.instance.start = PlayerController.instance.gameObject.transform.position;
            image.AddComponent<Rigidbody2D>();
        }

        DrawingToJson.instance.imagePoints.Clear();
       
        nbrOfSpawns++;
       
        
    }
    public void _drawLine()
    {
        drawLine = true;
        drawLineButton.SetActive(false);
    }
}

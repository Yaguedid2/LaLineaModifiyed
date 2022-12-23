using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour
{
    public LineRenderer line;
    public GameObject lineObject;
    public Transform lineTransform;
    public static LineManager instance;
    public float lineCurviness = 1;
    public int edgeOfline;
    public Vector3 start,end;
    float distanceBetweenLinePoints = 10;
    public List<Vector3> listOfLineEnds = new List<Vector3>();
   
    private void Awake()
    {
        instance = this;
       
    }

     void Start()
    {
        //edgeOfline = 2;
        drawLine(true,30);
    }
    bool firstTime = true;
    public void drawLine(bool firstTime,float distance)
    {
        
        if (firstTime)
        {
           GameObject l= Instantiate(lineObject);
            lineTransform = l.transform;
            line = l.GetComponent<LineRenderer>();
            //  Mesh mesh = new Mesh();
            // l.GetComponent<LineRenderer>().BakeMesh(mesh);
            //l.GetComponent<MeshCollider>().sharedMesh = mesh;
            //l.GetComponent<MeshCollider>().convex = true;
            //edgeOfline = 1;
        }
        else
        {
            //edgeOfline = line.positionCount - 1;
            LineRenderer oldLine = line;
            GameObject l = Instantiate(lineObject);
            lineTransform = l.transform;
            if(ObjectManager.instance.drawLine)
            {
                Vector3 s = ObjectManager.instance.drawingTransform.TransformPoint(start);
                start = lineTransform.InverseTransformPoint(s);
            }
            else
            {
                start = oldLine.GetPosition(oldLine.positionCount - 1);
            }
            
            l.GetComponent<LineRenderer>().SetPosition(0, new Vector3(start.x, start.y,start.z));
            float y;
            if(ObjectManager.instance.drawLine)
             y = Random.Range(-10f, 10f);
            else
              y = Random.Range(-2f, 2f);
            l.GetComponent<LineRenderer>().SetPosition(1, start-new Vector3(distance/3, 0, y));
            l.GetComponent<LineRenderer>().SetPosition(2, start - new Vector3(distance/2, 0, y));
            l.GetComponent<LineRenderer>().SetPosition(3, start - new Vector3(distance, 0, y));

           
            line = l.GetComponent<LineRenderer>();
            if(ObjectManager.instance.drawLine)
            {
                PlayerController.instance.changeLinePosition(false);
            }
           
            //Destroy(oldLine, 7);

            //line.SetPosition(0, new Vector3(line.GetPosition(line.positionCount - 1).x + 5, line.GetPosition(line.positionCount - 1).y, line.GetPosition(line.positionCount - 1).z));


            /*
            line.SetPosition(0, new Vector3(start.x,start.y, line.GetPosition(0).z));
            line.SetPosition(1, new Vector3(line.GetPosition(0).x - 5, line.GetPosition(0).y, line.GetPosition(0).z));
        
        
         line.SetPosition(2, new Vector3(line.GetPosition(1).x, line.GetPosition(1).y, line.GetPosition(1).z));
         line.SetPosition(3, new Vector3(line.GetPosition(2).x-distanceBetweenLinePoints, Random.Range(line.GetPosition(1).y+0.5f, 10), line.GetPosition(1).z));
            */

            //Mesh mesh = new Mesh();

            //line.BakeMesh(mesh);
            //line.gameObject.GetComponent<MeshCollider>().sharedMesh = mesh;


            //line.gameObject.GetComponent<MeshCollider>().sharedMesh = mesh;
            //line.gameObject.GetComponent<MeshCollider>().convex = true;
           
        }
    }
    public void eraseAndReDraw()
    {
        listOfLineEnds.RemoveAt(listOfLineEnds.Count - 1);
        Destroy(lineTransform.gameObject);
        Destroy(ObjectManager.instance.drawingTransform.gameObject);
    }
}

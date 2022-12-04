using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour
{
    public LineRenderer line;
    public static LineManager instance;
    public float lineCurviness = 1;
    public int edgeOfline;
    public Vector3 start,end;
    float distanceBetweenLinePoints = 10; 
    private void Awake()
    {
        instance = this;
       
    }

     void Start()
    {
        edgeOfline = 2;
    }
    bool firstTime = true;
    public void drawLine()
    {
        firstTime = false;
        if (firstTime)
        {
            line.SetPosition(1, new Vector3(line.GetPosition(0).x - 5, line.GetPosition(0).y, line.GetPosition(0).z));
            edgeOfline = 1;
        }
        else
        {
            edgeOfline = line.positionCount - 1;
            LineRenderer oldLine = line;
            line = Instantiate(line);
            //Destroy(oldLine, 7);

            //line.SetPosition(0, new Vector3(line.GetPosition(line.positionCount - 1).x + 5, line.GetPosition(line.positionCount - 1).y, line.GetPosition(line.positionCount - 1).z));
            line.SetPosition(0, new Vector3(start.x,start.y, line.GetPosition(0).z));
            line.SetPosition(1, new Vector3(line.GetPosition(0).x - 5, line.GetPosition(0).y, line.GetPosition(0).z));
        }
        
        line.SetPosition(2, new Vector3(line.GetPosition(1).x, line.GetPosition(1).y, line.GetPosition(1).z));
      line.SetPosition(3, new Vector3(line.GetPosition(2).x-distanceBetweenLinePoints, Random.Range(line.GetPosition(1).y+0.5f, 10), line.GetPosition(1).z));
      line.SetPosition(4, new Vector3(line.GetPosition(3).x, line.GetPosition(3).y, line.GetPosition(1).z));
      line.SetPosition(5, new Vector3(line.GetPosition(4).x - distanceBetweenLinePoints, Random.Range(line.GetPosition(1).y + 0.5f, 10), line.GetPosition(1).z));
        line.SetPosition(6, new Vector3(line.GetPosition(5).x, line.GetPosition(5).y, line.GetPosition(1).z));
        line.SetPosition(7, new Vector3(line.GetPosition(6).x-distanceBetweenLinePoints, line.GetPosition(1).y, line.GetPosition(1).z));

        PlayerController.instance.changeLinePosition(false);
    }
}

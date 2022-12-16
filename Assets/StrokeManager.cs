using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrokeManager : MonoBehaviour
{
   public  DrawMesh stroke;
    List<DrawMesh> listOfStrokes=new List<DrawMesh>();
    public static StrokeManager instance;
   
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        createStroke();
    }

    // Update is called once per frame
    void Update()
    {
        if (listOfStrokes[index].strokeCreated && strokeCreated)
        {
           
            createStroke();
        }
    }
    int index=-1;
    bool strokeCreated = true;
    public void createStroke()
    {
        strokeCreated = false;
       DrawMesh s=Instantiate(stroke);
        s.transform.parent = transform;
        listOfStrokes.Add(s);
        index++;
        strokeCreated = true;
    }
    public void clearAll()
    {
        foreach (DrawMesh s in listOfStrokes)
        {
           
            Destroy(s.gameObject);
        }
           
        listOfStrokes.Clear();
        index = -1;
        createStroke();
        DrawingToJson.instance.imageStrokes.Clear();
        DrawingToJson.instance.imagePoints.Clear();
        DrawingToJson.instance.strokeIndexes.Clear();
      
    }
}

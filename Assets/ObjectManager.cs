using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ObjectManager : MonoBehaviour
{
    public GameObject DrawingFrame;
    public GameObject okButton, clearButton;
    public static ObjectManager instance;
    public GameObject drawingArea,realDrawingArea;
    public Sprite drawingImage;
    public PhysicsMaterial2D PhysicsMaterial2D;
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
        AssetDatabase.Refresh();
        GameObject image = new GameObject();
        image.transform.position = realDrawingArea.transform.position;
        image.AddComponent<SpriteRenderer>();

        image.AddComponent<Rigidbody2D>();
       
        image.AddComponent<EdgeCollider2D>();
        image.GetComponent<EdgeCollider2D>().points = DrawingToJson.instance.imagePoints.ToArray();
        image.GetComponent<EdgeCollider2D>().sharedMaterial = PhysicsMaterial2D;
        image.transform.localScale = new Vector3(10, 10, 1);
        image.GetComponent<SpriteRenderer>().sprite = drawingImage;
        DrawingToJson.instance.imagePoints.Clear();
        nbrOfSpawns++;
        
    }
}

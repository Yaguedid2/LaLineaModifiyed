using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DrawMesh : MonoBehaviour {

    public GameObject drawArea;
    public Material drawingMaterial;
    public float lineThickness = 1f;
    public bool canvasFull = false;
    public LayerMask layerMask;
    public bool strokeCreated = false;
    public bool canDraw = true;
    private Mesh mesh;
    public GameObject stroke;
    private Vector3 lastMousePosition;
    float minx=0, max=0,miny=0,maxy=0;
    public List<float> strokePixelsX = new List<float>();
    public List<float> strokePixelsY = new List<float>();
  
    public List<List<float>> strokePixelsXY =new List<List<float>>();

    private void Awake() {
        canvasFull = false;
    }
    private void Start()
    {
        canvasFull = false;
        strokeCreated = false;
        canDraw = true;
        drawArea = ObjectManager.instance.realDrawingArea;
    }
    bool firstClick = false;
    /// <summary>
    /// 
    /// </summary>



    /// <summary>
    /// 
    /// </summary>
    ///
    int indexOfPoint = 0;
    private void Update() {

        //Debug.Log(Mathf.Abs(Mathf.Floor(getPoistionOnDrawArea().x * 55.5f)) + "  "+ Mathf.Abs(Mathf.Floor(getPoistionOnDrawArea().y * 55.5f)));
        checkIfInDrawArea();
        if (strokeCreated)
            return;
       
      
        if (Input.GetMouseButtonDown(0) && !canvasFull) {

            // Mouse Pressed
            if (!checkIfInDrawArea())
                return;
            mesh = new Mesh();
            firstClick = true;
           DrawingToJson.instance.mousePosition[0] =Mathf.Abs(Mathf.Floor(getPoistionOnDrawArea().x*10));
            DrawingToJson.instance.mousePosition[1] =Mathf.Abs(Mathf.Floor(getPoistionOnDrawArea().y*10));
            Vector3[] vertices = new Vector3[4];
            Vector2[] uv = new Vector2[4];
            int[] triangles = new int[6];

            vertices[0] = getMouseWorlPosition();
            vertices[1] = getMouseWorlPosition();
            vertices[2] = getMouseWorlPosition();
            vertices[3] = getMouseWorlPosition();
            minx = max = ObjectManager.instance.realDrawingArea.transform.InverseTransformPoint(getMouseWorlPosition()).x;
            miny = maxy = ObjectManager.instance.realDrawingArea.transform.InverseTransformPoint(getMouseWorlPosition()).y;

            uv[0] = Vector2.zero;
            uv[1] = Vector2.zero;
            uv[2] = Vector2.zero;
            uv[3] = Vector2.zero;

            triangles[0] = 0;
            triangles[1] = 3;
            triangles[2] = 1;

            triangles[3] = 1;
            triangles[4] = 3;
            triangles[5] = 2;

            mesh.vertices = vertices;
            mesh.uv = uv;
            mesh.triangles = triangles;
            mesh.MarkDynamic();
            
            GetComponent<MeshFilter>().mesh = mesh;
            GetComponent<MeshRenderer>().material = drawingMaterial;
            lastMousePosition = getMouseWorlPosition();
        }



        if(firstClick)
        if (Input.GetMouseButton(0) && canDraw) {
            canvasFull = true;
                if (!checkIfInDrawArea())
                    return;
                // Mouse held down
                float minDistance = .1f;
            if (Vector3.Distance(getMouseWorlPosition(), lastMousePosition) > minDistance) {
                Vector3[] vertices = new Vector3[mesh.vertices.Length + 2];
                Vector2[] uv = new Vector2[mesh.uv.Length + 2];
                int[] triangles = new int[mesh.triangles.Length + 6];

                   strokePixelsX.Add(Mathf.Abs(Mathf.Floor(getPoistionOnDrawArea().x * 55.5f)));
                   
                   strokePixelsY.Add(Mathf.Abs(Mathf.Floor(getPoistionOnDrawArea().y * 55.5f)));
                   
                    //forImagePoint
                    Vector2 point = new Vector2();
                    point = ObjectManager.instance.realDrawingArea.transform.InverseTransformPoint(getMouseWorlPosition());
                    indexOfPoint++;
                   
                  

                    if (point.x < minx)
                    {
                        minx = point.x;
                       
                    }
                    if (point.x >= max)
                    {
                        max = point.x;
                        DrawingToJson.instance.indexOfMaxX = indexOfPoint;
                    }

                  
                    if (point.y < miny)
                        miny = point.y;
                    if (point.y >= maxy)
                        maxy = point.y;
                    DrawingToJson.instance.imagePoints.Add(point);
                   
                    // Debug.Log(getPoistionOnDrawArea());

                    mesh.vertices.CopyTo(vertices, 0);
                mesh.uv.CopyTo(uv, 0);
                mesh.triangles.CopyTo(triangles, 0);

                int vIndex = vertices.Length - 4;
                int vIndex0 = vIndex + 0;
                int vIndex1 = vIndex + 1;
                int vIndex2 = vIndex + 2;
                int vIndex3 = vIndex + 3;

                Vector3 mouseForwardVector = (getMouseWorlPosition() - lastMousePosition).normalized;
                Vector3 normal2D = new Vector3(0, 0, -1f);
               
                Vector3 newVertexUp = getMouseWorlPosition() + Vector3.Cross(mouseForwardVector, normal2D) * lineThickness;
                Vector3 newVertexDown = getMouseWorlPosition() + Vector3.Cross(mouseForwardVector, normal2D * -1f) * lineThickness;

                vertices[vIndex2] = newVertexUp;
                vertices[vIndex3] = newVertexDown;

                uv[vIndex2] = Vector2.zero;
                uv[vIndex3] = Vector2.zero;

                int tIndex = triangles.Length - 6;

                triangles[tIndex + 0] = vIndex0;
                triangles[tIndex + 1] = vIndex2;
                triangles[tIndex + 2] = vIndex1;

                triangles[tIndex + 3] = vIndex1;
                triangles[tIndex + 4] = vIndex2;
                triangles[tIndex + 5] = vIndex3;

                mesh.vertices = vertices;
                mesh.uv = uv;
                mesh.triangles = triangles;

                lastMousePosition = getMouseWorlPosition();
            }
        }
        
        else if(canvasFull)
        {
            canDraw = false;
                strokePixelsXY.Add(strokePixelsX);
                strokePixelsXY.Add(strokePixelsY);
                DrawingToJson.instance.imageStrokes.Add(strokePixelsXY);
                DrawingToJson.instance.max = max;
                DrawingToJson.instance.minx = minx;
                DrawingToJson.instance.maxy = maxy;
                DrawingToJson.instance.miny = miny;
                strokeCreated = true;
            }
    }

    public Vector3 getMouseWorlPosition()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return new Vector3(pos.x,pos.y,0);
    }


     bool checkIfInDrawArea()
    {
        // Bit shift the index of the layer (8) to get a bit mask

        Vector3 firePoint = new Vector3(getMouseWorlPosition().x, getMouseWorlPosition().y, -10);
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(firePoint, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(firePoint, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
         
            return true;
        }
        else
        {
            Debug.DrawRay(firePoint, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
           
            
        }
        return false;
    }


    Vector2 getPoistionOnDrawArea()
    {
        Vector3 firePoint = new Vector3(getMouseWorlPosition().x, getMouseWorlPosition().y, -10);
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(firePoint, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(firePoint, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            
            return new Vector2(hit.point.x-Camera.main.transform.position.x-ObjectManager.instance.realDrawingArea.transform.position.x- ObjectManager.instance.realDrawingArea.transform.localScale.x-1.2f, -hit.point.y+ ObjectManager.instance.realDrawingArea.transform.localScale.y/2);
        }
        else
        {
            Debug.DrawRay(firePoint, transform.TransformDirection(Vector3.forward) * 1000, Color.white);


        }
        return Vector2.zero;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;
public class AlgoGameManager : MonoBehaviour
{
    public List<GameObject> listOfCubes = new List<GameObject>();
    public List<Vector2> listOfVectors = new List<Vector2>();
    public Material mat1,mat2,mat3;
    public TextMeshProUGUI directionText;
    public List<string> directionList;
    List<Vector2> listOfDirections = new List<Vector2>();
    List<string> fontion1 = new List<string>();
    List<string> fonction2 = new List<string>();
    public GameObject player;
   public RectTransform Case;
    void Start()
    {
        instantianteListOfVectors();
         Spots = selecteTwoSpots();





        StartCoroutine(move(Spots[0])) ;

    }
    Vector2[] Spots;
    Vector2 finishPosition;
    bool finishedMovingToSpots = false,finishedMovingAllOver=false;
    int x = 0, y = 0;
    string direction = "";
    bool fisrtStep = true;
    Vector2 lastDirection,interPos;
    int XOrY=0;

    string actualXorY = "",lastXorY="";
    IEnumerator move(Vector2 pos)
   {
        int lastX = x, lastY = y;





        selectCube(interPos);
        int xMovesCount = (int)pos.x - x;
        int signMoveX = xMovesCount < 0 ? -1 : 1;
        xMovesCount = Mathf.Abs(xMovesCount);
        int yMovesCount = (int)pos.y - y;
        int signMovey = yMovesCount < 0 ? -1 : 1;
        yMovesCount = Mathf.Abs(yMovesCount);
        while(xMovesCount>0 || yMovesCount>0)
        {


          
            lastXorY = actualXorY;

             XOrY = Random.Range(0, 100) % 2 == 0 ? 1 : 0;

            if (XOrY == 1 && xMovesCount > 0)
            {
                //x
                if (check(new Vector2(x + 1 * signMoveX, y)))
                    continue;
                actualXorY = "x";
                lastX = x;
                x = x + 1 * signMoveX;
               
                xMovesCount--;
            }
            else if (XOrY == 0 && yMovesCount > 0)
            {
                if (check(new Vector2(x, y + 1 * signMovey)))
                    continue;
                actualXorY = "y";
                lastY = y;
                y = y + 1 * signMovey;
               
                yMovesCount--;              
            }
            /////////////////////////////////////////
            lastDirection = interPos;
            interPos = new Vector2(x, y);
               if (!lastXorY.Equals(actualXorY))
               {
                   var dot = 0f ;
              
                dot = perp_dot(lastDirection, interPos);
                 
                  if (dot > 0)
                       direction = "left";
                   else if (dot < 0)
                       direction = "right";
                   else
                       direction = "forward";

               }
               else
               {
                   direction = "forward";
               }

            directionList.Add(direction);
          

            ///////////////////////////////////






            

            selectCube(interPos);
           

        }
        
        nbrOfRoute++;

        if (!secondRoute)
        {
            secondRoute = true;
                 yield return StartCoroutine(move(Spots[1]));
        }
        if(!ThirdRoute)
        {
            ThirdRoute = true;
            yield return StartCoroutine(move(finishPosition));
        }
      
        if(!finishedMovingAllOver)
        {
            finishedMovingAllOver = true;
            yield return StartCoroutine(spawnCases());
        }
      
    }
    float perp_dot(Vector2 a, Vector2 b) { return -a.y * b.x + a.x * b.y; }
   
   // int relative_direction(vector2 a, vector2 b, vector2 c) { vector2 v1 = b - a; vector2 v2 = c - b; return sign(perp_dot(v1, v2)); }

    bool secondRoute = false;
    bool ThirdRoute = false;
    int nbrOfRoute = 1;
    bool showingDirectionsOneTime = true;
    IEnumerator spawnCases()
    {
        hideInvisitedCubes();
        float offsetX=0, OffsetY=0;
        int index = 1;
        foreach(string direction in directionList)
        {
            offsetX += 1.5f;
            if (index%5==0)
            {
                OffsetY += -1.2f;
                offsetX = 0;
            }
          
            directionText.text += " " + direction;
            RectTransform c = Instantiate(Case);
            c.parent = Case.parent;
            c.position = new Vector3(Case.position.x + offsetX, Case.position.y + OffsetY, Case.position.z);
            c.localScale = new Vector3(0.18709f, 0.18709f, 0.18709f);
            index++;
        }
        searchOfOcc();   
        yield return null;
    }
    void showdirections()
    {
      
        
    }
    bool check(Vector2 pos)
    {
        GameObject f = listOfCubes[listOfVectors.FindIndex(x => x == pos)];
       return f.GetComponent<Cube>().visited;
    }
    void selectCube(Vector2 pos)
    {
      
        Material m;
        if (nbrOfRoute == 1)
            m = mat1;
        else if (nbrOfRoute == 2)
            m = mat2;
        else
            m = mat3;
        GameObject f = listOfCubes[listOfVectors.FindIndex(x => x == pos)];
        if(f.GetComponent<MeshRenderer>().sharedMaterial!= m)
        {
            f.GetComponent<MeshRenderer>().sharedMaterial = m;
            f.GetComponent<Cube>().visited = true;
            
        }
            
    }

   Vector2[] selecteTwoSpots()
    {
        int randPosX,randPosY;
        randPosX = Random.Range(7, 9);
        randPosY = Random.Range(0, 2);
        
        Vector2 randomSpot1 = new Vector2(randPosX, randPosY);

        randPosX = Random.Range(2, 5);
        randPosY = Random.Range(6, 8);

        Vector2 randomSpot2 = new Vector2(randPosX, randPosY);
        int order = Random.Range(1, 100) % 2 == 0 ? 1:0;
        if(order==1)
        {
            finishPosition = new Vector2(10, 0);
            
        }else
            finishPosition = new Vector2(10, 10);

       
        return new Vector2[] { randomSpot2, randomSpot1};
    }
   
    void instantianteListOfVectors()
    {
        int x = 0;
        int y = -1;
        for (int i = 0; i <= 120; i++)
        {
            if (i % 11 == 0)
            {
                y++;
                x = 0;
            }

            Vector2 c = new Vector2(x, y);
            listOfVectors.Add(c);
            x++;
        }
    }
    void hideInvisitedCubes()
    {
        foreach(GameObject c in listOfCubes)
        {
            if (!c.GetComponent<Cube>().visited)
                c.GetComponent<MeshRenderer>().enabled=false;
        }
    }
    void searchOfOcc()
    {
        /*for(int i=0;i< directionList.Count;i++)
        {
            if(i+3<directionList.Count)
            {
                string first = directionList[i];
                string second = directionList[i + 1];
                string third = directionList[i + 2];
                string fourth = directionList[i + 3];
                int string1index;
                int string2index,string3index,string4index;
                for(int j=i;j<directionList.Count;j+=4)
                {
                    string1index = directionList.FindIndex(j,s => s.Equals(first));


                    if (string1index >= 0)
                    {
                        string2index = directionList.FindIndex(j,s => s.Equals(second));
                        if (string2index >= 0 && string2index == string1index + 1)
                        {
                            string3index = directionList.FindIndex(j,s => s.Equals(third));
                            if (string3index >= 0 && string3index == string2index + 1)
                            {
                                string4index = directionList.FindIndex(j,s => s.Equals(fourth));
                                if (string4index >= 0 && string4index == string3index + 1)
                                {
                                    fontion1.Add(first);
                                    fontion1.Add(second); fontion1.Add(third); fontion1.Add(fourth);
                                    break;
                                }
                            }
                        }
                    }
                }

                if (fontion1.Count > 0)
                    break;
            }
        }
        if (fontion1.Count > 0)
            Debug.Log(fontion1[0] + "" + fontion1[1]+""+fontion1[2]+" "+fontion1[3]);*/



        string[] pattern_data = new string[] { directionList[0],directionList[1],directionList[2]};
        string[] data = directionList.ToArray();
        int count = 0;
        for (int i = 0; i < data.Length; i++)
        {
            string[] sub = data.Skip(i).Take(pattern_data.Length).ToArray();
            if (sub.SequenceEqual(pattern_data))
            {
               Debug.Log(i+" "+sub);
                count++;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System;
using UnityEngine.UI;


public class RenderCamera : MonoBehaviour
{
    // Start is called before the first frame update
    public int FileCounter = 0;
    public GameObject errorMessage;
    public Canvas canvas;
    private void Start()
    {
        ObjectManager.instance.okButton.SetActive(false);
        ObjectManager.instance.clearButton.SetActive(false);
        ObjectManager.instance.drawingArea.SetActive(false);
    }

    public  void CamCapture()
    {
        if (DrawingToJson.instance.imageStrokes.Count == 0)
        {

            StartCoroutine(showError());
            return;
        }
        

       
        Camera Cam = GetComponent<Camera>();

        RenderTexture currentRT = RenderTexture.active;
       
        RenderTexture.active = Cam.targetTexture;
        
        Cam.Render();

        Texture2D Image = new Texture2D(Cam.targetTexture.width, Cam.targetTexture.height);
        Image.ReadPixels(new Rect(0, 0, Cam.targetTexture.width, Cam.targetTexture.height), 0, 0);
        Image.Apply();
        RenderTexture.active = currentRT;

        var Bytes = Image.EncodeToPNG();
        Destroy(Image);

        File.WriteAllBytes(Application.dataPath + "/Backgrounds/" + FileCounter + ".png", Bytes);
       
        
        //

        //call server to predict
        //

        

        MyClass myObject = new MyClass();
      
        myObject.strokes = loopOnList();
        myObject.box = box;
        string json = JsonUtility.ToJson(myObject);
        string[] r= json.Split('?');
      
        string final = r[0].Remove(r[0].Length - 1) + r[1] + r[2].Remove(0,1);
        File.WriteAllText(".\\Assets\\MyFile.json", final);
        if(!ObjectManager.instance.drawLine)
        FecthServer.instance.call(final);
        else
        {
            ObjectManager.instance.instantiateObject("DrawnLine",500);
        }
       
       
    }
    
    int[] box=new int[4];
    string loopOnList()
    {
       
        float xmin, xmax, ymin, ymax;
        List<float> listOmins = new List<float>();
        List<float> listOfMax = new List<float>();
        List<float> listOminsY = new List<float>();
        List<float> listOfMaxY = new List<float>();
        //public List<List<List<float>>> imageStrokes = new List<List<List<float>>>();
        List<List<List<float>>> imageStrokes = DrawingToJson.instance.imageStrokes;
       
        string r = "";
        r += "?[";
        int index = 0;
      
        foreach (List<List<float>> stroke in imageStrokes)
        {
            r += "[";
            r = r.Trim();
            r += "[";
            r = r.Trim();

            //float i = stroke[index][0].AsQueryable().Min();
            
            listOmins.Add(stroke[0].Min());
            //float i2 = stroke[index].AsQueryable().Max();
            listOfMax.Add(stroke[0].Max());
            foreach (float x in stroke[0])
            {
             
                    r += x + ",";
                r = r.Trim();
            }
            r = r.Remove(r.Length - 1);
            r += "],";
            r = r.Trim();
            r += "[";
            r = r.Trim();
            //float i = listy.AsQueryable().Min();
            listOminsY.Add(stroke[1].Min());
            //float i2 = listy.AsQueryable().Max();
            listOfMaxY.Add(stroke[1].Max());
            foreach (float y in stroke[1])
            {
               
                    r += y + ",";
                r = r.Trim();

            }
            r = r.Remove(r.Length - 1);
            r += "],";
            r = r.Trim();
            r = r.Remove(r.Length - 1);
            r += "],";
            r = r.Trim();
            index++;
        }
        r = r.Remove(r.Length - 1);
        r += "]?";
        r = r.Trim();
        if(listOmins.Count<=0 )
        {
            xmin = 0;
            xmax = 0;
            ymin = 0;
            ymax = 0;
        }else
        {
             xmin = listOmins.AsQueryable().Min();
        xmax = listOfMax.AsQueryable().Max();
        ymin = listOminsY.AsQueryable().Min();
        ymax = listOfMaxY.AsQueryable().Max();
        }
       
        box[0] = 0;
        box[1] = 0;
        box[2] = (int)xmax;
        box[3] = (int)ymax;
        DrawingToJson.instance.imageStrokes.Clear();
       
        return r.Trim();

    }

    void getBoundingBox()
    {

    }

    [Serializable]
    public class MyClass
    {
        public string strokes;
        public int[] box;
      
    }

    IEnumerator showError()
    {
        var message = Instantiate(errorMessage);

        var rectTransform = message.GetComponent<RectTransform>();
        
        rectTransform.SetParent(canvas.transform);
        rectTransform.sizeDelta = new Vector2(100, 100);
        rectTransform.localScale = new Vector3(4.1747f, 0.73752f, 1);
        
        float positionY = 50;
        rectTransform.localPosition = new Vector2(0, positionY);

        rectTransform.anchorMin = new Vector2(0.5f, 1);
        rectTransform.anchorMax = new Vector2(0.5f, 1);
        yield return new WaitForSeconds(1f);
        Destroy(message);

        //rectTransform.localPosition = new Vector2(0, -50);


    }

}

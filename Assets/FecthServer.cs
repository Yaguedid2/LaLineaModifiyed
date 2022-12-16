using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Http;
using System;
using Newtonsoft.Json;
using UnityEngine.UI;
using TMPro;

public class FecthServer : MonoBehaviour
{
    public Canvas canvas;
    public GameObject choiceButton;
    public static FecthServer instance;
  
    private void Awake()
    {
        instance = this;
    }
   
    private void Start()
    {


        modelLoaded = false;
        
    
        
    }
    private void Update()
    {
        GameManager.instance.GameLoaded = modelLoaded;
        if (!modelLoaded)
        {
            isModelLoaded();
            Debug.Log("model isn t loaded yet");

        }
        if(readyToGetResponse)
        {
            Debug.Log("ready");
            getResult();
        }
       
       
    }


    public void call(string json)
    {
        
       Debug.Log(json);
        getResponse(json);
        readyToGetResponse=true;
        
    }


    public static async void getResponse(string json)
    {
        //Define your baseUrl
        string baseUrl = "http://127.0.0.1:9090/predict/" + json;
        //Have your using statements within a try/catch block
        try
        {
            //We will now define your HttpClient with your first using statement which will use a IDisposable.
            using (HttpClient client = new HttpClient())
            {
                //In the next using statement you will initiate the Get Request, use the await keyword so it will execute the using statement in order.
                //The HttpResponseMessage which contains status code, and data from response.
                using (HttpResponseMessage res = await client.GetAsync(baseUrl))
                {
                    //Then get the data or content from the response in the next using statement, then within it you will get the data, and convert it to a c# object.
                    using (HttpContent content = res.Content)
                    {
                        var data = await content.ReadAsStringAsync();
                        //If the data isn't null return log convert the data using newtonsoft JObject Parse class method on the data.
                        
                    }
                }
            }
        }
        catch (Exception e)
        {

        }
       
    }
    



    bool modelLoaded = false;
    bool readyToGetResponse = false;

    public  async void LoadModel()
    {
        //Define your baseUrl
        string baseUrl = "http://127.0.0.1:9090/loadModel";
        //Have your using statements within a try/catch block
        try
        {
            //We will now define your HttpClient with your first using statement which will use a IDisposable.
            using (HttpClient client = new HttpClient())
            {
                //In the next using statement you will initiate the Get Request, use the await keyword so it will execute the using statement in order.
                //The HttpResponseMessage which contains status code, and data from response.
                using (HttpResponseMessage res = await client.GetAsync(baseUrl))
                {
                    //Then get the data or content from the response in the next using statement, then within it you will get the data, and convert it to a c# object.
                    using (HttpContent content = res.Content)
                    {
                        var data = await content.ReadAsStringAsync();
                        //If the data isn't null return log convert the data using newtonsoft JObject Parse class method on the data.
                        
                    }
                }
            }
        }
        catch (Exception e)
        {

        }
    }
   
    public static async void isModelLoaded()
    {
        //Define your baseUrl
        string baseUrl = "http://127.0.0.1:9090/isModelLoaded";
        //Have your using statements within a try/catch block
        try
        {
            //We will now define your HttpClient with your first using statement which will use a IDisposable.
            using (HttpClient client = new HttpClient())
            {
                //In the next using statement you will initiate the Get Request, use the await keyword so it will execute the using statement in order.
                //The HttpResponseMessage which contains status code, and data from response.
                using (HttpResponseMessage res = await client.GetAsync(baseUrl))
                {
                    //Then get the data or content from the response in the next using statement, then within it you will get the data, and convert it to a c# object.
                    using (HttpContent content = res.Content)
                    {
                        var data = await content.ReadAsStringAsync();
                        //If the data isn't null return log convert the data using newtonsoft JObject Parse class method on the data.
                        if (data != null)
                        {
                            if (data.Equals("true"))
                                instance.modelLoaded = true;
                            else
                            {
                                instance.modelLoaded = false;
                                if(!instance.alreadyLoading)
                                instance.StartCoroutine(instance.invokeLoadModel());
                                 
                            }

                        }
                        else
                        {
                            instance.modelLoaded = false;
                           
                              
                            
                        }
                    }
                }
            }
        }
        catch (Exception e)
        {

        }
    }

    public static async void getResult()
    {
        //Define your baseUrl
        string baseUrl = "http://127.0.0.1:9090/result";
        //Have your using statements within a try/catch block
        try
        {
            //We will now define your HttpClient with your first using statement which will use a IDisposable.
            using (HttpClient client = new HttpClient())
            {
                //In the next using statement you will initiate the Get Request, use the await keyword so it will execute the using statement in order.
                //The HttpResponseMessage which contains status code, and data from response.
                using (HttpResponseMessage res = await client.GetAsync(baseUrl))
                {
                    //Then get the data or content from the response in the next using statement, then within it you will get the data, and convert it to a c# object.
                    using (HttpContent content = res.Content)
                    {
                        var data = await content.ReadAsStringAsync();
                        //If the data isn't null return log convert the data using newtonsoft JObject Parse class method on the data.
                        if (data != null)
                        {
                           
                           
                            Debug.Log(data);
                            
                           instance.StartCoroutine(instance.dataToJson(data));
                            instance.readyToGetResponse = false;
                        }
                        else
                        {
                            
                        }
                    }
                }
            }
        }
        catch (Exception e)
        {

        }
    }
    bool alreadyLoading = false;
    IEnumerator invokeLoadModel()
    {
        alreadyLoading = true;
        LoadModel();
        yield return new WaitForSeconds(40f);
        
        alreadyLoading = false;
    }
    IEnumerator  dataToJson(string data)
    {

        List<Root> myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(data);
        ObjectManager.instance.clearButton.SetActive(false);
        ObjectManager.instance.okButton.SetActive(false);
        ObjectManager.instance.drawLineButton.SetActive(false);
        Hand.instance.showHand(false);
        SpeakManager.instance.Speak("Hmmm! ", false,2);
        yield return new WaitForSeconds(2f);
        SpeakManager.instance.Speak("Here is what i think ",false,5);
        yield return new WaitForSeconds(5f);
        float posOfButton = 400;
        float anchorButton = 0;
        foreach (Root possibility in myDeserializedClass)
        {
           
            var button = Instantiate(choiceButton, Vector3.zero, Quaternion.identity);
            listOfChoiceButtons.Add(button);
            var rectTransform = button.GetComponent<RectTransform>();
            rectTransform.SetParent(canvas.transform);
            rectTransform.sizeDelta = new Vector2(160, 30);
            rectTransform.localScale = new Vector3(1, 1, 1);
            rectTransform.localPosition = new Vector2(557, posOfButton);
            button.GetComponentInChildren<TextMeshProUGUI>().text = possibility.className;
            rectTransform.anchorMin = new Vector2(0, 0);
            rectTransform.anchorMax = new Vector2(0, 0);
            button.GetComponent<Button>().onClick.AddListener(()=> SpawnObject(possibility.className,possibility.index));
            posOfButton -= 50;
            anchorButton += 0.5f;
            yield return new WaitForSeconds(0.3f);
        }
           
        
      

       
    }
    List<GameObject> listOfChoiceButtons = new List<GameObject>();
    void SpawnObject(string className,int index)
    {
        foreach (GameObject button in listOfChoiceButtons)
            Destroy(button);
        listOfChoiceButtons.Clear();
        ObjectManager.instance.instantiateObject(className,index);
    }
    public class Root
    {
        public double probability { get; set; }
        public string className { get; set; }
        public int index { get; set; }
    }
}
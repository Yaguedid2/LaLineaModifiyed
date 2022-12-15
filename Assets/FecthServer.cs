using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Http;
using System;
using Newtonsoft.Json;


public class FecthServer : MonoBehaviour
{
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
        List<string> possibilities = new List<string>();
        ObjectManager.instance.instantiateObject(possibilities);
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
                            
                            instance.dataToJson(data);
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
    void dataToJson(string data)
    {

        List<Root> myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(data);
        foreach (Root possibility in myDeserializedClass)
            SpeakManager.instance.Speak(possibility.className);

       
    }

    public class Root
    {
        public double probability { get; set; }
        public string className { get; set; }
        public int index { get; set; }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Http;
using System;
public class FecthServer : MonoBehaviour
{
    public static FecthServer instance;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        
    }


    
    public void call()
    {
        List<string> possibilities = new List<string>();
        ObjectManager.instance.instantiateObject(possibilities);
    }


    public static async void GetPokemon()
    {
        //Define your baseUrl
        string baseUrl = "http://127.0.0.1:8000/zb/6";
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
                            //Now log your data in the console
                            Debug.Log(data);
                        }
                        else
                        {
                            Debug.Log("noData");
                        }
                    }
                }
            }
        }
        catch (Exception e)
        {

        }
    }
}
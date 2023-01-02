using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WheatherManager : MonoBehaviour
{
    float temperator=23,momentumTemperator;
    public TextMeshProUGUI tempText;
    public Image headTempor, bottomTempor;
    public GameObject sun, wind, cloud, rain, snow;
    List<GameObject> listOfAllSpawnedWheathers = new List<GameObject>();
    public float timeForWheather = 10f;
    public float timeBetweenChanges = 10f;

    void Start()
    {
        StartCoroutine(root());
    }

    // Update is called once per frame
    void Update()
    {
        tempoCounter();
    }
    string wheather="",previousWheather;
    string compangnienSun, previousCompagnienSun, compangnienRain, previousCompagnienRain, 
        compangnienCloud, previousCompagnienCloud, compangnienSnow, previousCompagnienSnow, compangnienWind, previousCompagnienWind;
    public IEnumerator root()
    {
        PlayerController.instance.GetComponentInChildren<Cloth>().worldAccelerationScale = -0.5f;
        //StopAllCoroutines();
        destroyAllWeathers();
        yield return new WaitForSeconds(timeBetweenChanges);
        previousWheather =wheather;
        previousCompagnienWind = compangnienWind;
        previousCompagnienSun = compangnienSun;
        previousCompagnienRain = compangnienRain;
        previousCompagnienSnow = compangnienSnow;
        previousCompagnienCloud = compangnienCloud;
        wheather = selectWeather();
       
        switch (wheather)
        {
            case "wind":
                StartCoroutine(generateWind(true,true));
               
                int windAlone = Random.Range(1, 5);
                    if (windAlone % 2 == 0)
                    {

                        int cloudOrSunOrSnoworRain = Random.Range(1, 100);
                        if (cloudOrSunOrSnoworRain < 25)
                        {
                            compangnienWind = "sun";
                            
                           
                        }

                        else if (cloudOrSunOrSnoworRain < 50)
                        {
                            compangnienWind = "rain";
                          
                        }
                        else if (cloudOrSunOrSnoworRain < 75)
                        {
                            compangnienWind = "cloud";
                           
                        }

                        else
                        {
                            compangnienWind = "snow";
                           
                        }
                    switch (compangnienWind)
                    {
                        case "sun": StartCoroutine(generateSun(false,true)); break;
                        case "rain": StartCoroutine(generateRain(false,true)); break;
                        case "cloud": StartCoroutine(generateCloud(false,true)); break;
                        case "snow": StartCoroutine(generateSnow(false,true)); break;

                    }
                }
                   
               
               



                break;
            case "sun":
                StartCoroutine(generateSun(true,true));
              
                int sunAlone = Random.Range(1, 5);
               
                    if (sunAlone % 2 == 0)
                    {

                        int cloudOrWindOrSnoworRain = Random.Range(1, 100);
                        if (cloudOrWindOrSnoworRain < 25)
                        {
                            compangnienSun = "wind";
                            
                        }
                        else if (cloudOrWindOrSnoworRain < 50)
                        {
                            compangnienSun = "cloud";
                           
                        }
                        else if (cloudOrWindOrSnoworRain < 75)
                        {
                            compangnienSun = "snow";
                           
                        }
                        else
                        {
                            compangnienSun = "rain";
                            
                        }

                    switch (compangnienRain)
                    {
                        case "wind": StartCoroutine(generateWind(false,true)); break;
                        case "cloud": StartCoroutine(generateCloud(false,true)); break;
                        case "snow": StartCoroutine(generateSnow(false,true)); break;
                        case "rain": StartCoroutine(generateRain(false,true)); break;

                    }

                }
               
               

                break;
            case "rain":
               
                StartCoroutine(generateCloud(false,false));
                StartCoroutine(generateRain(true,true));
                
                int rainAlone = Random.Range(1, 5);
                    if (rainAlone % 2 == 0)
                    {

                        int windOrSun = Random.Range(1, 100);
                        if (windOrSun % 2 == 0)
                        {
                            compangnienRain = "sun";
                            
                        }
                        else
                        {
                            compangnienRain = "wind";
                            
                        }
                    switch (compangnienRain)
                    {
                        case "sun": StartCoroutine(generateSun(false,true)); break;
                        case "wind": StartCoroutine(generateWind(false,true)); break;
                    }
                }
               

              
              

                break;
            case "snow":
                StartCoroutine(generateCloud(false,false));
                StartCoroutine(generateSnow(true,true));
               
                int snowAlone = Random.Range(1, 5);
                    if (snowAlone % 2 == 0)
                    {

                        int WindOrSunorRain = Random.Range(1, 100);
                        if (WindOrSunorRain < 35)
                        {
                            compangnienSnow = "sun";
                           
                        }

                        else if (WindOrSunorRain < 70)
                        {
                            compangnienSnow = "rain";
                            
                        }
                        else
                        {
                            compangnienSnow = "wind";
                            
                        }



                    switch (compangnienSnow)
                    {
                        case "sun": StartCoroutine(generateSun(false,false)) ; break;
                        case "rain": StartCoroutine(generateRain(false,false)); break;
                        case "wind": StartCoroutine(generateWind(false,false)); break;
                    }
                }
               

               

                break;
            case "cloudy":
                StartCoroutine(generateCloud(true,true));
               
                int cloudAlone = Random.Range(1, 5);
                    if (cloudAlone % 2 == 0)
                    {

                        int sunOrWindOrSnoworRain = Random.Range(1, 100);
                        if (sunOrWindOrSnoworRain < 25)
                        {
                            compangnienCloud = "wind";
                           
                        }
                        else if (sunOrWindOrSnoworRain < 50)
                        {
                            compangnienCloud = "sun";
                            
                        }
                        else if (sunOrWindOrSnoworRain < 75)
                        {
                            compangnienCloud = "snow";
                            
                        }
                        else
                        {
                            compangnienCloud = "rain";
                           
                        }


                    switch (compangnienCloud)
                    {
                        case "wind": StartCoroutine(generateWind(false,true)); break;
                        case "sun": StartCoroutine(generateSun(false,true)); break;
                        case "snow": StartCoroutine(generateSnow(false,true)); break;
                        case "rain": StartCoroutine(generateRain(false,true)); break;

                    }

                }

                
                

                break;
        }
     

    }
    string selectWeather()
    {
        int rand = Random.Range(0, 100);
        if (rand<=20)
        {
            return "wind";
            //windd
        }
        else if (rand > 20 && rand <40)
        {
            return "sun";
            //sun
        }else if(rand > 40 && rand < 60)
        {
            return "rain";
            //rain
        }else if (rand > 60 && rand < 80)
        {
            return "snow";
            //snow
        }else
        {
            return "cloudy";
            //cloudy
        }

    }
    IEnumerator generateWind(bool callRoot,bool allowChangeTemp)
    {
        // GameObject windClone = Instantiate(wind);
        //windClone.transform.parent = Camera.main.transform;
        if (allowChangeTemp)
        {
            momentumTemperator = Random.Range(10, 20);
            stepTemp = 0.00001f;
        }
        wind.transform.localPosition = new Vector3(-9.1f, -0.63f, 5.2f);
        bool oneTime = true; 
        
        listOfAllSpawnedWheathers.Add(wind);
       
        int maxParticles = 1000;
        sun.GetComponentInChildren<Light>().color = new Color(15f/255f, 147f/255f, 149f/255f);
        for (int nbrOfParticles = 0; nbrOfParticles <= maxParticles; nbrOfParticles++)
        {
            foreach (ParticleSystem p in wind.GetComponentsInChildren<ParticleSystem>())
            {
                p.maxParticles = nbrOfParticles;
            }
            yield return new WaitForSeconds(0.001f);
            if(oneTime)
            {
                oneTime = false;
                wind.GetComponent<ParticleSystem>().Play();
            }
           
        }
       // PlayerController.instance.GetComponentInChildren<Cloth>().worldAccelerationScale = -5;
        yield return new WaitForSeconds(timeForWheather);
        if (callRoot)
        {
            int probForClouds = Random.Range(0, 10);
            if (probForClouds <= 7)
                StartCoroutine(generateCloud(true,true));
            else 
                StartCoroutine(root());
        }
       

    }
    IEnumerator generateSun(bool callRoot, bool allowChangeTemp)
    {
        //GameObject sunClone = Instantiate(sun);
        //sunClone.transform.parent = Camera.main.transform;
        if(allowChangeTemp)
        {
            momentumTemperator = Random.Range(15, 36);
            stepTemp = 0.00001f;
        }
       
        sun.GetComponent<ParticleSystem>().Play();
        //-8f
        sun.transform.localPosition = new Vector3(-13f, 4.91f, 3.1f);
        sun.GetComponentInChildren<Light>().color = new Color(255f / 255f, 245f / 255f, 0f / 255f);
        listOfAllSpawnedWheathers.Add(sun);
       while(sun.transform.localPosition.x<-8)
        {
            sun.transform.localPosition = Vector3.MoveTowards(sun.transform.localPosition, new Vector3(-8f, 4.91f, 3.1f), 0.02f);
            yield return new WaitForEndOfFrame();
        }

       
            
        
        yield return new WaitForSeconds(timeForWheather);
        while (sun.transform.localPosition.x > -13)
        {
            sun.transform.localPosition = Vector3.MoveTowards(sun.transform.localPosition, new Vector3(-13f, 4.91f, 3.1f), 0.02f);
            yield return new WaitForEndOfFrame();
        }
        if (callRoot)
            StartCoroutine(root());
        

    }
    IEnumerator generateSnow(bool callRoot, bool allowChangeTemp)
    {
        //GameObject snowClone = Instantiate(snow);
        //snowClone.transform.parent = Camera.main.transform;
        bool oneTime = true;

        snow.transform.localPosition = new Vector3(0, 4.8f, 7.2f);
        if (allowChangeTemp)
        {
            momentumTemperator = Random.Range(-20, 2);
            stepTemp = 0.00001f;
        }
        sun.GetComponentInChildren<Light>().color = new Color(15f / 255f, 147f / 255f, 149f / 255f);
        listOfAllSpawnedWheathers.Add(snow);
        int maxParticles = 1000;

        for (int nbrOfParticles = 0; nbrOfParticles <= maxParticles; nbrOfParticles++)
        {
            foreach (ParticleSystem p in snow.GetComponentsInChildren<ParticleSystem>())
            {
                p.maxParticles = nbrOfParticles;
            }
            if (oneTime)
            {
                oneTime = false;
                snow.GetComponent<ParticleSystem>().Play();
            }
            yield return new WaitForSeconds(0.001f);
        }
        yield return new WaitForSeconds(timeForWheather);
        if (callRoot)
            StartCoroutine(root());
    }
    IEnumerator generateCloud(bool callRoot, bool allowChangeTemp)
    {
        //GameObject cloudClone = Instantiate(cloud);
        //cloudClone.transform.parent = Camera.main.transform;
        if (allowChangeTemp)
        {
            momentumTemperator = Random.Range(10, 27);
            stepTemp = 0.00001f;
        }
        cloud.transform.localPosition = new Vector3(-2.4f, 4.8f, 3.1f);
        bool oneTime = true;
        sun.GetComponentInChildren<Light>().color = new Color(15f / 255f, 147f / 255f, 149f / 255f);
        listOfAllSpawnedWheathers.Add(cloud);
        int maxParticles = 500;
       
        for(int nbrOfParticles=0;nbrOfParticles<=maxParticles;nbrOfParticles++)
        {
            foreach (ParticleSystem p in cloud.GetComponentsInChildren<ParticleSystem>())
            {
                p.maxParticles = nbrOfParticles;
            }
            if (oneTime)
            {
                cloud.GetComponent<ParticleSystem>().Play();
                oneTime = false;
            }
            yield return new WaitForSeconds(0.001f);
        }
        

        yield return new WaitForSeconds(timeForWheather);
        if(callRoot)
        {
            int probForRainOrSnow = Random.Range(0, 10);
            if (probForRainOrSnow <= 7 && previousWheather!= "snow" && previousWheather != "rain")
            {
                int rainOrSnow = Random.Range(0, 11);
                if (rainOrSnow % 2 == 0)
                    StartCoroutine(generateRain(true,true));
                else
                    StartCoroutine(generateSnow(true,true));
            }
            else 
                StartCoroutine(root());
        }
        
    }
    IEnumerator generateRain(bool callRoot, bool allowChangeTemp)
    {
        //GameObject rainClone = Instantiate(rain);
        //rainClone.transform.parent = Camera.main.transform;
        if (allowChangeTemp)
        {
            momentumTemperator = Random.Range(0, 27);
            stepTemp = 0.00001f;
        }
        rain.transform.localPosition = new Vector3(0, -5.11f, 3.1f);
        sun.GetComponentInChildren<Light>().color = new Color(15f / 255f, 147f / 255f, 149f / 255f);
        bool oneTime = true;
        listOfAllSpawnedWheathers.Add(rain);
        int maxParticles = 1000;

        for (int nbrOfParticles = 0; nbrOfParticles <= maxParticles; nbrOfParticles++)
        {
            foreach (ParticleSystem p in rain.GetComponentsInChildren<ParticleSystem>())
            {
                p.maxParticles = nbrOfParticles;
            }
            if (oneTime)
            {
                oneTime = false;
                rain.GetComponent<ParticleSystem>().Play();
            }
            yield return new WaitForSeconds(0.001f);
        }
        yield return new WaitForSeconds(timeForWheather);
        if (callRoot)
            StartCoroutine(root());
    }
    void destroyAllWeathers()
    {
        //animate
        //
        foreach (GameObject w in listOfAllSpawnedWheathers)
        {
            w.GetComponent<ParticleSystem>().Stop();
           
        }
         
        listOfAllSpawnedWheathers.Clear();
    }
    float stepTemp = 0.00001f;
    void tempoCounter()
    {
        if (stepTemp < 1 && wheather!="")
        {
            temperator = temperator * (1 - stepTemp) + momentumTemperator * stepTemp;
            stepTemp += 0.00001f;
            if (temperator >= 0)
            {
                bottomTempor.color = new Color(255 / 255f, 6 / 255f, 6 / 255f);
                headTempor.fillAmount = temperator / 40;
            }

            else
            {
                headTempor.fillAmount = 0;
                bottomTempor.color = new Color(94 / 255f, 67 / 255f, 176 / 255f);
            }
            tempText.text = Mathf.FloorToInt(temperator).ToString() + " C°";
        }
       
        
    }
}

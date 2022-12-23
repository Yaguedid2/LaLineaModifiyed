using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HeathManager : MonoBehaviour
{
    public int steps = 0;
    public TextMeshProUGUI stepsText,tiredText,hungryText,asleepText,happinessText;
    float happiness = 100;
    public float hungry = 0,tired=0,bored=0,coldHot=0,asleep=0,alone=0;
    public float hungryF = 0, tiredF = 0, boredF = 0, coldF = 0, hotF = 0, asleepF = 0, aloneF = 0;
    public float stepPerTired = 0.1f;
    float timeWithoutEating = 0,timeWithoutSleep=0;

    void Start()
    {

        stepsText.text = steps.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        timeWithoutEating += Time.deltaTime;
        timeWithoutSleep += Time.deltaTime;
        tiredCalc();
        hungryCalc();
        asleepCalc();
        happinessCalc();
        changeColorBackground();
    }
    public void addStep()
    {
        steps+=(int)Mathf.Floor(LineManager.instance.lineCurviness);
        stepsText.text = steps.ToString();
    }
    void tiredCalc()
    {
        tired = steps * stepPerTired  +(coldHot/100)*0.7f+(hungry)*0.5f+(alone/100)*0.1f+asleep+(bored/100)*0.1f;
        tiredText.text =Mathf.Floor(tired).ToString()+" %";
    }
    void hungryCalc()
    {
        hungry = timeWithoutEating * 0.1f + tired * 0.3f + (coldHot/100) * 0.1f;
        hungryText.text =Mathf.Floor(hungry).ToString()+" %";

    }
    void asleepCalc()
    {
        asleep = timeWithoutSleep * 0.1f + tired * 0.3f + (alone/100) * 0.1f+(bored/100)*0.1f;
        asleepText.text = Mathf.Floor(asleep).ToString() + " %";

    }
    void happinessCalc()
    {
        angry = (tired * 0.5f + asleep * 0.3f + hungry * 0.5f + (bored / 100) * 0.2f + (coldHot / 100) * 0.5f + (alone / 100) * 0.2f);
        if(happiness>0)
        happiness =100-angry;
        happinessText.text = Mathf.Floor(happiness).ToString() + " %";
    }
    float angry;
    void changeColorBackground()
    {
        float H = 124-(124/100)*angry;

        Camera.main.backgroundColor = Color.HSVToRGB(H / 360f, 54f / 100f, 56f / 100f);
    }
}

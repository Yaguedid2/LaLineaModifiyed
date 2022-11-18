using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HeathManager : MonoBehaviour
{
    public int steps = 0;
    public TextMeshProUGUI stepsText;
    void Start()
    {

        stepsText.text = steps.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void addStep()
    {
        steps++;
        stepsText.text = steps.ToString();
    }
}

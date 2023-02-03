using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Case : MonoBehaviour
{
    public Sprite down, up, left,right;
    Image display;
    void Start()
    {
        display=GetComponentsInChildren<Image>()[1];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void showArrow()
    {
        switch(ArrowControlManager.selected)
        {
          
            case "up": display.sprite = up; break;
            case "right": display.sprite = right; break;
            case "left": display.sprite = left; break;

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    // Start is called before the first frame update
    float offsetX, offsetY;
    public static Hand instance;
     bool show = false;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        showHand(false);
        offsetX = GetComponent<BoxCollider2D>().size.x*transform.localScale.x / 2;
        offsetY = GetComponent<BoxCollider2D>().size.y * transform.localScale.y / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if(show)
        transform.position = getMouseWorlPosition();
    }
    public Vector3 getMouseWorlPosition()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return new Vector3(pos.x+offsetX, pos.y-offsetY, -8.4f);
    }
    public void showHand(bool showOrNot)
    {
       
            gameObject.SetActive(showOrNot);
            show = showOrNot;
        
    }
}

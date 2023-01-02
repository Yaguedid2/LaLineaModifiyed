using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Card : MonoBehaviour
{
   public RawImage RawImage,question;
    public RenderTexture animationTexture;
    public TextMeshProUGUI textInside;
    void Start()
    {

       
    }

    // Update is called once per frame
    void Update()
    {

        GetComponent<Button>().interactable = !CardGameManager.instance.beginAnimationMatch;
    }
    public void flip()
    {
        question.gameObject.SetActive(false);
        RawImage.gameObject.SetActive(true);
        textInside.gameObject.SetActive(true);
       
    }
    public void unflip()
    {
        RawImage.gameObject.SetActive(false);
        question.gameObject.SetActive(true);
        textInside.gameObject.SetActive(false);
    }
}

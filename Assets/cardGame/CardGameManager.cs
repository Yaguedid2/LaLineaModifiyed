using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class CardGameManager : MonoBehaviour
{
    public List<RenderTexture> listOfAnimations = new List<RenderTexture>();
    public List<GameObject> listOfRecords = new List<GameObject>();
    public List<Image> listOfCards = new List<Image>();
    public ParticleSystem glowEffect,confettiStar,confettiFinal;
    public static CardGameManager instance;
    public TextMeshProUGUI timerText;
    public GameObject parachute;
    public GameObject scorePanel, star1, star2, star3,lines,records,quitImage,quitArrow,quitingPlayer;

    public TextMeshProUGUI noteScoreText, phraseText;


    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        spawnCards();
    }

    // Update is called once per frame
    void Update()
    {
        if(!beginAnimationMatch)
        timer();
    }
    float timeInMSec = 0,timeInGame;
    void timer()
    {
        if (scorePanelShown)
        {
            timeInMSec += Time.deltaTime;
            string t = String.Format("{0:0}", timeInMSec);
            timeInGame = Int32.Parse(t);
            timerText.text = t;
        }
        else
            timerText.gameObject.SetActive(false);
       
    }






    GameObject previousClick;
    int nbrOfMatches = -1;
  public void click(GameObject image)
    {
      

        if (previousClick!=null &&  previousClick.GetComponent<Card>() != null && image.GetComponent<Card>() != null && previousClick.GetComponent<Card>().RawImage.texture == image.GetComponent<Card>().RawImage.texture && previousClick.name!=image.name)
        {
            nbrOfMatches++;
           
            StartCoroutine(flyParachute(listOfRecords[nbrOfMatches].transform.position));
            StartCoroutine(matchAnimation(image,previousClick));
        }
        else if (previousClick != null && previousClick.GetComponent<Card>() != null)
        {
            previousClick.GetComponent<Animator>().Play("unflipCard");
        }
        previousClick = image;

    }
    List<float> listOftimeRecords = new List<float>();
    IEnumerator flyParachute(Vector3 targetPosition)
    {
        beginAnimationMatch = true;
        float timeToDoIt = 0.7f;
        float timeInSec = 0f;
        Vector3 origine = parachute.transform.position;

        Vector3 startposition = parachute.transform.position;
        Vector3 target = new Vector3(targetPosition.x, targetPosition.y, startposition.z);

        while (timeInSec < timeToDoIt)
        {
            parachute.transform.position = Vector3.Lerp(startposition, target, timeInSec / timeToDoIt);
          

            timeInSec += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        parachute.transform.position = target;
        listOfRecords[nbrOfMatches].SetActive(true);
        listOftimeRecords.Add(timeInGame);
        listOfRecords[nbrOfMatches].GetComponentInChildren<TextMeshProUGUI>().text = timeInGame.ToString();
        yield return new WaitForSeconds(1f);

        timeToDoIt = 1f;
        timeInSec = 0f;


        startposition = parachute.transform.localPosition;
        target = new Vector3(0f, 0f, startposition.z);

        while (timeInSec < timeToDoIt)
        {
            parachute.transform.localPosition = Vector3.Lerp(startposition, target, timeInSec / timeToDoIt);


            timeInSec += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(1f);



        timeToDoIt = 1f;
         timeInSec = 0f;


         startposition = parachute.transform.localPosition;
         target = new Vector3(-1.18f, 2.2f, startposition.z);

        while (timeInSec < timeToDoIt)
        {
            parachute.transform.localPosition = Vector3.Lerp(startposition, target, timeInSec / timeToDoIt);


            timeInSec += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        parachute.transform.position = origine;
        beginAnimationMatch = false;
        if (nbrOfMatches >= 7)
            StartCoroutine(showscorePanel());

    }
    bool scorePanelShown = true;
    IEnumerator showscorePanel()
    {
        scorePanelShown = false;
        int note=0;
        int notefinal;
        for(int i=1;i<listOftimeRecords.Count;i++)
        {
            float n = listOftimeRecords[i] - listOftimeRecords[i - 1];
           
                note += 10-notecalc(n);
           
        }
        notefinal = note / 8;
        Debug.Log(notefinal);
        lines.SetActive(false);
        records.SetActive(false);
        scorePanel.SetActive(true);
        for (int i = 0; i <= notefinal; i++)
        {
            noteScoreText.text = i.ToString();
            yield return new WaitForSeconds(0.2f);
         }
        if (note >= 7)
        {
            star1.SetActive(true);
            confettiStar.transform.position = star1.GetComponent<RectTransform>().position;
            confettiStar.Play();
            yield return new WaitForSeconds(0.5f);
            confettiStar.Stop();
            star2.SetActive(true);
        
            confettiStar.transform.position = star2.GetComponent<RectTransform>().position;
            confettiStar.Play();
            yield return new WaitForSeconds(0.5f);
            confettiStar.Stop();
            star3.SetActive(true);
            confettiStar.transform.position = star3.GetComponent<RectTransform>().position;
            confettiStar.Play();
        }
        else if (note >= 5)
        {
            star1.SetActive(true);
            confettiStar.transform.position = star1.GetComponent<RectTransform>().position;
            confettiStar.Play();
            yield return new WaitForSeconds(0.5f);
            confettiStar.Stop();
            star2.SetActive(true);
            confettiStar.transform.position = star2.GetComponent<RectTransform>().position;
            confettiStar.Play();
        }
        else
        {
            confettiStar.Stop();
            star1.SetActive(true);
            confettiStar.transform.position = star1.GetComponent<RectTransform>().position;
            confettiStar.Play();
        }


        phraseText.text = phraseGenerator(notefinal);
        confettiFinal.Play();
        yield return new WaitForSeconds(0.5f);
        quitArrow.SetActive(true);        
        quitImage.SetActive(true);
        quitingPlayer.GetComponent<Animator>().Play("walk");


        yield return new WaitForEndOfFrame();
    }
    int notecalc(float nbr)
    {
        int minus =(int) nbr / 10;
        minus = minus>=10?10:minus;
        return  2*minus;
    }
    string phraseGenerator(int note)
    {
        string[] good = { " You’ve got it made! ", 
            "Super! ", "Sensational!", "Wonderful! ", 
            "Superb! ", 
            "Perfect! ", 
            "Good remembering!",
            "Terrific! ",
            "Muy Bien! ",
            "You certainly did well today.","You’ve got your brain in gear today",
            };
        string[] medium = { "Good going", "That’s good!", "Now you have it. ", "That’s A work", "That’s coming along nicely" };
        string[] bad = { "Not bad. ", "Keep on trying! ", "Keep it up! ", "One more time and you’ll haveit." };
        if (note >= 7)
            return good[UnityEngine.Random.Range(0, good.Length)];
        else if(note>=5)
            return medium[UnityEngine.Random.Range(0, medium.Length)];
        else
            return bad[UnityEngine.Random.Range(0, bad.Length)];
    }







    public bool beginAnimationMatch;
    IEnumerator matchAnimation(GameObject a,GameObject b)
    {
       
        float timeToDoIt = 0.7f;
        float timeInSec = 0f;
      
       
        Vector3 startA = a.GetComponent<RectTransform>().position;
        Vector3 startB = b.GetComponent<RectTransform>().position;

        while (timeInSec<timeToDoIt)
        {
            a.GetComponent<RectTransform>().position = Vector3.Lerp(startA, (startA+ startB)/2, timeInSec / timeToDoIt);
            b.GetComponent<RectTransform>().position = Vector3.Lerp(startB, (startA + startB) / 2, timeInSec / timeToDoIt);
           
            timeInSec += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        glowEffect.transform.position = (startA + startB) / 2;
        glowEffect.Play();
        yield return new WaitForSeconds(0.3f);

     
        Destroy(a);
        Destroy(b);
    }




    List<int> listOfChoosenAnims = new List<int>();
    List<int> listOfChoosencards = new List<int>();
    void spawnCards()
    {
        for(int i=0;i<8;i++)
        {
            int animIndex;
            do
            {
                animIndex = UnityEngine.Random.Range(0, listOfAnimations.Count);
            } while (listOfChoosenAnims.Contains(animIndex));
            listOfChoosenAnims.Add(animIndex);
            RenderTexture animation = listOfAnimations[animIndex];
            int cardIndex;
            do
            {
                cardIndex = UnityEngine.Random.Range(0, listOfCards.Count);
            } while (listOfChoosencards.Contains(cardIndex));
            RawImage RawImage, question;
            TextMeshProUGUI textInside;
            textInside = listOfCards[cardIndex].GetComponentInChildren<TextMeshProUGUI>();
            RawImage = listOfCards[cardIndex].GetComponentsInChildren<RawImage>()[0];
            question = listOfCards[cardIndex].GetComponentsInChildren<RawImage>()[1];
            listOfCards[cardIndex].GetComponent<Card>().RawImage = RawImage;
            listOfCards[cardIndex].GetComponent<Card>().question = question;
            listOfCards[cardIndex].GetComponent<Card>().textInside = textInside;
            RawImage.texture=animation;
           textInside.text = setText(animation.name);
            RawImage.gameObject.SetActive(false);
            textInside.gameObject.SetActive(false);
            listOfChoosencards.Add(cardIndex);
            do
            {
                cardIndex = UnityEngine.Random.Range(0, listOfCards.Count);
            } while (listOfChoosencards.Contains(cardIndex));
           
            RawImage = listOfCards[cardIndex].GetComponentsInChildren<RawImage>()[0];
            question = listOfCards[cardIndex].GetComponentsInChildren<RawImage>()[1];
            textInside = listOfCards[cardIndex].GetComponentInChildren<TextMeshProUGUI>();
            listOfCards[cardIndex].GetComponent<Card>().RawImage = RawImage;
            listOfCards[cardIndex].GetComponent<Card>().textInside = textInside;
           textInside.text = setText(animation.name);
            listOfCards[cardIndex].GetComponent<Card>().question = question;
            RawImage.texture = animation;
            RawImage.gameObject.SetActive(false);
           textInside.gameObject.SetActive(false);
            listOfChoosencards.Add(cardIndex);
        }
       

    }
    string setText(string index)
    {
        switch(index)
        {
            case "1":return "Belly"; break;
            case "2": return "Chiken"; break;
            case "3": return "Oppa"; break;
            case "4": return "HipHop"; break;
            case "5": return "House"; break;
            case "6": return "Jazz"; break;
            case "7": return "Macarena"; break;
            case "8": return "Semba"; break;
            case "9": return "Snake"; break;
            case "10": return "Tut"; break;
            case "11": return "Wave"; break;

            case "12": return "Break1"; break;
            case "13": return "Flair"; break;
            case "14": return "Break3"; break;
            case "15": return "Break2"; break;
            case "16": return "Break1"; break;
            default:return "";
        }
    }
    public void switchBackToScene()
    {
        SceneManager.LoadScene("primal", LoadSceneMode.Single);
    }
}

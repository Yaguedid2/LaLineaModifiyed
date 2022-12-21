using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SpeakManager : MonoBehaviour
{
    public PlayerController player;
    public TextMeshPro text;
    List<string> whatToSay = new List<string>();
    public static SpeakManager instance;
    public float timeBetweenWords = 1f;
    public bool isPrinting = false;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (whatToSay.Count != 0 && !isPrinting)
            StartCoroutine(generateText());
    }
    bool splitPhrase = false;
    float persTimeBetweenWords;
    public void Speak(string phrase,bool split,float time)
    {
        persTimeBetweenWords = time;
        splitPhrase = split;
        whatToSay.Add(phrase);
       
    }
    IEnumerator generateText( )
    {
        isPrinting = true;
       
        float _timeBetweenWords;
        if (persTimeBetweenWords == 0)
            _timeBetweenWords = timeBetweenWords;
        else
            _timeBetweenWords = persTimeBetweenWords;
        if (splitPhrase)
        {
            string[] words = whatToSay[0].Split(' ');
            whatToSay.RemoveAt(0);
            for (int i = 0; i < words.Length; i++)
            {
                Vector2 headPosition = new Vector2(player.GetComponent<Collider>().bounds.size.x + player.transform.position.x, player.transform.position.y + player.GetComponent<Collider>().bounds.size.y);
                //Debug.Log(headPosition);
                float offsetX = Random.Range(3f, 4f);
                float offsetY = Random.Range(0f, 3f);

                TextMeshPro t = Instantiate(text);
           
                t.transform.position = new Vector3(headPosition.x - offsetX, headPosition.y - offsetY, -8.85f);
                t.text = words[i];
                yield return new WaitForSeconds(_timeBetweenWords);
                t.GetComponent<Rigidbody>().velocity = new Vector3(0, 1, 0);
               
                Destroy(t.gameObject, _timeBetweenWords );
               

            }
           
            isPrinting = false;
        }
        else
        {
            Vector2 headPosition = new Vector2(player.GetComponent<Collider>().bounds.size.x + player.transform.position.x, player.transform.position.y + player.GetComponent<Collider>().bounds.size.y);
            //Debug.Log(headPosition);
            float offsetX = Random.Range(3f, 4f);
            float offsetY = Random.Range(0f, 3f);

            TextMeshPro t = Instantiate(text);
            t.transform.position = new Vector3(headPosition.x - offsetX, headPosition.y - offsetY, -8.85f);
            t.text = whatToSay[0];
            whatToSay.RemoveAt(0);
            t.GetComponent<Rigidbody>().velocity = new Vector3(0, 1, 0);
            Destroy(t.gameObject, _timeBetweenWords / 2);
            yield return new WaitForSeconds(_timeBetweenWords);
            
            isPrinting = false;
        }
            
          
            
      

    }
}

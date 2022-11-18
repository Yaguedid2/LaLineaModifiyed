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
        StartCoroutine(generateText());
    }

    // Update is called once per frame
    void Update()
    {
        if (whatToSay.Count != 0 && !isPrinting)
            StartCoroutine(generateText());
    }
    public void Speak(string phrase)
    {
        whatToSay.Add(phrase);
       
    }
    IEnumerator generateText()
    {
        isPrinting = true;
             string[] words = whatToSay[0].Split(' ');
            for (int i = 0; i < words.Length; i++)
            {
                Vector2 headPosition = new Vector2(player.GetComponent<Collider>().bounds.size.x + player.transform.position.x, player.transform.position.y + player.GetComponent<Collider>().bounds.size.y);
                Debug.Log(headPosition);
                float offsetX = Random.RandomRange(3f, 4f);
                float offsetY = Random.RandomRange(0f, 3f);
                TextMeshPro t = Instantiate(text);
                t.transform.position = new Vector2(headPosition.x - offsetX, headPosition.y - offsetY);
                t.text = words[i];
                t.GetComponent<Rigidbody>().velocity = new Vector3(0, 1, 0);
                Destroy(t.gameObject, timeBetweenWords / 2);
                yield return new WaitForSeconds(timeBetweenWords);

            }
            whatToSay.RemoveAt(0);
        isPrinting = false;
            
      

    }
}

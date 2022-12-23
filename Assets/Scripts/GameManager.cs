using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool GameLoaded = false;
    public GameObject LoadingPanel;
   
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        LoadingPanel.SetActive(false);
       
    }
    bool oneTime = true;

    // Update is called once per frame
    void Update()
    {
        showLoading();
       
        
    }
    void showLoading()
    {
        if (GameLoaded == false)
            LoadingPanel.SetActive(true);
        else
        {
            LoadingPanel.SetActive(false);
           
        }
    }
    public void pauseUnpauseGame(bool pause)
    {
        if (pause)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MazeGameManager : MonoBehaviour
{
    public bool fps, tps;
    public Transform camFps, camTps;
    public GameObject vFps, vTps;
    public PlayerMazeController player;
    public static MazeGameManager instance;
    public GameObject fpsJoystick, tpsJoystsick;
    public GameObject generator;
    float pollingTime = 1f;
    float time;
    int frameCount;
    public TextMeshProUGUI fpsText;

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
        time += Time.deltaTime;
        frameCount++;
        if(time>=pollingTime)
        {
            int frameRate = Mathf.RoundToInt(frameCount / time);
            fpsText.text = frameRate.ToString();
            time -= pollingTime;
            frameCount = 0;
        }
    }
    public void _fps()
    {
        fps = true;
        tps = false;
        camTps.gameObject.SetActive(false);
        vTps.gameObject.SetActive(false);
        vFps.gameObject.SetActive(true);
        camFps.gameObject.SetActive(true);
       
        player.cam = camFps;

    }
    public void _tps()
    {
        fps = false;
        tps = true;
        camTps.gameObject.SetActive(true);
        vTps.gameObject.SetActive(true);
        vFps.gameObject.SetActive(false);
        camFps.gameObject.SetActive(false);
       
        player.cam = camTps;
    }
}

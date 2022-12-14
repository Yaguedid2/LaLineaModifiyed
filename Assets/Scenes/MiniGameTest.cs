using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
      void OnGUI()
        {
            //This displays a Button on the screen at position (20,30), width 150 and height 50. The button’s text reads the last parameter. Press this for the SceneManager to load the Scene.
            if (GUI.Button(new Rect(20, 30, 150, 30), "primal"))
            {
                //The SceneManager loads your new Scene as a single Scene (not overlapping). This is Single mode.
                SceneManager.LoadScene("primal", LoadSceneMode.Single);
            }

            //Whereas pressing this Button loads the Additive Scene.
            if (GUI.Button(new Rect(20, 60, 150, 30), "mibiGame"))
            {
                //SceneManager loads your new Scene as an extra Scene (overlapping the other). This is Additive mode.
                SceneManager.LoadScene("miniGame", LoadSceneMode.Single);
            }
        }
    
}

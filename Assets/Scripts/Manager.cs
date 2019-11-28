using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // Quit if esc is pressed
        if(Input.GetKeyDown("escape")){
            Application.Quit();
        }

        // Reset scene if space key pressed or food over
        if(Input.GetKeyDown("space") || Time.timeSinceLevelLoad > 7f){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}

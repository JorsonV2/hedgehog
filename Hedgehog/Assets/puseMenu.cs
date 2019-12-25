using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class puseMenu : MonoBehaviour
{
    public Canvas canvas;
    public Button resume;
    public Button quit;
    // Start is called before the first frame update
    void Start()
    {
        canvas.enabled = false;
        resume.onClick.AddListener(Resume);
        quit.onClick.AddListener(Quit);
    }
    void Resume(){
        Time.timeScale = 1;
        canvas.enabled = false;
    }

    void Quit(){
        Application.Quit();
    }

}

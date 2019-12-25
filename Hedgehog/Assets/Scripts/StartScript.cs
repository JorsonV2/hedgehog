using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{
    public Button button;
    public Button quit;
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(ClickStart);
        quit.onClick.AddListener(ClickQuit);
    }

    void ClickStart(){
        SceneManager.LoadScene("Level1");
    }
    void ClickQuit(){
        Application.Quit();
    }
}

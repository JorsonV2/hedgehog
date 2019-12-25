using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seasonTrigger : MonoBehaviour
{
    public string triggerdSeason;
    public GameController gameController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "player"){
            gameController.UpdateSeason(triggerdSeason);
        }
    }
}

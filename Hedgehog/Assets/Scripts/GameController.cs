using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Canvas canvas;
    public float colorLerpSpeed = 1;
    public float leavesFadeSpeed = 1;
    public Color springLeavesColor;
    public Color summerLeavesColor;
    public Color autumnLeavesColor;
    public Color springGrassColor;
    public Color summerGrassColor;
    public Color autumnGrassColor;
    public Color winterGrassColor;
    public Material Leaves;
    public Material Grass;

    private string saeson = "spring";
    private float colorLerpValue = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("escape")){
            canvas.enabled = true;
            Time.timeScale = 0;
        }

        if(colorLerpValue < 1)
            colorLerpValue += colorLerpSpeed * Time.deltaTime;
        if(colorLerpValue > 1)
            colorLerpValue = 1;

        if(saeson == "spring"){
            Leaves.color = springLeavesColor;
            Grass.color = springGrassColor;
        }
        else if(saeson == "summer"){
            Leaves.color = Color.Lerp(springLeavesColor, summerLeavesColor, colorLerpValue);
            Grass.color = Color.Lerp(springGrassColor, summerGrassColor, colorLerpValue);
        }
        else if(saeson == "autumn"){
            Leaves.color = Color.Lerp(summerLeavesColor, autumnLeavesColor, colorLerpValue);
            Grass.color = Color.Lerp(summerGrassColor, autumnGrassColor, colorLerpValue);
        }
        else if(saeson == "winter"){
            Grass.color = Color.Lerp(autumnGrassColor, winterGrassColor, colorLerpValue);
            Color cl = Leaves.color;
            if(cl.a > 0)
                cl.a -= leavesFadeSpeed * Time.deltaTime;
            else 
                cl.a = 0;
            Leaves.color = cl;
        }
    }

    public void UpdateSeason(string newSeason){
        if(saeson != newSeason){
            saeson = newSeason;
            colorLerpValue = 0;
        }  
    }

}

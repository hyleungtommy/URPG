using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;
public class DifficultyScene : MonoBehaviour
{
    public Text textDifficulty;
    // Start is called before the first frame update
    void Start()
    {
        render();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void render(){
        textDifficulty.text = "Current Difficulty:\n" + Param.difficulty[Game.difficulty];
    }

    public void onClickButton(int id){
        Game.difficulty = id;
        SaveManager.saveValue(SaveKey.difficulty,Game.difficulty);
        render();
    }
}

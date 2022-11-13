using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG;
public class CraftMenuScene : BasicScene
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClickButton(int id){
        switch(id){
            case 0:
                jumpToScene(SceneName.Smithing);
            break;
            case 1:
                jumpToScene(SceneName.Brewing);
            break;
            case 2:
            break;
            case 3:
            break;
            default:
            break;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG;
public class TownshipScene : BasicScene
{
    public HeaderCtrl header;
    // Start is called before the first frame update
    void Start()
    {
        Game.town.OfflineProgress();
        header.render();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickButton(int id){
        switch(id){
            case 0:
                jumpToScene("Townhall");
            break;
            case 1:
                Game.inventorySceneType = "warehouse";
                jumpToScene("Inventory");
            break;
            default:
            break;
        }
    }

}

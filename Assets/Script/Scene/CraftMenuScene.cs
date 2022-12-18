using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG;
public class CraftMenuScene : BasicScene
{
    
    // Start is called before the first frame update
    void Start()
    {
        //for testing
        //Debug.Log("test script started, insert equipments for crafting test");
        //Game.inventory.smartInsert(DB.equipments[4].toEquipment(0),1);
        /*
        Game.inventory.smartInsert(DB.equipments[4].toEquipment(0),1);
        Game.inventory.smartInsert(DB.equipments[5].toEquipment(0),1);
        Game.inventory.smartInsert(DB.equipments[6].toEquipment(0),1);
        Game.inventory.smartInsert(DB.equipments[7].toEquipment(0),1);
        Game.inventory.smartInsert(DB.equipments[8].toEquipment(0),1);
        Game.inventory.smartInsert(DB.equipments[9].toEquipment(0),1);
        Game.inventory.smartInsert(DB.equipments[10].toEquipment(0),1);
        Game.inventory.smartInsert(DB.equipments[11].toEquipment(0),1);
        Game.inventory.smartInsert(DB.equipments[12].toEquipment(0),1);
        Game.inventory.smartInsert(DB.equipments[13].toEquipment(0),1);
        */
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
                jumpToScene(SceneName.Reinforcing);
            break;
            case 3:
                jumpToScene(SceneName.Enchanting);
            break;
            default:
            break;
        }
    }
}

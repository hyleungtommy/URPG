using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;
public class DungeonScene : BasicScene
{
    public Text dungeonInfo;
    public Image dungeonImage;
    public Text roomText;
    public GameObject singleOptionButtonMenu;
    public GameObject doubleOptionButtonMenu;
    public GameObject tripleOptionButtonMenu;
    public GameObject quadOptionButtonMenu;
    public Text singleOptionButtonText;
    public Text doubleOptionButton1Text;
    public Text doubleOptionButton2Text;
    public Text tripleOptionButton1Text;
    public Text tripleOptionButton2Text;
    public Text tripleOptionButton3Text;
    public Text quadOptionButton1Text;
    public Text quadOptionButton2Text;
    public Text quadOptionButton3Text;
    public Text quadOptionButton4Text;
    public GameObject confirmLeaveDialog;
    public GameObject lootDialog;
    public InvBox[] lootBox;
    Dungeon dungeon;
    // Start is called before the first frame update
    void Start()
    {
        dungeon = Game.currentDungeon;
        confirmLeaveDialog.SetActive(false);
        lootDialog.SetActive(false);
        Render();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Render(){
        dungeonInfo.text = dungeon.dungeonName + "\nFloor" + (dungeon.currentFloorNum + 1);
        dungeonImage.sprite = dungeon.currentRoom.img;
        roomText.text = dungeon.currentRoom.desc;

        string[]buttonTexts = dungeon.currentRoom.buttonTexts;
        singleOptionButtonMenu.gameObject.SetActive(buttonTexts.Length == 1);
        doubleOptionButtonMenu.gameObject.SetActive(buttonTexts.Length == 2);
        tripleOptionButtonMenu.gameObject.SetActive(buttonTexts.Length == 3);
        quadOptionButtonMenu.gameObject.SetActive(buttonTexts.Length == 4);

        if(buttonTexts.Length == 1){
            singleOptionButtonText.text = buttonTexts[0];
        }else if(buttonTexts.Length == 2){
            doubleOptionButton1Text.text = buttonTexts[0];
            doubleOptionButton2Text.text = buttonTexts[1];
        }else if(buttonTexts.Length == 3){
            tripleOptionButton1Text.text = buttonTexts[0];
            tripleOptionButton2Text.text = buttonTexts[1];
            tripleOptionButton3Text.text = buttonTexts[2];
        }else if(buttonTexts.Length == 4){
            quadOptionButton1Text.text = buttonTexts[0];
            quadOptionButton2Text.text = buttonTexts[1];
            quadOptionButton3Text.text = buttonTexts[2];
            quadOptionButton4Text.text = buttonTexts[3];
        }
    }

    public void OnClickOptions(int option){
        Dungeon.Action Action = dungeon.ChooseOption(option);
        if(Action == Dungeon.Action.None){
            Render();
        }else if(Action == Dungeon.Action.EnterBattle){
            dungeon.ChangeCurrentRoomType();
            jumpToScene(SceneName.Battle);
        }else if(Action == Dungeon.Action.ShowLoot){
            dungeon.ChangeCurrentRoomType();
            RenderLootDialog();
            Render();
        }
        
    }

    public void OnClickLeave(){
        confirmLeaveDialog.SetActive(true);
    }

    public void OnClickConfirmLeave(){
        jumpToScene(SceneName.MainMenu);
    }

    public void OnClickCancel(){
        confirmLeaveDialog.SetActive(false);
    }

    void RenderLootDialog(){
        lootDialog.SetActive(true);
        StorageSystem lootStorage = dungeon.GetLoot();
        for(int i = 0 ; i < lootBox.Length ; i++){
            lootBox[i].gameObject.SetActive(i < lootStorage.getSize());
            if(i < lootStorage.getSize()){
                lootBox[i].setStorageSlot(lootStorage.getSlot(i));
                lootBox[i].render();
                Game.inventory.smartInsert(lootStorage.getSlot(i).getContainment(),lootStorage.getSlot(i).getQty());
            }
        }
    }

    public void OnClickDismissLottDialog(){
        lootDialog.SetActive(false);
    }
    

}

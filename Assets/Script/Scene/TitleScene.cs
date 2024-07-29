using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;
using System.IO;
public class TitleScene : BasicScene
{
    public CheatMenuCtrl cheatMenu;
    public GameObject buttonContinue;
    public GameObject buttonLoadGame;
    public GameObject saveMenu;
    public GameObject buttonMenu;
    public GameObject buttonReturn;
    public SaveButtonCtrl[] saveButtonCtrls;
    bool playedBefore;
    int saveMenuMode;//0 = create new game, 1 = load game

    // Start is called before the first frame update
    void Start()
    {
        cheatMenu.gameObject.SetActive(false);
        saveMenu.gameObject.SetActive(false);
        buttonReturn.gameObject.SetActive(false);
        playedBefore = SaveManager.getBool(SaveKey.played_before);
        buttonContinue.gameObject.SetActive(playedBefore);
        buttonLoadGame.gameObject.SetActive(playedBefore);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClickContinue()
    {
        DB.LoadGameData();
        Game.initialize();
        if (SaveManager.getBool(SaveKey.played_before))
        {
            int currentSlotId = SaveManager.getInt(SaveKey.current_save_slot);
            Game.LoadGame(currentSlotId);
            jumpToScene(SceneName.MainMenu);
        }
        else
        {
            if (Param.isDevelopment)
            {
                cheatMenu.gameObject.SetActive(true);
            }
            else
            {
                SaveManager.saveValue(SaveKey.played_before, true);
                SaveManager.save();
                jumpToScene(SceneName.MainMenu);
            }
        }
    }

    public void OnClickNewGame(){
        saveMenuMode = 0;
        ShowSaveMenu();
    }

    void ShowSaveMenu(){
        saveMenu.gameObject.SetActive(true);
        buttonMenu.gameObject.SetActive(false);
        buttonReturn.gameObject.SetActive(true);
        int i = 0;
        foreach(SaveButtonCtrl saveButtonCtrl in saveButtonCtrls){
            SaveData saveData = LoadSave(i);
            saveButtonCtrl.Render(saveData);
            i++;
        }
    }

    public void OnClickLoadGame(){
        saveMenuMode = 1;
        ShowSaveMenu();
    }

    SaveData LoadSave(int saveNo){
        string path = Application.dataPath + "/Resources/Save/save" + (saveNo + 1) + ".json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<SaveData>(json);
        }else{
            return null;
        }
    }

    public void OnClickSaveSlot(int i){
        if(saveMenuMode == 0){// New Game
            if(!saveButtonCtrls[i-1].isEmptySlot){
                return;
            }
            SaveManager.saveValue(SaveKey.current_save_slot, i);
            SaveManager.save();
            DB.LoadGameData();
            Game.initialize();
            Game.SaveGame();

            if (Param.isDevelopment)
            {
                cheatMenu.gameObject.SetActive(true);
                saveMenu.gameObject.SetActive(false);
                buttonReturn.gameObject.SetActive(false);
            }
            else
            {
                SaveManager.saveValue(SaveKey.played_before, true);
                SaveManager.save();
                jumpToScene(SceneName.MainMenu);
            }
        }else{//Load Game
            if(saveButtonCtrls[i-1].isEmptySlot){
                return;
            }
            SaveManager.saveValue(SaveKey.current_save_slot, i);
            SaveManager.save();
            DB.LoadGameData();
            Game.initialize();

            Game.LoadGame(i);
            jumpToScene(SceneName.MainMenu);
        }
    }

    public void OnClickReturn(){
        saveMenu.gameObject.SetActive(false);
        buttonMenu.gameObject.SetActive(true);
        buttonReturn.gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;
public class SettingScene : BasicScene
{
    public HeaderCtrl header;
    public GameObject resetPopbox;
    // Start is called before the first frame update
    void Start()
    {
        header.render();
        resetPopbox.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void onClickResetSave()
    {
        resetPopbox.gameObject.SetActive(true);
    }

    public void onClickSaveGame()
    {
        Game.saveGame();
    }

    public void onClickQuitGame()
    {
        Game.saveGame();
        Application.Quit();
    }

    public void onClickResetYes()
    {
        resetPopbox.gameObject.SetActive(false);
        Game.resetSave();
        Application.Quit();
    }

    public void onClickResetNo()
    {
        resetPopbox.gameObject.SetActive(false);
    }

    public void onClickDifficulty(){
        jumpToScene(SceneName.Difficulty);
    }

    public void OnClickCheatScene(){
        jumpToScene(SceneName.Cheat);
    }
}

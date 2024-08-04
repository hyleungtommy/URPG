using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;
public class MainMenuScene : BasicScene
{
    public HeaderCtrl header;
    public Image bg;
    public Button buttonToCity;
    public Text dunegonText;
    public GameObject dungeonPanel;

    // Start is called before the first frame update
    void Start()
    {
        render();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void render()
    {
        header.render();
        bg.sprite = Game.currLoc.bgImg;
        buttonToCity.gameObject.SetActive(Game.currLoc.townFacility.Length > 0);

        dungeonPanel.gameObject.SetActive(Game.currLoc.dungeonId > 0);
        if(Game.currLoc.dungeonId > 0){
            dunegonText.text = "Enter dungeon:" + DB.QueryDungeon(Game.currLoc.dungeonId).name + ", require a map";
        }
    }

    public void onClickToCity()
    {
        Game.currInCity = true;
        jumpToScene(SceneName.City);
    }

    public void onClickProgressiveMode()
    {
        if (Game.currLoc != null)
        {
            Game.currentMapMode = Constant.MapModeProgressive;
            jumpToScene(SceneName.Battle);
        }
    }

    public void onClickExploreMode()
    {
        if (Game.currLoc != null)
        {
            Game.currentMapMode = Constant.MapModeExplore;
            jumpToScene(SceneName.Battle);
        }
    }

    public void onClickDungeon(){
        if(Game.currLoc.dungeonId > 0){
            Game.currentMapMode = Constant.MapModeDungeon;
            Game.currentDungeon = new Dungeon(DungeonGenerator.GenerateDungeon(Game.currLoc.dungeonId), Game.currLoc.dungeonId);
            jumpToScene(SceneName.Dungeon);
        }
    }

    void OnApplicationQuit()
    {
        Debug.Log("Save Game start");
        Game.SaveGame();
    }
}

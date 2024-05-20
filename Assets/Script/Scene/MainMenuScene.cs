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

    void OnApplicationQuit()
    {
        Debug.Log("Save Game start");
        Game.saveGame();
    }
}

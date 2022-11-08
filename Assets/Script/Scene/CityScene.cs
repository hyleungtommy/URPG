using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;

public class CityScene : BasicScene
{
    public HeaderCtrl header;
    public Image townBG;
    public Button[]buttons;
    // Start is called before the first frame update
    void Start()
    {
        header.render();
        townBG.sprite = Game.currLoc.townbg;
        List<string>list = new List<string>(Game.currLoc.townFacility);
        buttons[0].gameObject.SetActive(list.Contains("Shop"));
        buttons[1].gameObject.SetActive(list.Contains("SkillCenter"));
        buttons[2].gameObject.SetActive(list.Contains("Blacksmith"));
        buttons[3].gameObject.SetActive(list.Contains("Guild"));
        buttons[4].gameObject.SetActive(list.Contains("Camp"));

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void onClickBtn(int id)
    {
        switch (id)
        {
            case 0:
                jumpToScene(SceneName.Shop);
                break;
            case 1:
                jumpToScene(SceneName.SkillCenter);
                break;
            case 2:
                jumpToScene(SceneName.Blacksmith);
                break;
            default:
                break;
        }
    }

    public void onClickToWilderness()
    {
        Game.currInCity = false;
        jumpToScene(SceneName.MainMenu);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;
public class TitleScene : BasicScene
{
    public GameObject cheatMenu;
    public Toggle tgUnlockAllMember;
    public Toggle tgUnlockAllSkill;

    // Start is called before the first frame update
    void Start()
    {
        cheatMenu.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void onClickStartGame()
    {
        if (SaveManager.getBool(SaveKey.played_before))
        {
            Game.loadGame();
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

    public void onClickCheatMenuConfirm()
    {
        if (tgUnlockAllMember.isOn)
        {
            Game.party.unlockAllMember();
        }
        if (tgUnlockAllSkill.isOn)
        {
            Game.party.learntAllSkill();
        }
        SaveManager.saveValue(SaveKey.played_before, true);
        SaveManager.save();
        jumpToScene(SceneName.MainMenu);
    }
}

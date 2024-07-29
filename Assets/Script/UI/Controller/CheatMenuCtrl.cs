using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;
public class CheatMenuCtrl : MonoBehaviour
{
    public Toggle tgUnlockAllMember;
    public Toggle tgUnlockAllSkill;
    public BasicScene basicScene;

    public void OnClickCheatMenuConfirm()
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
        basicScene.jumpToScene(SceneName.MainMenu);
    }
}
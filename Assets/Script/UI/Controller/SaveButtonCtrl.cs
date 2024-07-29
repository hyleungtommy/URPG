using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;
public class SaveButtonCtrl : MonoBehaviour
{
    public Text mainText;
    public GameObject preview;
    public Text mapName;
    public Text currLoc;
    public Text money;
    public bool isEmptySlot;

    public void Render(SaveData saveData){
        if(saveData == null){
            preview.gameObject.SetActive(false);
            mainText.text = "Empty Slot";
            isEmptySlot = true;
        }else{
            preview.gameObject.SetActive(true);
            mainText.text = saveData.playerName + " Lv." + saveData.playerLv;
            money.text = saveData.playerMoney.ToString();
            currLoc.text = saveData.previewMapName;
            mapName.text = saveData.previewMapLoc;
            isEmptySlot = false;
        }
    }
}
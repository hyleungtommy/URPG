using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;

public class GrowthScene : BasicScene
{
    public Text textUPPT;
    public Text textStaminaPts;
    public Text textStaminaAddPts;
    public Text textStrengthPts;
    public Text textStrengthAddPts;
    public Text textManaPts;
    public Text textManaAddPts;
    public Text textAgilityPts;
    public Text textAgilityAddPts;
    public Text textDexterityPts;
    public Text textDexterityAddPts;
    public Button buttonStamina;
    public Button buttonStrength;
    public Button buttonMana;
    public Button buttonAgility;
    public Button buttonDexterity;
    public Button buttonConfirm;
    public Button buttonCancel;
    public Text textCharName;
    BattleCharacter character;
    int remainingUPPT;
    int[]upptTempAlloc = {0,0,0,0,0};
    // Start is called before the first frame update
    void Start()
    {
        character = Game.party.getAllUnlockedCharacter()[Game.selectedCharacterInStatusScene];
        render();
    }

    void Awake() {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 30;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void render(){
        remainingUPPT = character.uppt - (int)Util.getSumOfArray(upptTempAlloc);
        textCharName.text = character.name + " Lv." + character.lv;
        textUPPT.text = remainingUPPT.ToString();
        textStaminaPts.text = character.stat.stamina.ToString();
        textStaminaAddPts.text = (upptTempAlloc[0] > 0 ? "(+" + upptTempAlloc[0] + ")" : "");
        textStrengthPts.text = character.stat.strength.ToString();
        textStrengthAddPts.text = (upptTempAlloc[1] > 0 ? "(+" + upptTempAlloc[1] + ")" : "");
        textManaPts.text = character.stat.mana.ToString();
        textManaAddPts.text = (upptTempAlloc[2] > 0 ? "(+" + upptTempAlloc[2] + ")" : "");
        textAgilityPts.text = character.stat.agility.ToString();
        textAgilityAddPts.text = (upptTempAlloc[3] > 0 ? "(+" + upptTempAlloc[3] + ")" : "");
        textDexterityPts.text = character.stat.dexterity.ToString();
        textDexterityAddPts.text = (upptTempAlloc[4] > 0 ? "(+" + upptTempAlloc[4] + ")" : "");
        buttonStamina.gameObject.SetActive(remainingUPPT > 0);
        buttonStrength.gameObject.SetActive(remainingUPPT > 0);
        buttonMana.gameObject.SetActive(remainingUPPT > 0);
        buttonAgility.gameObject.SetActive(remainingUPPT > 0);
        buttonDexterity.gameObject.SetActive(remainingUPPT > 0);
        buttonConfirm.gameObject.SetActive(character.uppt > 0 && (int)Util.getSumOfArray(upptTempAlloc) > 0);
        buttonCancel.gameObject.SetActive(character.uppt > 0 && (int)Util.getSumOfArray(upptTempAlloc) > 0);
    }

    public void onClickAddPtsButton(int id){
        upptTempAlloc[id] ++;
        render();
    }

    public void onClickResetButton(){
        upptTempAlloc = new int[]{0,0,0,0,0};
        render();
    }

    public void onClickConfirmButton(){
        character.assignUPPT(upptTempAlloc);
        upptTempAlloc = new int[]{0,0,0,0,0};
        render();
    }

    public override void onSelectCharacter(int id, BattleCharacter character)
    {
        base.onSelectCharacter(id, character);
        if(character != null){
            this.character = character;
            upptTempAlloc = new int[]{0,0,0,0,0};
            render();
        }
    }
}

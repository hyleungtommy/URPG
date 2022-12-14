using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;

public class RewardPanelCtrl : MonoBehaviour
{
    public Image title;
    public Text textMoney;
    public Text textEXP;
    public Button btnRebattle;
    public Button btnNextArea;
    public EXPGainCtrl[] expGain;
    public ItemBox[] enemyDropDisplays;
    BattleCtrl battleCtrl;
    BattleScene scene;
    List<ItemAndQty>drops;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setBattleCtrl(BattleScene scene, BattleCtrl battleCtrl)
    {
        this.battleCtrl = battleCtrl;
        this.scene = scene;
    }

    public void setEnemyDrop(List<ItemAndQty>drops){
        this.drops = drops;
    }

    public void show()
    {
        gameObject.SetActive(true);
        if (battleCtrl.battleState == BattleCtrl.PLAYER_WIN)
        {
            title.sprite = SpriteManager.youWin;
            textMoney.text = battleCtrl.getTotalMoneyGain().ToString();
            textEXP.text = battleCtrl.getTotalEXPGain().ToString();
            BattleCharacter[] characters = Game.party.getAllBattleCharacter();
            for (int i = 0; i < 4; i++)
            {
                expGain[i].gameObject.SetActive(false);
            }
            for (int i = 0; i < characters.Length; i++)
            {
                expGain[i].gameObject.SetActive(true);
                expGain[i].setCharacter(characters[i], battleCtrl.levelUps[i]);
                expGain[i].render();
            }
            for(int i = 0; i < enemyDropDisplays.Length ; i++){
                if(i >= drops.Count){
                    enemyDropDisplays[i].gameObject.SetActive(false);
                }else{
                    enemyDropDisplays[i].gameObject.SetActive(true);
                    enemyDropDisplays[i].render(drops[i].item);
                }
            }
        }
        else
        {
            title.sprite = SpriteManager.youLose;
            textMoney.text = "0";
            textEXP.text = "0";
            for (int i = 0; i < expGain.Length; i++)
            {
                expGain[i].gameObject.SetActive(false);
            }
            for(int i = 0; i < enemyDropDisplays.Length ; i++){
                enemyDropDisplays[i].gameObject.SetActive(false);
            }
        }
        btnRebattle.gameObject.SetActive(Game.currentMapMode == Constant.MapModeExplore);
        btnNextArea.gameObject.SetActive(Game.currentMapMode == Constant.MapModeProgressive && battleCtrl.battleState == BattleCtrl.PLAYER_WIN);

    }

    public void onClickRewardPanelButton(int id)
    {
        scene.onClickRewardPanelButton(id);
    }
}

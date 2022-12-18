using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;
using UnityEngine.SceneManagement;
public class BattleScene : BasicScene
{
    public EnemyStatCtrl[] enemyStats;
    public CharacterStatCtrl[] playerStats;
    public EnemyStatCtrl bossStat;
    public GameObject topBar;
    public Text topBarText;
    BattleCtrl battleCtrl;
    public Image battleBG;
    public GameObject actionBtnGrp;
    public RewardPanelCtrl rewardPanel;
    public GameObject areaPanel;
    public Text textAreaPanel;
    public ItemPanelCtrl itemPanel;
    public SkillPanelCtrl skillPanel;

    private bool showingRewardPanel;// prevent multi update of reward panel;
    // Start is called before the first frame update
    void Start()
    {
        prepareBattle();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.P))
        {
            battleCtrl.skipBattle();
        }
    }

    void prepareBattle()
    {
        PlotData pd = PlotMatcher.matchPlotBattle(Game.currLoc.currArea, true);
        if (pd != null)
        {
            Debug.Log("Plot found, pt=" + pd.triggerPt + ", area=" + pd.triggerArea + ",before battle=" + pd.triggerBeforeBattle);
            PlotMatcher.nextPlot = pd;
            PlotMatcher.nextScene = SceneName.Battle;
            SceneManager.LoadScene(SceneName.Dialog);
        }
        else
        {
            battleBG.sprite = Game.currLoc.battleImg;
            battleCtrl = new BattleCtrl(Game.currLoc.generateEnemy(), Game.party.createBattleParty(), this);
            if (Game.currentMapMode == Constant.MapModeProgressive)
            {
                textAreaPanel.text = Game.currLoc.currArea + "/" + Game.currLoc.maxArea;
            }
            areaPanel.gameObject.SetActive(Game.currentMapMode == Constant.MapModeProgressive);

            if (battleCtrl.bossFight)
            {
                bossStat.gameObject.SetActive(true);
                bossStat.setEntity(battleCtrl.enemyParty[0]);
                for (int i = 0; i < enemyStats.Length; i++)
                {
                    enemyStats[i].gameObject.SetActive(false);
                }
            }
            else
            {
                for (int i = 0; i < enemyStats.Length; i++)
                {
                    if (i < battleCtrl.enemyParty.Length)
                    {
                        enemyStats[i].gameObject.SetActive(true);
                        enemyStats[i].setEntity(battleCtrl.enemyParty[i]);
                    }
                    else
                    {
                        enemyStats[i].gameObject.SetActive(false);
                    }
                }
            }

            for (int i = 0; i < playerStats.Length; i++)
            {
                if (i < battleCtrl.playerParty.Length)
                {
                    playerStats[i].gameObject.SetActive(true);
                    playerStats[i].setEntity(battleCtrl.playerParty[i]);
                }
                else
                {
                    playerStats[i].gameObject.SetActive(false);
                }
            }
            showingRewardPanel = false;
            rewardPanel.gameObject.SetActive(false);
            itemPanel.gameObject.SetActive(false);
            skillPanel.gameObject.SetActive(false);
            StartCoroutine("tick");
        }

    }

    void render()
    {
        if (battleCtrl.bossFight)
        {
            bossStat.render();
        }
        else
        {
            for (int i = 0; i < enemyStats.Length; i++)
            {
                enemyStats[i].render();
            }
        }

        for (int i = 0; i < playerStats.Length; i++)
        {
            playerStats[i].render();
        }
    }

    IEnumerator tick()
    {
        while (battleCtrl.battleState == BattleCtrl.BATTLE_RUNNING || battleCtrl.battleState == BattleCtrl.PLAYER_TURN)
        {
            if (battleCtrl.battleState == BattleCtrl.BATTLE_RUNNING)
                battleCtrl.tick();
            render();
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void setTopBar(string name, Sprite img)
    {
        topBarText.text = name + " : Please choose action";
    }

    public void onPlayerTurn()
    {
        battleCtrl.selectionMode = BattleCtrl.SELECTION_NONE;
        actionBtnGrp.SetActive(true);
        topBar.gameObject.SetActive(true);
    }

    public void onEnemyDefeated(int index)
    {
        if (battleCtrl.bossFight)
        {
            bossStat.gameObject.SetActive(false);
        }
        else
        {
            enemyStats[index].gameObject.SetActive(false);
        }

    }

    public void onSelectEnemy(int index)
    {
        if (battleCtrl.battleState == BattleCtrl.PLAYER_TURN)
        {
            battleCtrl.selectEnemy(index);
            if (battleCtrl.selectionMode == BattleCtrl.SELECTION_ATTACK)
            {
                battleCtrl.playerUseNormalAttack();
                if (battleCtrl.actionEntity != null && battleCtrl.actionEntity is EntityPlayer)
                {
                    //StartCoroutine(changeCharacterFace(battleCtrl.ActionEntity.Id));

                    if (battleCtrl.bossFight)
                    {
                        bossStat.render();
                    }
                    else
                    {
                        for (int i = 0; i < enemyStats.Length; i++)
                        {
                            enemyStats[i].render();
                        }
                    }

                }
                actionBtnGrp.SetActive(false);
            }
            else if (battleCtrl.selectionMode == BattleCtrl.SELECTION_SKILL_USE_ON_ENEMY)
            {

                battleCtrl.useSelectedSpecial();
                if (battleCtrl.actionEntity != null && battleCtrl.actionEntity is EntityPlayer)
                {
                    //					Debug.Log (battle.ActionEntity.Id);
                    //StartCoroutine (changeCharacterFace (battleCtrl.ActionEntity.Id));
                    for (int i = 0; i < playerStats.Length; i++)
                    {
                        playerStats[i].render();
                    }
                }
                actionBtnGrp.SetActive(false);

            }
            //battle.SelectionMode = BattleHandler.SELECTION_NONE;
        }
    }

    public void onSelectPlayer(int index)
    {
        // when a party member is selected (by either using Item or Skill on Player Party)
        if (battleCtrl.battleState == BattleCtrl.PLAYER_TURN)
        {
            battleCtrl.selectPlayer(index);
            if (battleCtrl.selectionMode == BattleCtrl.SELECTION_ITEM)
            {
                battleCtrl.useSelectedSpecial();
            }
            else if (battleCtrl.selectionMode == BattleCtrl.SELECTION_SKILL_USE_ON_PARTNER)
            {
                battleCtrl.useSelectedSpecial();
            }
        }

    }

    public void onSelectItem(int elementId)
    {
        int itemId = itemPanel.elements[elementId].itemId;
        if (itemId >= 0)
        {
            battleCtrl.onSelectItem(itemId);
            itemPanel.gameObject.SetActive(false);
            topBarText.text = Constant.topBarSelectPlayer;
        }
    }

    public void onSelectSkill(int elementId)
    {
        int skillId = skillPanel.elements[elementId].skillId;
        Skill s = skillPanel.elements[elementId].skill;
        if (skillId >= 0 && s != null)
        {
            if (battleCtrl.battleState == BattleCtrl.PLAYER_TURN)
            {
                battleCtrl.selectedSkill = s;
                if (s.aoe)
                {
                    if (s.isAttackSkill())
                    {
                        //Debug.Log(1);
                        battleCtrl.selectionMode = BattleCtrl.SELECTION_SKILL_USE_ON_ENEMY;
                        battleCtrl.useSelectedSpecial();
                    }
                    else
                    {
                        //Debug.Log(2);
                        battleCtrl.selectionMode = BattleCtrl.SELECTION_SKILL_USE_ON_PARTNER;
                        battleCtrl.useSelectedSpecial();
                    }
                }
                else
                {
                    if (s.isAttackSkill())
                    {
                        //Debug.Log(3);
                        battleCtrl.selectionMode = BattleCtrl.SELECTION_SKILL_USE_ON_ENEMY;
                        topBarText.text = Constant.topBarSelectAnEnemy;
                    }
                    else if ((s is SkillBuff && s.useOnSelf) || s is SkillDefense)
                    {
                        battleCtrl.selectionMode = BattleCtrl.SELECTION_SKILL_USE_ON_PARTNER;
                        battleCtrl.useSelectedSpecial();
                    }
                    else
                    {
                        //Debug.Log(4);
                        battleCtrl.selectionMode = BattleCtrl.SELECTION_SKILL_USE_ON_PARTNER;
                        topBarText.text = Constant.topBarSelectPlayer;
                    }
                }
            }
            skillPanel.gameObject.SetActive(false);
        }
    }

    public void onClickAttackButton()
    {
        if (battleCtrl.battleState == BattleCtrl.PLAYER_TURN)
        {
            battleCtrl.selectionMode = BattleCtrl.SELECTION_ATTACK;
            //setEnemySelectionArrowActive(true);
            itemPanel.gameObject.SetActive(false);
            skillPanel.gameObject.SetActive(false);
            topBar.gameObject.SetActive(true);
            topBarText.text = Constant.topBarSelectAnEnemy;
            //setPlayerSelectionArrowActive(true);
        }
    }

    public void onClickItemButton()
    {
        if (battleCtrl.battleState == BattleCtrl.PLAYER_TURN)
        {
            battleCtrl.selectionMode = BattleCtrl.SELECTION_ITEM;
            topBar.gameObject.SetActive(true);
            topBarText.text = Constant.topBarSelectAnItem;
            itemPanel.setVirtualInventory(battleCtrl.virtualInventory);
            itemPanel.gameObject.SetActive(true);
            skillPanel.gameObject.SetActive(false);
            itemPanel.render();
        }
    }

    public void onClickSkillButton()
    {
        if (battleCtrl.battleState == BattleCtrl.PLAYER_TURN)
        {
            battleCtrl.selectionMode = BattleCtrl.SELECTION_SKILL_PENDING;
            topBar.gameObject.SetActive(true);
            topBarText.text = Constant.topBarSelectASkill;
            skillPanel.setSkillList((battleCtrl.actionEntity as EntityPlayer).skillList, battleCtrl.actionEntity as EntityPlayer);
            skillPanel.gameObject.SetActive(true);
            itemPanel.gameObject.SetActive(false);
            skillPanel.render();
        }
    }

    public void afterPlayerAction()
    {
        actionBtnGrp.gameObject.SetActive(false);
        //for (int j = 0; j < enemyImg.Length; j++) 
        //enemyImg [j].transform.GetChild (0).gameObject.SetActive (false);
        //setPlayerSelectionArrowActive(false);
        //setEnemySelectionArrowActive(false);
        topBar.gameObject.SetActive(false);
        if (battleCtrl.bossFight)
        {
            bossStat.render();
        }
        else
        {
            for (int i = 0; i < enemyStats.Length; i++)
            {
                enemyStats[i].render();
            }
        }
    }

    public void showRewardPanel(List<ItemAndQty>drops)
    {
        if (!showingRewardPanel)
        {
            showingRewardPanel = true;
            //rewardPanel.showRewardPanel(this, battleCtrl);
            rewardPanel.setBattleCtrl(this, this.battleCtrl);
            rewardPanel.setEnemyDrop(drops);
            rewardPanel.show();
            Game.saveGame();
            //RPGSystem.saveGame();
        }

    }

    public void onClickRewardPanelButton(int id)
    {
        PlotData pd = PlotMatcher.matchPlotBattle(Game.currLoc.currArea, false);
        switch (id)
        {
            case 0:
                jumpToScene(SceneName.MainMenu);
                Game.currLoc.resetAreaStatus();
                break;
            case 1:
                if (pd != null)
                {
                    Debug.Log("Plot found, pt=" + pd.triggerPt + ", area=" + pd.triggerArea + ",before battle=" + pd.triggerBeforeBattle);
                    PlotMatcher.nextPlot = pd;
                    PlotMatcher.nextScene = SceneName.Battle;
                    SceneManager.LoadScene(SceneName.Dialog);
                }
                else
                {
                    prepareBattle();
                }

                break;
            case 2:
                if (battleCtrl.bossFight)
                {
                    Game.currLoc.resetAreaStatus();
                    if (pd != null)
                    {
                        Debug.Log("Plot found, pt=" + pd.triggerPt + ", area=" + pd.triggerArea + ",before battle=" + pd.triggerBeforeBattle);
                        PlotMatcher.nextPlot = pd;
                        PlotMatcher.nextScene = SceneName.MainMenu;
                        SceneManager.LoadScene(SceneName.Dialog);
                    }
                    else
                    {
                        jumpToScene(SceneName.MainMenu);
                    }
                }
                else
                {
                    Game.currLoc.progressArea();
                    if (pd != null)
                    {
                        Debug.Log("Plot found, pt=" + pd.triggerPt + ", area=" + pd.triggerArea + ",before battle=" + pd.triggerBeforeBattle);
                        PlotMatcher.nextPlot = pd;
                        PlotMatcher.nextScene = SceneName.Battle;
                        SceneManager.LoadScene(SceneName.Dialog);
                    }
                    else
                    {
                        prepareBattle();
                    }
                }
                break;
        }
    }

    public void createFloatingText(List<BattleMessage> bundle)
    {
        //Debug.Log("bundle=" + bundle.Count);
        Transform trans = null;
        if (bundle != null && bundle.Count > 0)
        {
            int i = 0;
            foreach (BattleMessage message in bundle)
            {
                if ((message.sender is EntityPlayer && message.isAttackMessage()) || (message.sender is EntityEnemy && !message.isAttackMessage()))
                {
                    //Debug.Log("message.receiver.Id=" + message.receiver.Id + "enemyImg.Lengt=" + enemyImg.Length);
                    if (battleCtrl.bossFight)
                    {
                        trans = bossStat.transform;
                    }
                    else
                    {
                        trans = enemyStats[message.receiver.id].transform;
                    }

                }
                else
                {
                    trans = playerStats[message.receiver.id].transform;
                }
                //Debug.Log("transfor" + transform.ToString());
                GetComponent<BattleAnimator>().createDamageText(message, trans);

            }



            if ((bundle[0].AOE && bundle[0].sender is EntityPlayer && bundle[0].isAttackMessage()) || (bundle[0].AOE && bundle[0].sender is EntityEnemy && !bundle[0].isAttackMessage()))
            {
                if (battleCtrl.bossFight)
                {
                    GetComponent<BattleAnimator>().createSkillAnimation(bundle[0], bossStat.transform);
                }
                else
                {
                    for (int j = 0; j < enemyStats.Length; j++)
                    {
                        if (enemyStats[j].enemy.IsActive())
                            GetComponent<BattleAnimator>().createSkillAnimation(bundle[0], enemyStats[j].transform);
                    }
                }

            }
            else if ((bundle[0].AOE && bundle[0].sender is EntityEnemy && bundle[0].isAttackMessage()) || (bundle[0].AOE && bundle[0].sender is EntityPlayer && !bundle[0].isAttackMessage()))
            {
                for (int j = 0; j < playerStats.Length; j++)
                {
                    if (playerStats[j].player.IsActive())
                        GetComponent<BattleAnimator>().createSkillAnimation(bundle[0], playerStats[j].transform);
                }
            }

            // show skill name on enemyCTRL
            /*
			if (bundle [0].sender is EntityEnemy && bundle [0].type != BattleMessage.Type.NormalAttack) {
				Debug.Log ("show " + bundle [0].sender.Id);
				enemyInfo[bundle [0].sender.Id].GetComponent<BattleEnemyInfoCtrl> ().showSkillButton (bundle [0].SkillName);
			}
            */
            GetComponent<BattleAnimator>().createSkillAnimation(bundle[0], trans);

        }

    }




}

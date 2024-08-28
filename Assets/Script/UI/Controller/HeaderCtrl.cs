using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using RPG;
public class HeaderCtrl : BasicScene
{
    public Text playerName;
    public Text money;
    public Text currArea;
    public Text currLoc;
    public GameObject currAreaPanel;
    public Text textResourceWood;
    public Text textResourceFood;
    public Text textResourceStone;
    public Text textResourceMetal;
    public Text textPopulation;
    public GameObject townInfoRow;
    public bool enableTownInfoRow;
    // Start is called before the first frame update
    void Start()
    {
        render();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void render()
    {
        townInfoRow.gameObject.SetActive(enableTownInfoRow);
        playerName.text = Game.playerName;
        money.text = Game.money.ToString();
        if (Game.currLoc != null)
        {
            currAreaPanel.gameObject.SetActive(!Game.currInCity);
            if (!Game.currInCity)
            {
                currArea.text = (Game.currLoc.currZone.ToString() + "/" + Game.currLoc.maxZone);
                currLoc.text = Game.currLoc.name;
            }
            else
            {
                currLoc.text = Game.currLoc.townName;
            }

        }
        else
        {
            currLoc.text = "no map";
            currArea.text = "na";
        }
        if(enableTownInfoRow){
            textResourceFood.text = Game.town.Resources.Food.ToString();
            textResourceWood.text = Game.town.Resources.Wood.ToString();
            textResourceStone.text = Game.town.Resources.Stone.ToString();
            textResourceMetal.text = Game.town.Resources.Metal.ToString();
            textPopulation.text = Game.town.Population + "/" + Game.town.MaxPopulation;
        }

    }

    public void onClickMenuBtn(int id)
    {
        switch (id)
        {
            case 0:
                if (Game.currInCity)
                {
                    jumpToScene(SceneName.City);
                }
                else
                {
                    jumpToScene(SceneName.MainMenu);
                }
                break;
            case 1:
                jumpToScene(SceneName.Map);
                break;
            case 2:
                jumpToScene(SceneName.Status);
                break;
            case 3:
                jumpToScene(SceneName.Inventory);
                break;
            case 4:
                jumpToScene(SceneName.Quest);
                break;
            case 5:
                jumpToScene(SceneName.CraftMenu);
                break;
            case 6:
                jumpToScene(SceneName.Setting);
                break;
            case 7:
                jumpToScene(SceneName.Township);
                break;
            default:
                break;
        }
    }
}

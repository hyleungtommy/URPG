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
        playerName.text = Game.playerName;
        money.text = Game.money.ToString();
        if (Game.currLoc != null)
        {
            currAreaPanel.gameObject.SetActive(!Game.currInCity);
            if (!Game.currInCity)
            {
                currArea.text = (Game.currLoc.currArea.ToString() + "/" + Game.currLoc.maxArea);
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
            default:
                break;
        }
    }
}

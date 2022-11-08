using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;
public class MapScene : BasicScene
{
    public Text mapName;
    public Text mapDesc;
    public Image mapBG;
    public HeaderCtrl header;
    public Button prevPage;
    public Button nextPage;
    private Map[] maps;
    private int page;
    // Start is called before the first frame update
    void Start()
    {
        maps = DB.maps.Where(a => (a.unlocked == true)).ToArray();
        page = Game.currLoc.id;
        //Game.currLoc = maps[0];
        render();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void render()
    {
        header.render();
        mapName.text = maps[page].name;
        mapDesc.text = "Recommand Lv." + maps[page].reqLv + " ~ " + maps[page].maxLv + "\n" + maps[page].desc;
        mapBG.sprite = maps[page].bgImg;

        if (page == 0)
        {
            prevPage.gameObject.SetActive(false);
            nextPage.gameObject.SetActive(true);
        }
        else if (page == maps.Length - 1)
        {
            prevPage.gameObject.SetActive(true);
            nextPage.gameObject.SetActive(false);
        }
        else
        {
            prevPage.gameObject.SetActive(true);
            nextPage.gameObject.SetActive(true);
        }
    }

    public void onClickPrevPageButton()
    {
        page--;
        Game.currLoc = maps[page];
        render();
    }

    public void onClickNextPageButton()
    {
        page++;
        Game.currLoc = maps[page];
        render();
    }

    public void onClickReturn()
    {
        jumpToScene(SceneName.MainMenu);
    }
}

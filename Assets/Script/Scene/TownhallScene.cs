using System.Collections;
using System.Collections.Generic;
using RPG;
using UnityEngine;

public class TownhallScene : ListScene
{
    public HeaderCtrl header;
    // Start is called before the first frame update
    void Start()
    {
        OnClickTab(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickTab(int tabId){
        List<Building> buildList;
        switch(tabId){
            case 0:
                buildList = new List<Building>(Game.town.MainBuildingList);
            break;
            case 1:
                buildList = new List<Building>(Game.town.ResourceBuildingList);
            break;
            default:
                buildList = new List<Building>();
            break;
        }
        RenderContentView<BuildingUpgradeBox>(buildList.ConvertAll<Displayable>(d => d));

    }
}

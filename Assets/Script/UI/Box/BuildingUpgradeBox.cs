using System.Collections;
using System.Collections.Generic;
using RPG;
using UnityEngine;
using UnityEngine.UI;
public class BuildingUpgradeBox : MonoBehaviour, Renderable
{
    public Image imgBuilding;
    public Text textName;
    public Text textReqTime;
    public Text textPrice;
    public Text textRequireResourceWood;
    public Text textRequireResourceStone;
    public Text textRequirePopulation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Render(Displayable item)
    {
        Building building = item as Building;
        imgBuilding.sprite = building.img;
        textName.text = building.Name;
        //textReqTime.text = building.Requirement.RequireTime.ToString();
        textReqTime.text = Util.FormatTime(building.Requirement.RequireTime);
        textPrice.text = building.Requirement.RequireMoney.ToString();
        textRequireResourceWood.text = building.Requirement.RequireWood.ToString();
        textRequireResourceStone.text = building.Requirement.RequireStone.ToString();
        textRequirePopulation.text = building.Requirement.RequirePopulation.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;
public class BuildingUpgradeInfoBox : BasicInfoBox
{
    public Text textHeader;
    public Text textBasicInfo;
    public BasicBox box;
    public Text textDesc;
    public Text textUpgradeText;
    public Text textError;
    public Button btnUpgrade;
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

    protected override void showContent()
    {
        base.showContent();
        Building building = base.obj as Building;
        box.render(building);
        textHeader.text = building.Name;
        textBasicInfo.text = building.Name + " Lv." + building.Lv;
        textDesc.text = building.Desc;
        GetUpgradeText(building);
        textPrice.text = building.Requirement.RequireMoney.ToString();
        textRequireResourceWood.text = building.Requirement.RequireWood.ToString();
        textRequireResourceStone.text = building.Requirement.RequireStone.ToString();
        textRequirePopulation.text = building.Requirement.RequirePopulation.ToString();
        string errorMsg = building.CanUpgrade();
        if(errorMsg.Length > 0){
            btnUpgrade.gameObject.SetActive(false);
            textError.gameObject.SetActive(true);
            textError.text = errorMsg;
        }else{
            btnUpgrade.gameObject.SetActive(true);
            textError.gameObject.SetActive(false);
        }
    }

    void GetUpgradeText(Building building){
        if(building is ResourceBuilding){
            ResourceBuilding resourceBuilding = building as ResourceBuilding;
            textUpgradeText.text = 
            "Current:\n" + resourceBuilding.ResourceType + " +" + resourceBuilding.ResourceGenerateRate + "\n" +
            "Next:\n" + resourceBuilding.ResourceType + " +" + resourceBuilding.ResourceGenerateRateNextLv;
        }else if(building is Warehouse){
            Warehouse warehouse = building as Warehouse;
            textUpgradeText.text = 
            "Current:\nResource Cap:" + warehouse.MaxCapacity + " Item Storage:" + warehouse.ItemStorage.getSize() + "\n" +
            "Next:\nResource Cap:" + (warehouse.MaxCapacity + Constant.WarehouseResourceCapacityIncrement) +
            " Item Storage:" + (warehouse.ItemStorage.getSize() + Constant.WarehouseStorageSlotIncrement);
        }else{
            textUpgradeText.text = "";
        }
    }

    public void OnClickUpgrade(){
        
    }
}

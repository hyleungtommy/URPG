using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;
using System.Linq;
using System;
using System.Text;
using System.IO;
public class CheatScene : MonoBehaviour
{
    public Toggle noCraftRequirement;
    public Toggle unlockAllRecipe;
    public Toggle skillNoCooldown;
    public HeaderCtrl header;
    public Dropdown allItem;
    public Dropdown allEquip;
    public InputField storyPoint;
    List<Dropdown.OptionData>itemOptions;
    List<Dropdown.OptionData>equipOptions;
    // Start is called before the first frame update
    void Start()
    {
        allEquip.ClearOptions();
        allItem.ClearOptions();
        List<CustomOptionData>items = DB.items.Select(item => new CustomOptionData(item.id + ":" + item.name, item.id)).ToList();
        List<CustomOptionData>equips = DB.equipments.Select(equipment => new CustomOptionData(equipment.id + ":" + equipment.name, equipment.id)).ToList();
        itemOptions = items.Select(item => item as Dropdown.OptionData).ToList();
        equipOptions = equips.Select(item => item as Dropdown.OptionData).ToList();
        allItem.AddOptions(itemOptions);
        allEquip.AddOptions(equipOptions);
        storyPoint.text = Game.plotPt.ToString();
        Render();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickAddMoney(){
        Game.platinumCoin += 10000;
        Game.money += 1000000;
        header.render();
    }

    void Render(){
        noCraftRequirement.isOn = Param.noCraftRequirement;
        unlockAllRecipe.isOn = Param.unlockAllRecipe;
        skillNoCooldown.isOn = Param.skillNoCooldown;
    }

    public void OnValueChange(int i){
        switch (i){
            case 0:
                Param.noCraftRequirement = noCraftRequirement.isOn;
                SaveManager.saveValue(SaveKey.no_craft_requirement, Param.noCraftRequirement);
                break;
            case 1:
                Param.unlockAllRecipe = unlockAllRecipe.isOn;
                SaveManager.saveValue(SaveKey.unlock_all_recipe, Param.unlockAllRecipe);
                break;
            case 2:
                Param.skillNoCooldown = skillNoCooldown.isOn;
                SaveManager.saveValue(SaveKey.skill_no_cooldown, Param.skillNoCooldown);
                break;
        }
        SaveManager.save();
        Render();
    }

    public void OnClickAddItem(){
        int id = (itemOptions[allItem.value] as CustomOptionData).id;
        Item item = DB.QueryItem(id);
        Game.inventory.smartInsert(item, item.getMaxStack());
    }

    public void OnClickEquip(){
        int id = (equipOptions[allEquip.value] as CustomOptionData).id;
        GeneralEquipment equipment = DB.QueryEquipment(id);
        Game.inventory.smartInsert(equipment.toEquipment(0), 1);
    }

    public void OnClickChangeStoryPoint(){
        Game.plotPt = Int32.Parse(storyPoint.text);
    }

    public void OnClickExportCSV(){
        StringBuilder csvContent = new StringBuilder();
        csvContent.AppendLine("Id,Name");
        foreach(ItemTemplate item in DB.items){
            csvContent.AppendLine(item.id + "," + item.name);
        }
        File.WriteAllText("items.csv", csvContent.ToString());
        csvContent = new StringBuilder();
        csvContent.AppendLine("Id,Name");
        foreach(GeneralEquipment item in DB.equipments){
            csvContent.AppendLine(item.id + "," + item.name);
        }
        File.WriteAllText("equips.csv", csvContent.ToString());
    }

    public class CustomOptionData : Dropdown.OptionData{
        public int id;

        public CustomOptionData(string text, int id) : base(text)
        {
            this.id = id;
        }
    }
}

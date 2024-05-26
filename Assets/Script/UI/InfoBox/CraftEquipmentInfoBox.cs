using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;
public class CraftEquipmentInfoBox : BasicInfoBox
{
    public Text textHeader;
    public Text textBasicInfo;
    public BasicBox box;
    public EquipmentPowerText powerText;
    public Text textPrice;
    public RequirementTextGroupCtrl requirementTextGroupCtrl;
    public Button craftButton;
    CraftRecipe e;

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
        e = obj as CraftRecipe;
        GeneralEquipment equip = (e.resultItem as GeneralEquipment);
        box.render(equip);
        textHeader.text = equip.name;
        textBasicInfo.text = equip.type + "\n" + "Req Lv.:" + equip.reqLv;
        powerText.render(equip);
        textPrice.text = e.requireMoney.ToString();
        requirementTextGroupCtrl.render(e);
        craftButton.gameObject.SetActive(Param.noCraftRequirement || requirementTextGroupCtrl.allRequirementMatches);

    }
}

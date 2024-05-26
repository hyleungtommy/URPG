using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;
public class EnchantInfoBox : BasicInfoBox
{
    public Text textHeader;
    public Text textBasicInfo;
    public BasicBox box;
    public EquipmentPowerText powerText;
    public Text textPrice;
    public RequirementTextGroupCtrl requirementTextGroupCtrl;
    public Button craftButton;
    Equipment e;
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
        e = obj as Equipment;
        box.render(e);
        textHeader.text = e.name;
        textBasicInfo.text = e.getTypeName() + "\n" + "Req Lv.:" + e.reqLv;
        powerText.render(e);
        textPrice.text = e.enchantment.requireMoney.ToString();
        requirementTextGroupCtrl.render(e.enchantment.requirements);
        craftButton.gameObject.SetActive(Param.noCraftRequirement || requirementTextGroupCtrl.allRequirementMatches);
    }
}

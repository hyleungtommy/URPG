using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;
public class ReinforceInfoBox : BasicInfoBox
{
    public Text textHeader;
    public Text textBasicInfo;
    public BasicBox box;
    public ReinforceIncrementText powerText;
    public Text textPrice;
    public RequirementTextGroupCtrl requirementTextGroupCtrl;
    Equipment equip;

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
        equip = obj as Equipment;
        box.render(equip);
        textHeader.text = equip.name;
        textBasicInfo.text = equip.getTypeName() + "\n" + "Req Lv.:" + equip.reqLv;
        powerText.render(equip);
        textPrice.text = equip.reinforceRecipe.requireMoney.ToString();
        requirementTextGroupCtrl.render(equip.reinforceRecipe);

    }
}
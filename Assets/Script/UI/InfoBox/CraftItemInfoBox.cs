using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;
public class CraftItemInfoBox : BasicInfoBox
{
    public Text textHeader;
    public Text textBasicInfo;
    public BasicBox box;
    public Text textPrice;
    public Text textDesc;
    public RequirementTextGroupCtrl requirementTextGroupCtrl;
    public Text textBuyQty;
    public BrewingScene scene;
    CraftRecipe e;
    int qty;
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
        qty = 1;
        e = obj as CraftRecipe;
        Item item = (e.resultItem as Item );
        box.render(item);
        textHeader.text = item.name;
        textBasicInfo.text = item.getTypeName();
        textPrice.text = (item.price * qty).ToString();
        requirementTextGroupCtrl.render(e,qty);
        textBuyQty.text = qty.ToString();
        textDesc.text = item.desc;
    }

    public void onClickChangeQty(int value){
        qty += value;
        if (qty < 1) qty = 1;
        if (qty > 99) qty = 99;
        textBuyQty.text = qty.ToString();
        requirementTextGroupCtrl.render(e,qty);
        textPrice.text = (e.requireMoney * qty).ToString();
        scene.setCraftQty(qty);
    }
}

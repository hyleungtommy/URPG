using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using RPG;

public class ShopInfoBox : BasicInfoBox
{
    // Start is called before the first frame update
    public Text textHeader;
    public Text textBasicInfo;
    public Text textDesc1;
    public Text textBuyQty;
    public Text textSum;
    public Text textPrice;
    public Button btnBuy;
    public ItemBox box;
    public ShopScene scene { get; set; }

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
        Item item = base.obj as Item;
        box.render(item);
        textHeader.text = item.name;
        textBasicInfo.text = item.getTypeName();
        textDesc1.text = item.desc;
        textPrice.text = item.price.ToString();
        renderBuyQty();
    }

    public void renderBuyQty()
    {
        textSum.text = scene.calculateSum().ToString();
        textBuyQty.text = scene.buyQty.ToString();
        btnBuy.enabled = scene.canBuy();
    }

    public void onClickBtnAddQty(int buyQty)
    {
        scene.changeBuyQty(buyQty);
        renderBuyQty();
    }

    public void onClickBuy()
    {
        scene.onBuy();
        gameObject.SetActive(false);
    }

}
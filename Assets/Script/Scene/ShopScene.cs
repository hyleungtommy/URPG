using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;

public class ShopScene : BasicScene
{
    public HeaderCtrl header;
    public ShopInfoBox infoBox;
    public GameObject scrollViewContent;
    public GameObject boxPrefab;
    List<Item> shopList;
    public int buyQty { get; set; }
    public int selectedSlotId { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        header.render();
        shopList = new List<Item>();
        ItemTemplate[] itemList = DB.items;
        for (int i = 0; i < itemList.Length; i++)
        {
            if (itemList[i].buyPlace == "Shop")
            {
                shopList.Add(itemList[i].toItem());
            }
        }
        int noOfBox = shopList.Count;
        Transform contentTran = scrollViewContent.transform;
        GameObject box;
        for (int i = 0; i < noOfBox; i++)
        {
            int j = i;
            box = (GameObject)Instantiate(boxPrefab, contentTran);
            ShopBox boxCtrl = box.GetComponent<ShopBox>();
            boxCtrl.render(shopList[i]);
            box.GetComponent<Button>().onClick.AddListener(() => this.onClickItem(j));
        }
        buyQty = 1;
        infoBox.scene = this;
        infoBox.hide();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void onClickItem(int slotId)
    {
        this.selectedSlotId = slotId;
        infoBox.setContent(shopList[selectedSlotId]);
        infoBox.show();
    }

    public void changeBuyQty(int buyQty)
    {
        this.buyQty += buyQty;
        if (this.buyQty < 1) this.buyQty = 1;
        if (this.buyQty > 99) this.buyQty = 99;
    }

    public int calculateSum()
    {
        return shopList[selectedSlotId].price * buyQty;
    }

    public bool canBuy()
    {
        bool canBuy = true;
        if (calculateSum() > Game.money) canBuy = false;
        return canBuy;
    }

    public void onBuy()
    {
        Game.money -= calculateSum();
        Game.inventory.smartInsert(shopList[selectedSlotId], this.buyQty);
        Game.saveGame();
        header.render();
    }




}

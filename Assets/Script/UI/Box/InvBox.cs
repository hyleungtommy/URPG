using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using RPG;

public class InvBox : BasicBox
{
    // Start is called before the first frame update
    public Text qty;
    public Image rarityImg;
    public StorageSlot slot { get; set; }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setStorageSlot(StorageSlot slot)
    {
        this.slot = slot;
    }

    public void render()
    {
        //override basic box render
        base.render(slot.getContainment());
    }

    protected override void boxHaveItem(Displayable obj)
    {
        base.boxHaveItem(obj);
        Item item = obj as Item;
        rarityImg.gameObject.SetActive(item.rarity > 0);
        qty.gameObject.SetActive(true);
        rarityImg.color = Constant.itemRarityColor[item.rarity];
        qty.text = slot.getQty().ToString();
    }

    protected override void boxIsEmpty()
    {
        base.boxIsEmpty();
        rarityImg.gameObject.SetActive(false);
        qty.gameObject.SetActive(false);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using RPG;

public class ItemBox : BasicBox
{
    // Start is called before the first frame update
    public Image rarityImg;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }



    protected override void boxHaveItem(Displayable obj)
    {
        base.boxHaveItem(obj);
        Item item = obj as Item;
        rarityImg.gameObject.SetActive(item.rarity > 0);
        rarityImg.color = Constant.itemRarityColor[item.rarity];
    }

    protected override void boxIsEmpty()
    {
        base.boxIsEmpty();
        rarityImg.gameObject.SetActive(false);
    }

}
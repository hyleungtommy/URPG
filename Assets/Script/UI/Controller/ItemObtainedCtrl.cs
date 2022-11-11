using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;

public class ItemObtainedCtrl: MonoBehaviour{
    public Image img;
    public Text itemName;
    public Text itemQty;
    public void render(Item item,int qty){
        img.sprite = item.img;
        itemName.text = item.name;
        itemQty.text = "x " + qty.ToString();
    }
}
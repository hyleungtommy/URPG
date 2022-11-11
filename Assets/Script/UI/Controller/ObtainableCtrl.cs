using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;
public class ObtainableCtrl:MonoBehaviour{
    public Image img;
    public Text Name;
    public void render(Item item){
        img.sprite = item.img;
        Name.text = item.name;
    }
}
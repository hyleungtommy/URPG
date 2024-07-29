using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;
public class BlacksmithInfoBox : BasicInfoBox
{
    public Text textHeader;
    public Text textBasicInfo;
    public Text textDesc1;
    public BasicBox box;
    public EquipmentPowerText powerText;
    public BlacksmithScene scene { get; set; }
    public Text textPrice;
    GeneralEquipment e;
    Button btnBuy;
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
        e = obj as GeneralEquipment;
        box.render(e);
        textHeader.text = e.name;
        textBasicInfo.text = e.type + "\n" + "Req Lv.:" + e.reqLv;
        textDesc1.text = e.desc;
        powerText.render(e);
        textPrice.text = e.price.ToString();
    }

    public bool canBuy()
    {
        return (Game.money >= e.price);
    }

    public void onBuy()
    {
        Game.money -= e.price;
        Equipment eq = e.toEquipment(0);
        Game.inventory.smartInsert(eq, 1);
        Game.SaveGame();
        hide();
        scene.render();
    }

}

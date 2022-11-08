using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;

public class BlacksmithBox : BasicLargeBox
{
    public EquipmentPowerText powerText;
    public Text textEquipName;
    public Text textPrice;
    // Start is called before the first frame update
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
        GeneralEquipment e = obj as GeneralEquipment;
        powerText.render(e);
        textEquipName.text = e.name;
        textPrice.text = e.price.ToString();
    }



}

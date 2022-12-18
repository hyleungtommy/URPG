using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;

public class EquipmentBox : BasicLargeBox
{
    public EquipmentPowerText powerText;
    public Text textEquipName;
    public StorageSlot slot { get; set; }
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
        //Debug.Log(obj);
        Equipment e = obj as Equipment;
        //Debug.Log(e);
        powerText.render(e);
        textEquipName.text = e.fullName;
    }

    public void render()
    {
        base.render(slot.getContainment());
    }


}

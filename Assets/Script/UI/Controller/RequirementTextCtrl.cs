using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;

public class RequirementTextCtrl : MonoBehaviour
{
    public Image imgReqItem;
    public Text textReqQty;
    public bool requirementFulfilled = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Render(Requirement requirement,int multiplier){
        imgReqItem.sprite = requirement.requireItem.img;
        if(requirement.type == Requirement.Type.Enemy){

            //textReqQty.text = (requirement.requireItem ).name + ": " + requirement.currentQty + "/" + requirement.requireQty;
        }else if(requirement.type == Requirement.Type.Item){
            int qty = 0;
            if(requirement.requireItem is GeneralEquipment){
                GeneralEquipment equip = requirement.requireItem as GeneralEquipment;
                qty = Game.inventory.searchTotalQtyOfEquipmentInInventory(equip.id);
                textReqQty.text = equip.name + ": " + qty + "/" + requirement.requireQty * multiplier;
            }else{
                Item item = requirement.requireItem as Item;
                qty = Game.inventory.searchTotalQtyOfItemInInventory(item.id);
                textReqQty.text = item.name + ": " + qty + "/" + requirement.requireQty * multiplier;
            }
            requirementFulfilled = qty >= (requirement.requireQty * multiplier);
            if(requirementFulfilled){
                textReqQty.color = Param.requirementText[0];
            }else{
                textReqQty.color = Param.requirementText[1];
            }
        }
    }
}

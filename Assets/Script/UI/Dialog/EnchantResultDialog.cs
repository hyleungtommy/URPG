using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;
public class EnchantResultDialog : MonoBehaviour
{
    public ItemBox itemBox;
    public Text textItemName;
    private TaskCompleteMsg taskCompleteMsg;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setTaskCompleteMsg(TaskCompleteMsg taskCompleteMsg){
        this.taskCompleteMsg = taskCompleteMsg;
    }
    public void show(){
        gameObject.SetActive(true);
        Equipment craftItem = taskCompleteMsg.obtainItem[0] as Equipment;
        itemBox.render(craftItem);
        //testing, only diplay the first effect
        EnchantmentEffect enchantmentEffect = craftItem.enchantment.effects[0];
        textItemName.text = craftItem.name + "\n" + enchantmentEffect.name + " Lv." + enchantmentEffect.lv + "\n" + enchantmentEffect.desc;
    }   
    public void close(){
        gameObject.SetActive(false);
    }
}

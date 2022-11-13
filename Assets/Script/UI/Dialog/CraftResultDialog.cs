using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;
public class CraftResultDialog : MonoBehaviour
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
        Item craftItem = taskCompleteMsg.obtainItem[0];
        int qty = taskCompleteMsg.qty[0];
        itemBox.render(craftItem);
        textItemName.text = craftItem.name + " x " + qty;
    }   
    public void close(){
        gameObject.SetActive(false);
    }
}

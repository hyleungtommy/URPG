using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;

public class ExploreResultDialog: MonoBehaviour{
    public ItemObtainedGroupCtrl itemObtainedGroupCtrl;
    private TaskCompleteMsg taskCompleteMsg;
    public void setTaskCompleteMsg(TaskCompleteMsg taskCompleteMsg){
        this.taskCompleteMsg = taskCompleteMsg;
    }
    public void show(){
        gameObject.SetActive(true);
        itemObtainedGroupCtrl.render(taskCompleteMsg);
    }   
    public void close(){
        gameObject.SetActive(false);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;

public class ItemObtainedGroupCtrl: MonoBehaviour{
    public ItemObtainedCtrl[] itemObtainedList;
    public void render(TaskCompleteMsg taskCompleteMsg){
        for(int i = 0 ; i < itemObtainedList.Length ; i++){
            if(i < taskCompleteMsg.obtainItem.Count){
                itemObtainedList[i].gameObject.SetActive(true);
                itemObtainedList[i].render(taskCompleteMsg.obtainItem[i],taskCompleteMsg.qty[i]);
            }else{
                itemObtainedList[i].gameObject.SetActive(false);
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;

public class TimedTaskCtrl : MonoBehaviour
{
    public Text textCurrentTask;
    public BarCtrl taskBar;
    public Button btnTaskComplete;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void render(ExploreTask craftTask){
        if(craftTask == null){
            textCurrentTask.text = "Idling";
            taskBar.gameObject.SetActive(false);
            if(btnTaskComplete != null){
                btnTaskComplete.gameObject.SetActive(false);
            }
        }else{
            taskBar.gameObject.SetActive(true);
            textCurrentTask.text = "Crafting something";
            taskBar.noAnimationRenderTime(craftTask.taskTime,craftTask.getRemainingTimeSecond(),craftTask.getRemainingTimeFormatted());
            if(btnTaskComplete != null){
                //Debug.Log("craftTask.getRemainingTimeSecond()=" + craftTask.getRemainingTimeSecond());
                if(craftTask.getRemainingTimeSecond() <= 0)
                    btnTaskComplete.gameObject.SetActive(true);
                else
                    btnTaskComplete.gameObject.SetActive(false);
            }
        }
    }
}

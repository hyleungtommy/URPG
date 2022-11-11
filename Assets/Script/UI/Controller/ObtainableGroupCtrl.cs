using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;
public class ObtainableGroupCtrl:MonoBehaviour{

    public ObtainableCtrl[] obtainableCtrls;
    public void render(Item[] obtainables){
        int i = 0;
        foreach(ObtainableCtrl obtainableCtrl in obtainableCtrls){
            if(i < obtainables.Length){
                obtainableCtrl.gameObject.SetActive(true);
                obtainableCtrl.render(obtainables[i]);
            }else{
                obtainableCtrl.gameObject.SetActive(false);
            }
            i++;
        }
    }

}
using System.Collections;
using System.Collections.Generic;
using RPG;
using UnityEngine;

public class GlobalBuffListCtrl : MonoBehaviour
{
    public GlobalBuffCtrl prefab;
    // Start is called before the first frame update
    void Start()
    {
        Transform contentTran = gameObject.transform;

        foreach (Transform child in contentTran)
        {
            Destroy(child.gameObject);
        }
        for(int i = 0 ; i < Game.globalBuffManager.GetCount() ; i++){
            GlobalBuffCtrl box = Instantiate(prefab, contentTran);
            box.Render(Game.globalBuffManager.GetBuff(i));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}

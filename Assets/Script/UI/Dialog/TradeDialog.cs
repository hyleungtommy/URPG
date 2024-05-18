using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;
using System.Linq;
using System;
public class TradeDialog : MonoBehaviour
{
    public GameObject receiveItem;
    public GameObject requirePlatinum;
    public ItemBox receiveItemBox;
    public Text receiveItemName;
    public Text receiveItemAmount;
    public Text requirePlatinumCoin;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Render(TradeList tradeList)
    {
        if(tradeList.require.itemId.Equals("platinumCoin")){
            requirePlatinum.SetActive(true);
            requirePlatinumCoin.text = "X " + tradeList.require.amount;
        }else{
            //todo
        }
        if(tradeList.receive.itemId.Equals("platinumCoin")){
            //todo
        }else{
            Item received = DB.items.First(item => item.id == Int32.Parse(tradeList.receive.itemId)).toItem();
            receiveItem.gameObject.SetActive(true);
            receiveItemName.text = received.name;
            receiveItemBox.render(received);
            receiveItemAmount.text = "X " + tradeList.receive.amount;
        }   
    }
}

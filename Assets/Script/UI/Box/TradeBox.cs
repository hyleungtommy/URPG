using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using RPG;
using System.Linq;
using System;

public class TradeBox : MonoBehaviour
{
    
    public GameObject receiveItem;
    public GameObject requirePlatinumCoin;
    public Text requirePlatinumCoinAmount;
    public ItemBox receiveItemBox;
    public Text receiveItemAmount;
    
    public Image arrow;

    public void Render(TradeList tradeList){
        if(tradeList.require.itemId.Equals("platinumCoin")){
            requirePlatinumCoin.gameObject.SetActive(true);
            requirePlatinumCoinAmount.text = "X " + tradeList.require.amount;
        }else{
            //todo
        }
        if(tradeList.receive.itemId.Equals("platinumCoin")){
            //todo
        }else{
            receiveItem.gameObject.SetActive(true);
            receiveItemBox.render(DB.QueryItem(tradeList.receive.itemId));
            receiveItemAmount.text = "X " + tradeList.receive.amount;
        }
    }

}
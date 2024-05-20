using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace RPG
{
    [Serializable]
    public class TradeListingTemplate
    {
        public string type;
        public TradeItem require;
        public TradeItem receive;

        public override string ToString()
        {
            return "";
        }
        
        [Serializable]
        public class TradeItem{
            public string itemId;
            public int min;
            public int max;
        }

        public TradeList ToTradeList(){
            int requireItemAmount = UnityEngine.Random.Range(require.min, require.max);
            int receiveItemAmount = UnityEngine.Random.Range(receive.min, receive.max);
            TradeList tradeList = new TradeList(type, require.itemId, receive.itemId, requireItemAmount, receiveItemAmount);
            return tradeList;
        }
    }

    
}
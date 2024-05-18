using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace RPG
{
    public class TradeList{
        public string type {get; set;}
        public TradeItem require {get; set;}
        public TradeItem receive {get; set;}

        public TradeList(string type, string requireItemId, string receiveItemId, int requireItemAmount, int receiveItemAmount){
            this.type = type;
            this.receive = new TradeItem();
            this.require = new TradeItem();
            this.require.itemId = requireItemId;
            this.receive.itemId = receiveItemId;
            this.require.amount = requireItemAmount;
            this.receive.amount = receiveItemAmount;
        }
        
        public class TradeItem{
            public string itemId {get; set;}
            public int amount {get; set;}
        }

    }

    
}
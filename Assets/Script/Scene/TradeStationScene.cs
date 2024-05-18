using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RPG;
using UnityEngine;
using UnityEngine.UI;
public class TradeStationScene : BasicScene
{
    public HeaderCtrl header;
    public GameObject scrollViewContent;
    public GameObject boxPrefab;
    public TradeDialog tradeDialog;
    public Text textPlatinumCoin;
    int selectedSlotId;
    // Start is called before the first frame update
    void Start()
    {
        header.render();
        if(Game.shouldRefreshTradeList){
            GenerateTradeListing();
        }
        int noOfBox = Game.currentTradeList.Count;
        Transform contentTran = scrollViewContent.transform;
        GameObject box;
        for (int i = 0; i < noOfBox; i++)
        {
            int j = i;
            box = (GameObject)Instantiate(boxPrefab, contentTran);
            TradeBox boxCtrl = box.GetComponent<TradeBox>();
            boxCtrl.Render(Game.currentTradeList[i]);
            box.GetComponent<Button>().onClick.AddListener(() => this.onClickItem(j));
        }
        tradeDialog.gameObject.SetActive(false);
        Render();
    }

    void Render(){
        textPlatinumCoin.text = Game.platinumCoin.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateTradeListing(){
        TradeListingTemplate[] copyOfTradeListingTemplate = (TradeListingTemplate[])DB.tradeListingTemplate.Clone();
        List<TradeListingTemplate>listingTemplates = copyOfTradeListingTemplate.ToList();
        Util.Shuffle<TradeListingTemplate>(listingTemplates);
        List<TradeListingTemplate> tradeListings = listingTemplates.GetRange(0,7);
        Game.currentTradeList.Clear();
        for(int i = 0 ; i < tradeListings.Count; i++){
            Game.currentTradeList.Add(tradeListings[i].ToTradeList());
        }
    }

    public void onClickItem(int slotId)
    {
        selectedSlotId = slotId;
        tradeDialog.gameObject.SetActive(true);
        tradeDialog.Render(Game.currentTradeList[slotId]);
    }

    public void OnClickTradeConfirm(){
        tradeDialog.gameObject.SetActive(false);
        Trade();
    }

    public void OnClickTradeCancel(){
        tradeDialog.gameObject.SetActive(false);
    }

    void Trade(){
       TradeList tradeList = Game.currentTradeList[selectedSlotId];
        if(tradeList.require.itemId.Equals("platinumCoin")){
            Game.platinumCoin -= tradeList.require.amount;
        }else{
            //todo
        }
        if(tradeList.receive.itemId.Equals("platinumCoin")){
            //todo
        }else{
            Item received = DB.items.First(item => item.id == Int32.Parse(tradeList.receive.itemId)).toItem();
            Game.inventory.smartInsert(received, tradeList.receive.amount);   
        }
        Render();
    }

}

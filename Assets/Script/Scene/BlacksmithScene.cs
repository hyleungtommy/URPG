using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;
public class BlacksmithScene : BasicScene
{
    // Start is called before the first frame update
    public HeaderCtrl header;
    public GameObject scrollViewContent;
    public GameObject boxPrefab;
    public BlacksmithInfoBox infoBox;
    List<GeneralEquipment> shopList;
    void Start()
    {
        header.render();
        infoBox.scene = this;
        infoBox.hide();
        shopList = new List<GeneralEquipment>();
        GeneralEquipment[] elist = DB.equipments;
        for (int i = 0; i < elist.Length; i++)
        {
            if (elist[i].buyPlace == "Blacksmith")
            {
                shopList.Add(elist[i]);
            }
        }
        int noOfBox = shopList.Count;
        Transform contentTran = scrollViewContent.transform;

        foreach (Transform child in contentTran)
        {
            Destroy(child.gameObject);
        }
        GameObject box;
        for (int i = 0; i < noOfBox; i++)
        {
            int j = i;
            box = (GameObject)Instantiate(boxPrefab, contentTran);
            BlacksmithBox boxCtrl = box.GetComponent<BlacksmithBox>();
            boxCtrl.render(shopList[i]);
            box.GetComponent<Button>().onClick.AddListener(() => this.onClickItem(j));
        }
    }

    public void render()
    {
        header.render();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void onClickItem(int slotId)
    {
        //this.selectedEquipmentId = availableEquipments[slotId].getContainment().id;
        infoBox.setContent(shopList[slotId]);
        infoBox.show();
    }



}

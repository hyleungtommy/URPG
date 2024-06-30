using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG;
using UnityEngine.UI;
public class QuestCenterScene : MonoBehaviour
{
    public HeaderCtrl header;
    public GameObject contentView;
    public GameObject questEntryPrefab;
    List<QuestEntryButtonCtrl> boxList;
    // Start is called before the first frame update
    void Start()
    {
        renderContentView();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void renderContentView(){
        List<DailyQuest> dailyQuests = Game.questManager.getAvailableQuests();
        header.render();
        int noOfBox = dailyQuests.Count;
        Transform contentTran = contentView.transform;
        boxList = new List<QuestEntryButtonCtrl>();
        foreach (Transform child in contentTran)
        {
            Destroy(child.gameObject);
        }
        GameObject box;
        for (int i = 0; i < noOfBox; i++)
        {
            int j = i;

            box = (GameObject)Instantiate(questEntryPrefab, contentTran);
            QuestEntryButtonCtrl boxCtrl = box.GetComponent<QuestEntryButtonCtrl>();
            boxCtrl.render(dailyQuests[i]);
            box.GetComponent<Button>().onClick.AddListener(() => this.onClickItem(j));
            boxList.Add(boxCtrl);
        }
    }

    public void onClickItem(int id){

    }
}

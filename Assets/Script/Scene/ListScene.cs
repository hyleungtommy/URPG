using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;
public abstract class ListScene : BasicScene
{
    public BasicInfoBox infoBox;
    public GameObject contentView;
    public GameObject boxPrefab;
    protected List<Displayable> itemList { get; set; }
    protected int selectedId {get; set;}

    protected void RenderContentView<T>(List<Displayable> itemList) where T : Renderable
    {
        this.itemList = itemList;
        int noOfBox = itemList.Count;
        Transform contentTran = contentView.transform;
        List<T> boxList = new List<T>();
        foreach (Transform child in contentTran)
        {
            Destroy(child.gameObject);
        }
        GameObject box;
        for (int i = 0; i < noOfBox; i++)
        {
            int j = i;

            box = (GameObject)Instantiate(boxPrefab, contentTran);
            T boxCtrl = box.GetComponent<T>();
            boxCtrl.Render(itemList[i]);
            box.GetComponent<Button>().onClick.AddListener(() => this.OnClickItem(j));
            boxList.Add(boxCtrl);
        }
        infoBox.hide();
    }

    public virtual void OnClickItem(int id)
    {
        selectedId = id;
        Displayable item = itemList[id];
        OnClickInfoBox(item);
    }
    public virtual void OnClickInfoBox(Displayable item)
    {
        infoBox.setContent(item);
        infoBox.show();

    }
}
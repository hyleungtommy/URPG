using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;
public class ItemPanelElement : MonoBehaviour
{
    // Start is called before the first frame update
    public ItemBox box;
    public Text textItemName;
    public Text textQty;
    public int itemId { get; set; }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void render(Item item, int qty)
    {
        textItemName.gameObject.SetActive(true);
        textQty.gameObject.SetActive(true);
        box.render(item);
        textItemName.text = item.name;
        textQty.text = qty.ToString();
    }

    public void renderEmpty()
    {
        box.render(null);
        textItemName.gameObject.SetActive(false);
        textQty.gameObject.SetActive(false);
    }

}

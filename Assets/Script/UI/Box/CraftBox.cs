using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using RPG;

public class CraftBox : BasicLargeBox
{
    // Start is called before the first frame update
    public Text textItemName;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override void boxHaveItem(Displayable obj)
    {
        base.boxHaveItem(obj);
        Item item = obj as Item;
        textItemName.text = item.name;
    }

    protected override void boxIsEmpty()
    {
        base.boxIsEmpty();
    }

}
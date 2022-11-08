using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;

public class SupportCharacterBox : BasicBox
{
    public Image working;
    // Start is called before the first frame update
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
        working.gameObject.SetActive(false);
    }

    protected override void boxIsEmpty()
    {
        base.boxIsEmpty();
        working.gameObject.SetActive(false);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using RPG;

public class BasicLargeBox : MonoBehaviour
{
    // Start is called before the first frame update
    public BasicBox innerBox;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void addClickEvent(UnityAction call)
    {
        GetComponent<Button>().onClick.AddListener(call);
    }

    public void render(Displayable obj)
    {
        if (obj != null)
        {
            boxHaveItem(obj);
        }
        else
        {
            boxIsEmpty();
        }
    }

    protected virtual void boxHaveItem(Displayable obj)
    {
        innerBox.render(obj);
    }

    protected virtual void boxIsEmpty()
    {
        innerBox.render(null);
    }


}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using RPG;

public class BasicInfoBox : MonoBehaviour
{
    // Start is called before the first frame update
    protected Displayable obj;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void onClickX()
    {
        gameObject.SetActive(false);
    }

    public void setContent(Displayable obj)
    {
        this.obj = obj;
    }

    public void show()
    {
        gameObject.SetActive(true);
        showContent();
    }

    public void hide()
    {
        gameObject.SetActive(false);
    }

    protected virtual void showContent()
    {

    }

    protected string getRedString(string str)
    {
        return "<color=#ff0000ff>" + str + "</color>";
    }



}
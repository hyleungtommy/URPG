using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;
public class BuffCtrl : MonoBehaviour
{
    Transform contentTran;
    public GameObject buffPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void render(BuffState buff)
    {
        contentTran = gameObject.transform;
        foreach (Transform child in contentTran)
        {
            Destroy(child.gameObject);
        }
        int noOfBox = 8;
        GameObject box;
        for (int i = 0; i < noOfBox; i++)
        {
            if (buff.getBuff(i) != null)
            {
                box = (GameObject)Instantiate(buffPrefab, contentTran);
                //Debug.Log(buff.getBuff(i).type);
                box.GetComponent<Image>().sprite = buff.getBuff(i).Img;
            }
        }
    }
}

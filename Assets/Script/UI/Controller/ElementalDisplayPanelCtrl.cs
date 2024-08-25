using System.Collections;
using System.Collections.Generic;
using RPG;
using UnityEngine;

public class ElementalDisplayPanelCtrl : MonoBehaviour
{
    List<ElementalDisplayCtrl>elementalDisplays;
    public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        elementalDisplays = new List<ElementalDisplayCtrl>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Render(ElementalTemplate elementalTemplate){
        Transform contentTran = gameObject.transform;
        foreach (Transform child in contentTran)
        {
            Destroy(child.gameObject);
        }
        int[]elementValues = Util.FlattenElementalMatrix(elementalTemplate);
        int i = 0;
        foreach(int elementalValue in elementValues){
            if(elementalValue > 0){
                GameObject box = Instantiate(prefab, contentTran);
                ElementalDisplayCtrl elementalDisplay = box.GetComponent<ElementalDisplayCtrl>();
                elementalDisplay.Render(i,elementalValue);
            }
            i++;
        }
    }
}

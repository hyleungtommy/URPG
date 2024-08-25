using System.Collections;
using System.Collections.Generic;
using RPG;
using UnityEngine;
using UnityEngine.UI;

public class ElementalDisplayCtrl : MonoBehaviour
{
    public Image ImageElementalIcon;
    public Text TextElementPercentage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Render(int elementalType, int elementalValue){
        ImageElementalIcon.sprite = SpriteManager.elementIcons[elementalType];
        TextElementPercentage.text = elementalValue + "%";
    }
}

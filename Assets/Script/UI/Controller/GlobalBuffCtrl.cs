using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG;
using UnityEngine.UI;
public class GlobalBuffCtrl : MonoBehaviour
{
    public Text textBuffRemainingRound;
    public Image textBuffImage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Render(GlobalBuff globalBuff){
        textBuffRemainingRound.text = globalBuff.rounds + " Rounds left";
        textBuffImage.sprite = globalBuff.img;
    }
}

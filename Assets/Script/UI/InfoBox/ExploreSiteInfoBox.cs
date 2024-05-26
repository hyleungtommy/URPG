using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;

public class ExploreSiteInfoBox : BasicInfoBox
{
    public Text textHeader;
    public Text textBasicInfo;
    public Text textDesc1;
    public Text textPrice;
    public Image imgExploreSite;
    public ObtainableGroupCtrl obtainableGroupCtrl;
    public Button buttonGoExplore;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void showContent()
    {
        base.showContent();
        ExploreSite exploreSite = base.obj as ExploreSite;
        textHeader.text = exploreSite.name;
        textBasicInfo.text = exploreSite.type.ToString();
        textDesc1.text = exploreSite.desc;
        textPrice.text = exploreSite.requireMoney.ToString();
        imgExploreSite.sprite = exploreSite.img;
        obtainableGroupCtrl.render(exploreSite.obtainableItems);
        buttonGoExplore.gameObject.SetActive(Game.money >= exploreSite.requireMoney && Game.craftSkillManager.availableExploreTeam > 0);
    }
}

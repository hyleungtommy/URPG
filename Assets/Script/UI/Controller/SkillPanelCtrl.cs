using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;
public class SkillPanelCtrl : MonoBehaviour
{
    public SkillPanelElement[] elements;
    public Button btnNextPage;
    public Button btnPrevPage;
    public Text textPage;
    int currPage;
    int maxPage;
    List<Skill> skillList;
    EntityPlayer player;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setSkillList(List<Skill> skillList, EntityPlayer player)
    {
        //Debug.Log("skill list:" + skillList.Count + ", ele len=" + elements.Length + " /" + (float)skillList.Count / (float)elements.Length);
        this.skillList = skillList;
        this.player = player;
        currPage = 0;
        maxPage = (int)Mathf.Floor((float)skillList.Count / (float)elements.Length);
    }

    public void render()
    {

        for (int i = 0; i < elements.Length; i++)
        {
            int pos = currPage * elements.Length + i;
            if (pos < skillList.Count)
            {
                elements[i].skillId = skillList[pos].id;
                elements[i].render(skillList[pos], player);
            }
            else
            {
                elements[i].skillId = -1;
                elements[i].renderEmpty();
            }
        }

        if (currPage == 0 && currPage == maxPage)
        {
            btnPrevPage.gameObject.SetActive(false);
            btnNextPage.gameObject.SetActive(false);
        }
        else if (currPage == 0)
        {
            btnPrevPage.gameObject.SetActive(false);
            btnNextPage.gameObject.SetActive(true);
        }
        else if (currPage == maxPage)
        {
            btnPrevPage.gameObject.SetActive(true);
            btnNextPage.gameObject.SetActive(false);
        }
        else
        {
            btnPrevPage.gameObject.SetActive(true);
            btnNextPage.gameObject.SetActive(true);
        }
        textPage.text = (currPage + 1) + "/" + (maxPage + 1);
    }

    public void onClickNextPage()
    {
        currPage++;
        render();
    }

    public void onClickPrevPage()
    {
        currPage--;
        render();
    }
}

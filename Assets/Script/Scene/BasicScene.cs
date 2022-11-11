using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;
using UnityEngine.SceneManagement;

public class BasicScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void jumpToScene(string sceneName)
    {
        PlotData pd = null;
        if (this is BattleScene)
        {

        }
        else
        {
            Debug.Log("match plot, pt = " + Game.plotPt);
            pd = PlotMatcher.matchPlot(sceneName);
        }


        if (pd != null)
        {
            PlotMatcher.nextScene = sceneName;
            PlotMatcher.nextPlot = pd;
            SceneManager.LoadScene(SceneName.Dialog);
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    public virtual void onSelectCharacter(int id, BattleCharacter character)
    {

    }

    /*
    public virtual void onSelectSupportCharacter(int id, SupportCharacter character)
    {

    }
    */
}

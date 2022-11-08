using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;
using UnityEngine.SceneManagement;
public class DialogScene : MonoBehaviour
{
    public Text textDialog;
    public Text textSpeakerName;
    public Image speakerImg;
    public Image dialogArrow;
    public Image bg;
    DialogCtrl dialogCtrl;
    string currentText;
    bool typingText;
    PlotData pd;
    // Start is called before the first frame update
    void Start()
    {
        //PlotData pd = DB.plots[0];
        pd = PlotMatcher.nextPlot;
        dialogCtrl = pd.dialog;
        dialogArrow.gameObject.SetActive(false);
        bg.sprite = pd.bg;
        render();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (typingText)
            {
                StopAllCoroutines();
                textDialog.text = currentText;
                typingText = false;
                dialogArrow.gameObject.SetActive(true);
            }
            else
                render();
        }
    }

    void render()
    {
        if (dialogCtrl.hasNext())
        {
            DialogText text = dialogCtrl.getNextDialog();
            Debug.Log(text.dialog);
            currentText = text.dialog;
            textSpeakerName.text = text.speakerName;
            speakerImg.sprite = text.speakerImg;
            StartCoroutine(typeText(currentText));
        }
        else
        {
            PlotData nextPD = PlotMatcher.handlePlotEnd(pd);
            if (nextPD != null)
            {
                PlotMatcher.nextPlot = nextPD;
                SceneManager.LoadScene(SceneName.Dialog);
            }
            else
            {
                PlotMatcher.nextPlot = null;
                SceneManager.LoadScene(PlotMatcher.nextScene);
            }
        }
    }

    IEnumerator typeText(string text)
    {
        typingText = true;
        dialogArrow.gameObject.SetActive(false);
        int wordCount = text.Length;
        for (int i = 0; i <= wordCount; i++)
        {
            string word = text.Substring(0, i);
            textDialog.text = word;
            yield return new WaitForSeconds(0.04f);
        }
        typingText = false;
        dialogArrow.gameObject.SetActive(true);
    }

    public void onClickSkip()
    {
        PlotData nextPD = PlotMatcher.handlePlotEnd(pd);
        if (nextPD != null)
        {
            PlotMatcher.nextPlot = nextPD;
            SceneManager.LoadScene(SceneName.Dialog);
        }
        else
        {
            PlotMatcher.nextPlot = null;
            SceneManager.LoadScene(PlotMatcher.nextScene);
        }
    }
}

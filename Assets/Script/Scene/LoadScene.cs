using System.Collections;
using System.Collections.Generic;
using RPG;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LoadScene : MonoBehaviour
{
    AsyncOperation loadingOperation;
    public Slider progressBar;
    public Text percentLoaded;
    // Start is called before the first frame update
    void Start()
    {
        loadingOperation = SceneManager.LoadSceneAsync(SceneName.MainMenu);
    }

    // Update is called once per frame
    void Update()
    {
        float progressValue = Mathf.Clamp01(loadingOperation.progress / 0.9f);
        percentLoaded.text = Mathf.Round(progressValue * 100) + "%";
        progressBar.value = progressValue;
    }
}

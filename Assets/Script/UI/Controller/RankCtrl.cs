using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;


public class RankCtrl : MonoBehaviour
{
    public Image[] stars;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void render(int rank)
    {
        for (int i = 0; i < stars.Length; i++)
        {
            if (i < rank) stars[i].gameObject.SetActive(true);
            else stars[i].gameObject.SetActive(false);
        }
    }
}

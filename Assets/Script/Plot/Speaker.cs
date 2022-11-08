using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace RPG
{
    public class Speaker
    {
        public string name { get; set; }
        public Sprite[] img { get; set; }
        public Speaker(string name, string[] imgPath)
        {
            this.name = name;
            List<Sprite> list = new List<Sprite>();
            for (int i = 0; i < imgPath.Length; i++)
            {
                if (imgPath[i] != "")
                {
                    Sprite[] oneSheet = Resources.LoadAll<Sprite>("Characters/Dialog Face/" + imgPath[i]);
                    //Debug.Log(imgPath[i]);
                    //Debug.Log(oneSheet.Length);
                    for (int j = 0; j < 8; j++)
                    {
                        list.Add(oneSheet[j]);
                    }
                }

            }
            img = list.ToArray();
        }

    }
}
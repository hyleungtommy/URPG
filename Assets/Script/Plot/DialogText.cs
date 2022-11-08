using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace RPG
{
    public class DialogText
    {
        public string speakerName { get; set; }
        public Sprite speakerImg { get; set; }
        public string dialog { get; set; }
        Speaker speaker;
        public DialogText(string dialogs)
        {

            string[] ds = dialogs.Split(':');
            if (ds.Length == 3)
            {
                speaker = SpeakerList.findSpeaker(ds[0]);
                if (speaker != null)
                {
                    speakerName = speaker.name;
                    dialog = ds[2];
                    if (speaker.img.Length >= Int32.Parse(ds[1]))
                    {
                        speakerImg = speaker.img[Int32.Parse(ds[1]) - 1];
                        //Debug.Log(Int32.Parse(ds[1]) + "," + speakerImg);
                    }
                    else
                    {
                        Debug.Log("speaker img not found :" + dialogs);
                    }
                }
                else
                {
                    Debug.Log("speaker not found :" + dialogs);
                }
            }
            else
            {
                Debug.Log("dialog do not have 3 elements :" + dialogs);
            }
        }

    }
}
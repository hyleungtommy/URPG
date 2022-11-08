using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace RPG
{
    public class DialogCtrl
    {
        List<DialogText> dialogs;
        int counter;
        public DialogCtrl(string[] dialogStr)
        {
            dialogs = new List<DialogText>();
            foreach (string s in dialogStr)
            {
                dialogs.Add(new DialogText(s));
            }
            counter = 0;
        }

        public bool hasNext()
        {
            return counter < dialogs.Count;
        }

        public DialogText getNextDialog()
        {
            DialogText d = dialogs[counter];
            counter++;
            return d;
        }

    }
}
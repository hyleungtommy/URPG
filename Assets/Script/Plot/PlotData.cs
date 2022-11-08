using System;
using UnityEngine;
namespace RPG
{
    public class PlotData
    {
        public int triggerPt { get; set; }
        public string scene { get; set; }
        public Sprite bg { get; set; }
        public DialogCtrl dialog { get; set; }
        public int triggerArea { get; set; }
        public bool triggerBeforeBattle { get; set; }
        public int plotPtIncrement { get; set; }
        public bool startNextPlotAtEnd { get; set; }
        public int triggerMap { get; set; }

        public PlotData(string[] dialog)
        {
            this.dialog = new DialogCtrl(dialog);
        }

    }
}
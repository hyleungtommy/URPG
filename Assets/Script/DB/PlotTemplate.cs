using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace RPG
{
    [Serializable]
    public class PlotTemplate
    {
        public int triggerPt;
        public string scene;
        public string bg;
        public string[] dialog;
        public int plotPtIncrement;
        public int triggerArea;
        public bool triggerBeforeBattle;
        public bool startNextPlotAtEnd;
        public int triggerMap;

        public PlotData toPlotData()
        {
            PlotData data = new PlotData(dialog);
            data.bg = Resources.Load<Sprite>("Background/" + bg);
            data.plotPtIncrement = plotPtIncrement;
            data.scene = scene;
            data.triggerArea = triggerArea;
            data.triggerBeforeBattle = triggerBeforeBattle;
            data.triggerPt = triggerPt;
            data.startNextPlotAtEnd = startNextPlotAtEnd;
            data.triggerMap = triggerMap;
            return data;
        }




    }
}
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using RPG;
using System.Linq;

namespace Editor
{
    public class PlotMatcherTest
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            TextAsset plotJSON = Resources.Load<TextAsset>("Data/Mock/mockPlot");
            PlotTemplate[] plotTemplates = JsonHelper.FromJson<PlotTemplate>(plotJSON.text);
            PlotData[] mockpdList = plotTemplates.Select((template) => template.toPlotData()).ToArray();
            DB.plots = mockpdList;
        }
        // A Test behaves as an ordinary method
        [Test]
        public void ShouldTriggerPlotWhenGoToMainMenu()
        {
            Game.plotPt = 0;
            Game.currLoc = new Map(0,"name","",null,null,10,10,10,null,null,null,null,0);
            PlotData pd = PlotMatcher.matchPlot("MainMenu");
            Assert.IsNotNull(pd);
            Assert.AreEqual(0,pd.triggerPt);
        }

        [Test]
        public void ShouldNotTriggerPlotWhenGoToOtherScreen()
        {
            Game.plotPt = 0;
            Game.currLoc = new Map(0,"name","",null,null,10,10,10,null,null,null,null,0);
            PlotData pd = PlotMatcher.matchPlot("Map");
            Assert.IsNull(pd);
        }

        [Test]
        public void ShouldNotTriggerPlotWhenInOtherMap()
        {
            Game.plotPt = 0;
            Game.currLoc = new Map(1,"name","",null,null,10,10,10,null,null,null,null,0);
            PlotData pd = PlotMatcher.matchPlot("Map");
            Assert.IsNull(pd);
        }

        [Test]
        public void ShouldTriggerPlotWhenInBattle()
        {
            Game.plotPt = 1;
            Game.currLoc = new Map(1,"name","",null,null,10,10,10,null,null,null,null,0);
            PlotData pd = PlotMatcher.matchPlotBattle(3,true);
            Assert.IsNotNull(pd);
            Assert.AreEqual(1,pd.triggerPt);
        }

        [Test]
        public void ShouldNotTriggerPlotWhenAreaNotMatch()
        {
            Game.plotPt = 1;
            Game.currLoc = new Map(1,"name","",null,null,10,10,10,null,null,null,null,0);
            PlotData pd = PlotMatcher.matchPlotBattle(1,true);
            Assert.IsNull(pd);
        }

        [Test]
        public void ShouldNotTriggerPlotAfterBattle()
        {
            Game.plotPt = 1;
            Game.currLoc = new Map(1,"name","",null,null,10,10,10,null,null,null,null,0);
            PlotData pd = PlotMatcher.matchPlotBattle(3,false);
            Assert.IsNull(pd);
        }
    }
}

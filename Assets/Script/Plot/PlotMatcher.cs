using System;
using System.Linq;
using UnityEngine;
namespace RPG
{
    public static class PlotMatcher
    {
        public static string nextScene { get; set; }
        public static PlotData nextPlot { get; set; }
        public static PlotData matchPlot(string nextScene)
        {
            return Array.Find(DB.plots, s => s.triggerPt == Game.plotPt && s.scene == nextScene && s.scene != "Battle" && s.triggerMap == Game.currLoc.id);
        }
        public static PlotData matchPlotBattle(int currArea, bool beforeBattle)
        {
            return Array.Find(DB.plots, s => s.triggerPt == Game.plotPt && s.triggerArea == currArea && s.triggerBeforeBattle == beforeBattle);
        }
        private static PlotData getLinkedPlot()
        {
            return Array.Find(DB.plots, s => s.triggerPt == Game.plotPt);
        }
        public static PlotData handlePlotEnd(PlotData pd)
        {
            if (Game.plotPt > 0)
            {
                int characterUnlock = Array.IndexOf(Constant.memberUnlockAt, Game.plotPt);
                if (characterUnlock > 0) Game.party.unlockCharacter(characterUnlock);

            }
            Game.plotPt += pd.plotPtIncrement;
            Game.saveGame();
            Debug.Log("End. Plot=" + Game.plotPt);
            if (pd.startNextPlotAtEnd)
            {
                return getLinkedPlot();
            }
            else
            {
                return null;
            }
        }
    }
}
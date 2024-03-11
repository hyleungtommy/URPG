using System;
using System.Linq;
using UnityEngine;
namespace RPG
{
    /// <summary>
    /// Controller to detect wether a plot should trigger
    /// </summary>
    public static class PlotMatcher
    {
        public static string nextScene { get; set; }
        public static PlotData nextPlot { get; set; }
        /// <summary>
        /// Check if the next scene shall trigger a plot
        /// </summary>
        /// <returns>PlotData if a plot shall be triggered, null otherwise</returns>
        public static PlotData matchPlot(string nextScene)
        {
            return Array.Find(DB.plots, s => s.triggerPt == Game.plotPt && s.scene == nextScene && s.scene != "Battle" && s.triggerMap == Game.currLoc.id);
        }
        /// <summary>
        /// Check if a plot shall trigger before or after the battle
        /// </summary>
        /// <returns>PlotData if a plot shall be triggered, null otherwise</returns>
        public static PlotData matchPlotBattle(int currArea, bool beforeBattle)
        {
            return Array.Find(DB.plots, s => s.triggerPt == Game.plotPt && s.triggerArea == currArea && s.triggerBeforeBattle == beforeBattle);
        }
        /// <summary>
        /// Check if a next plot shall be triggered immediately after the current one ends
        /// </summary>
        /// <returns>PlotData if a plot shall be triggered, null otherwise</returns>
        private static PlotData getLinkedPlot()
        {
            return Array.Find(DB.plots, s => s.triggerPt == Game.plotPt);
        }
        /// <summary>
        /// handle things to do when a plot end
        /// </summary>
        /// <returns>the next PlotData if the next plot shall be triggered immediately, null otherwise</returns>
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
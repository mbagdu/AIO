using EloBuddy;
using EloBuddy.SDK;
using System.Linq;
using UBAddons.Libs;

namespace UBAddons.Champions.Lissandra.Modes
{
    class LaneClear : Lissandra
    {
        public static void Execute()
        {
            if (MenuValue.LaneClear.UseQ && Q.IsReady())
            {
                var Minion = Q.GetLaneMinions(MenuValue.LaneClear.OnlyKillable);
                var farmLoc = Q.GetBestLinearCastPosition(Minion, MenuValue.General.QHitChance);
                if (farmLoc.HitNumber >= MenuValue.LaneClear.QHit)
                {
                    Q.Cast(farmLoc.CastPosition);
                }
            }
            if (MenuValue.LaneClear.UseW && W.IsReady())
            {
                var Minion = W.GetLaneMinions(MenuValue.LaneClear.OnlyKillable);
                if (player.CountEnemyMinionsInRangeWithPrediction((int)W.Range, W.CastDelay) >= MenuValue.LaneClear.WHit)
                {
                    W.Cast();
                }
            }
            if (MenuValue.LaneClear.UseE && E.IsReady())
            {
                var Minion = E.GetLaneMinions(MenuValue.LaneClear.OnlyKillable);
                var farmLoc = E.GetBestLinearCastPosition(Minion, MenuValue.General.QHitChance);
                if (farmLoc.HitNumber >= MenuValue.LaneClear.EHit)
                {
                    E.Cast(farmLoc.CastPosition);
                }
            }
        }
    }
}

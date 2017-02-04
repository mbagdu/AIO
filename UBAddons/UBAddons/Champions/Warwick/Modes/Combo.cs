using EloBuddy;
using EloBuddy.SDK;
using System.Linq;
using UBAddons.Libs;

namespace UBAddons.Champions.Warwick.Modes
{
    class Combo : Warwick
    {
        public static void Execute()
        {
            var Champ = EntityManager.Heroes.Enemies.Where(x => x.Health < HandleDamageIndicator(x));
            if (MenuValue.Combo.UseQ)
            {
                var target = Q.GetGapcloseTarget(375);
                if (target != null)
                {
                    //if (player.HealthPercent >= MenuValue.Combo.HP)
                    //{
                    //    if (Q.HoldingCast(target))
                    //    {
                    //        Core.DelayAction(() => Q.Cast(target), 300);
                    //    }
                    //}
                    //else
                    //{
                        Q.Cast(target);
                    //}
                }
            }
            if (MenuValue.Combo.UseE && E.IsReady() && /*E.ToggleState == 1 &&*/ player.HasBuff("WarwickE"))
            {
                var Count = player.CountEnemyHeroesInRangeWithPrediction((int)E.Range, 500);
                if (Count >= MenuValue.Combo.EHit)
                {
                    E.Cast();
                }
            }
            if (MenuValue.Combo.UseR && R.IsReady())
            {
                var target = R.GetGapcloseTarget(350);
                if (target != null)
                {
                    var pred = R.GetPrediction(target);
                    if (pred.CanNext(R, MenuValue.General.RHitChance, true))
                    {
                        R.Cast(pred.CastPosition);
                    }
                }
            }
        }
    }
}

using EloBuddy;
using EloBuddy.SDK;
using System.Linq;
using UBAddons.Libs;

namespace UBAddons.Champions.Warwick.Modes
{
    class PermaActive : Warwick
    {
        public static void Execute()
        {
            if (MenuValue.Misc.QKS && Q.IsReady())
            {
                var target = Q.GetKillableTarget();
                if (target != null)
                {
                    Q.Cast(target);
                }
            }
            if (MenuValue.Misc.RKS && R.IsReady())
            {
                var target = R.GetKillableTarget();
                if (target != null)
                {
                    var pred = R.GetPrediction(target);
                    if (pred.CanNext(R, MenuValue.General.RHitChance, false))
                    {
                        R.Cast(pred.CastPosition);
                    }
                }
            }
        }
    }
}

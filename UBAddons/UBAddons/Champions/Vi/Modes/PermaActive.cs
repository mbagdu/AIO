using EloBuddy;
using EloBuddy.SDK;
using UBAddons.Libs;

namespace UBAddons.Champions.Vi.Modes
{
    class PermaActive : Vi
    {
        public static void Execute()
        {
            if (MenuValue.Misc.QKS && Q.IsReady())
            {
                var target = Q.GetKillableTarget();
                if (target != null)
                {
                    CastQ(target);
                }
            }
            if (!Q.IsCharging)
            {
                if (MenuValue.Misc.EKS)
                {
                    if (E2.GetKillableTarget() != null)
                    {
                        CastE();
                    }
                }
                if (MenuValue.Combo.UseR)
                {
                    var target = R.GetGapcloseTarget(100);
                    if (target != null && MenuValue.Combo.UseROn(target))
                    {
                        R.Cast(target);
                    }
                }
            }
        }
    }
}

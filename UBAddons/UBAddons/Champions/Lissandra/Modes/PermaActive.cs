using EloBuddy;
using EloBuddy.SDK;
using System.Linq;
using UBAddons.Libs;

namespace UBAddons.Champions.Lissandra.Modes
{
    class PermaActive : Lissandra
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
            if (MenuValue.Misc.WKS && W.IsReady())
            {
                var target = W.GetKillableTarget();
                if (target != null)
                {
                    W.Cast();
                }
            }
            if (MenuValue.Misc.EKS && E.IsReady())
            {
                var target = E.GetKillableTarget();
                if (target != null)
                {
                    CastE(target, false);
                }
            }
            if (MenuValue.Misc.RKS && R.IsReady())
            {
                var target = R.GetKillableTarget();
                if (target != null)
                {
                    R.Cast(target);
                }
            }
        }
    }
}

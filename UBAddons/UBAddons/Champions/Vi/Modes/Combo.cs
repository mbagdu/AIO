using EloBuddy;
using EloBuddy.SDK;
using System.Linq;
using UBAddons.Libs;
using UBAddons.General;

namespace UBAddons.Champions.Vi.Modes
{
    class Combo : Vi
    {
        public static object E1 { get; private set; }

        public static void Execute()
        {
            var Champ = EntityManager.Heroes.Enemies.Where(x => x.Health < HandleDamageIndicator(x));
            if (MenuValue.Combo.UseQ && Q.IsReady())
            {
                var target = Q.GetGapcloseTarget(300);
                if (target != null)
                {
                    CastQ(target);
                }
            }
            if (!Q.IsCharging)
            {
                if (MenuValue.Combo.UseE )
                {
                    CastE();
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

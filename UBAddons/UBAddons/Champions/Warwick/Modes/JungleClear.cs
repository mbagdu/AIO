using EloBuddy.SDK;
using System.Linq;
using UBAddons.Libs;

namespace UBAddons.Champions.Warwick.Modes
{
    class JungleClear : Warwick
    {
        public static void Execute()
        {
            if (player.ManaPercent < MenuValue.JungleClear.ManaLimit) return;
            if (MenuValue.JungleClear.UseQ && Q.IsReady())
            {
                var JungMob = Q.GetJungleMobs();
                if (JungMob.Any())
                {
                    Q.Cast(JungMob.First());
                }
            }
        }
    }
}

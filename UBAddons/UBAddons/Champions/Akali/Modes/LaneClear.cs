using EloBuddy;
using EloBuddy.SDK;
using System.Linq;
using UBAddons.Libs;

namespace UBAddons.Champions.Akali.Modes
{
    class LaneClear : Akali
    {
        public static void Execute()
        {
            if (player.Mana < MenuValue.JungleClear.ManaLimit) return;
            if (MenuValue.LaneClear.UseQ && Q.IsReady())
            {
                var junglemobs = Q.GetJungleMobs();
                if (junglemobs.Any())
                {
                    Q.Cast(junglemobs.First());
                }
            }
            if (MenuValue.LaneClear.UseE && E.IsReady())
            {
                if (E.GetJungleMobs().Any())
                {
                    E.Cast();
                }
            }
        }
    }
}

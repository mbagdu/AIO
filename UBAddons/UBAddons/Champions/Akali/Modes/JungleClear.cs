using EloBuddy.SDK;
using System.Linq;
using UBAddons.Libs;

namespace UBAddons.Champions.Akali.Modes
{
    class JungleClear : Akali
    {
        public static void Execute()
        {
            if (player.Mana < MenuValue.JungleClear.ManaLimit) return;
            if (MenuValue.JungleClear.UseQ && Q.IsReady())
            {
                var minion = Q.GetLaneMinions(MenuValue.LaneClear.OnlyKillable);
                if (minion.Any())
                {
                    Q.Cast(minion.First());
                }
            }
            if (MenuValue.JungleClear.UseE && E.IsReady())
            {
                var minion = E.GetLaneMinions(MenuValue.LaneClear.OnlyKillable);
                if (minion.Count() > MenuValue.LaneClear.EHit)
                {
                    E.Cast();
                }
            }
        }
    }
}

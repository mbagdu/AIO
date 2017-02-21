using EloBuddy;
using EloBuddy.SDK;
using System.Linq;
using UBAddons.Libs;

namespace UBAddons.Champions.Vi.Modes
{
    class LaneClear : Vi
    {
        public static void Execute()
        {
            if (player.Mana < MenuValue.LaneClear.ManaLimit) return;
            if (ObjectManager.Get<AIHeroClient>().Any(x => x.IsValid && !x.IsDead && !x.IsZombie && player.IsInRange(x, MenuValue.LaneClear.ScanRange)
                && MenuValue.LaneClear.EnableIfNoEnemies)) return;
            if (MenuValue.LaneClear.UseQ && Q.IsReady())
            {
                var Minion = Q.GetLaneMinions(MenuValue.LaneClear.OnlyKillable);
                var farmLoc = Q.GetBestLinearCastPosition(Minion, MenuValue.General.QHitChance);
                if (farmLoc.HitNumber >= MenuValue.LaneClear.Qhit)
                {
                    Q.Cast(farmLoc.CastPosition);
                }
            }
            if (MenuValue.LaneClear.UseE && E.IsReady() && !Q.IsCharging)
            {
                var Minion = E2.GetLaneMinions(MenuValue.LaneClear.OnlyKillable);
                var farmLoc = E2.GetBestCircularCastPosition(Minion);
                if (farmLoc.HitNumber >= MenuValue.LaneClear.Ehit)
                {
                    E.Cast();
                }
            }
        }
    }
}

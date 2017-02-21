using EloBuddy;
using EloBuddy.SDK;
using System.Linq;
using UBAddons.Libs;

namespace UBAddons.Champions.Akali.Modes
{
    class Combo : Akali
    {
        public static void Execute()
        {
            var Champ = EntityManager.Heroes.Enemies.Where(x => x.Health < HandleDamageIndicator(x));
            if (MenuValue.Combo.UseQ && Q.IsReady())
            {
                var target = Q.GetTarget(Champ);
                if (target != null)
                {
                    Q.Cast(target);
                }
            }
            if (MenuValue.Combo.UseW && W.IsReady() && player.Mana > MenuValue.Combo.EnergyForW)
            {
                W.Cast(player.Position.Extend(Game.CursorPos, W.Range).To3DWorld());
            }
            if (MenuValue.Combo.UseE && E.IsReady())
            {
                var target = E.GetTarget();
                if (target != null)
                {
                    E.Cast();
                }
            }
            if (MenuValue.Combo.UseR && R.IsReady())
            {
                var target = R.GetGapcloseTarget(300);
                if (target != null)
                {
                    if (target.IsUnderHisturret())
                    {
                        switch (MenuValue.Combo.RLogics)
                        {
                            case 0:
                                {
                                    if (target.IsKillable(SpellSlot.Unknown))
                                    {
                                        R.Cast(target);
                                    }
                                }
                                break;
                            case 1:
                                {
                                    R.Cast(target);
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        R.Cast(target);
                    }
                }
            }
        }
    }
}

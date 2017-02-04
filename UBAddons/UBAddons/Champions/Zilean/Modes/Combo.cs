using EloBuddy.SDK;
using System.Linq;
using UBAddons.Libs;

namespace UBAddons.Champions.Zilean.Modes
{
    internal class Combo : Zilean
    {
        public static void Execute()
        {
            var Champ = EntityManager.Heroes.Enemies.Where(x => x.Health < HandleDamageIndicator(x));
            var target = Q.GetTarget(Champ);
            if (target == null) return;
            if (MenuValue.Combo.UseQ)
            {
                var pred = Q.GetPrediction(target);
                if (pred.CanNext(Q, MenuValue.General.QHitChance, false))
                {
                    Q.Cast(pred.CastPosition);
                }
            }
            if (MenuValue.Combo.UseW && !Q.IsReady(800) && W.IsReady())
            {
                W.Cast();
            }
            if (MenuValue.Combo.UseE && E.IsReady())
            {
                var Etar = E.GetTarget(0);
                switch (MenuValue.Combo.LogicE)
                {
                    case 0:
                        {
                            if (Etar == null && !player.HasBuff("TimeWarp") && W.IsReady())
                            {
                                E.Cast(player);
                            }
                            else
                            {
                                if (W.IsReady() && !player.HasBuff("TimeWarp"))
                                {
                                    E.Cast(player);
                                }
                                else
                                {
                                    E.Cast(target);
                                }
                            }
                            //if (Etar == null)
                            //{
                            //    E.Cast(player);
                            //}
                            //else
                            //{
                            //    if (player.IsFacing(Etar))
                            //    {
                            //        E.Cast(Etar);
                            //    }
                            //    else
                            //    {
                            //        E.Cast(player);
                            //    }
                            //}
                        }
                        break;
                    case 1:
                        {
                            if (Etar == null) return;
                            E.Cast(Etar);
                        }
                        break;
                    case 2:
                        {
                            E.Cast(player);
                        }
                        break;                       
                }
            }
        }
    }
}

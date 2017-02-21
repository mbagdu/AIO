using System.Linq;
using UBAddons.Libs;

namespace UBAddons.Champions.TwistedFate.Modes
{
    class JungleClear : TwistedFate
    {
        public static void Execute()
        {
            if (player.ManaPercent < MenuValue.JungleClear.ManaLimit) return;
            if (MenuValue.JungleClear.UseQ && Q.IsReady())
            {
                var JungleMob = Q.GetJungleMobs();
                if (JungleMob.Any())
                {
                    Q.Cast(JungleMob.First());
                }
            }
            if (MenuValue.JungleClear.UseW && W.IsReady())
            {
                var JungleMob = W.GetJungleMobs();
                if (JungleMob.Any())
                {
                    LogicPickedCard(MenuValue.JungleClear.UseW, MenuValue.JungleClear.WLogic);
                }
            }
        }
    }
}

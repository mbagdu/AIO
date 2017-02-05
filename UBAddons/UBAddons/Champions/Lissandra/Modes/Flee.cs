using EloBuddy;
using EloBuddy.SDK;

namespace UBAddons.Champions.Lissandra.Modes
{
    class Flee : Lissandra
    {
        public static void Execute()
        {
            if (E.IsReady())
            {
                CastE(null, true);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Events;
using UBAddons.Libs;
using UBAddons.General;

namespace UBAddons.UBCore.Activator
{
    class DefenseItem
    {
        public static void OnLoad()
        {
            BlockableSpells.OnBlockableSpell += BlockableSpells_OnBlockableSpell;
        }

        private static void BlockableSpells_OnBlockableSpell(AIHeroClient sender, BlockableSpells.OnBlockableSpellEventArgs args)
        {
            throw new NotImplementedException();
        }
        private static void BuildMenu()
        {
            if (Main.DefensiveMenu == null) return;
            var menu = Main.DefensiveMenu;

        }
    }
}

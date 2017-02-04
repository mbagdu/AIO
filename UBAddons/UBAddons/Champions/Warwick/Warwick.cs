using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using System;
using System.Linq;
using UBAddons.General;
using UBAddons.Libs;
using UBAddons.Log;
using Color = System.Drawing.Color;

namespace UBAddons.Champions.Warwick
{
    internal class Warwick : ChampionPlugin
    {
        internal static AIHeroClient player = Player.Instance;
        protected static Spell.Targeted Q { get; set; }
        protected static Spell.Active W { get; set; }
        protected static Spell.Active E { get; set; }
        protected static Spell.Skillshot R { get; set; }

        protected static Menu Menu { get; set; }
        protected static Menu ComboMenu { get; set; }
        protected static Menu JungleClearMenu { get; set; }
        protected static Menu FleeMenu { get; set; }
        protected static Menu MiscMenu { get; set; }
        protected static Menu DrawMenu { get; set; }

        static Warwick()
        {
            Q = new Spell.Targeted(SpellSlot.Q, 375, DamageType.Magical);            

            W = new Spell.Active(SpellSlot.W, 4000);

            E = new Spell.Active(SpellSlot.E, 375, DamageType.Magical);

            R = new Spell.Skillshot(SpellSlot.R, 0, SkillShotType.Linear, 250, 2200, 70, DamageType.Magical);

            DamageIndicator.DamageDelegate = HandleDamageIndicator;

            Obj_AI_Base.OnBasicAttack += delegate (Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
            {
                if (sender is Obj_AI_Turret || sender is AIHeroClient || sender.IsMonster)
                {
                    if (args.Target.IsMe && E.IsReady() && /*E.ToggleState == 1 &&*/ !player.HasBuff("WarwickE")
                    && (MenuValue.General.EOnAttack || (sender.IsMonster && MenuValue.JungleClear.UseE && Orbwalker.ActiveModes.JungleClear.IsOrb())))
                    {
                        E.Cast();
                    }
                }
            };
            Obj_AI_Base.OnProcessSpellCast += delegate (Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
            {
                if (args.Target != null && args.Target.IsMe && E.IsReady() && MenuValue.General.EOnSpell && /*E.ToggleState == 1 &&*/ !player.HasBuff("WarwickE"))
                {
                    E.Cast();
                }
            };
        }

        #region Creat Menu
        protected override void CreateMenu()
        {
            try
            {
                #region Mainmenu
                Menu = MainMenu.AddMenu("UB" + player.Hero, "UBAddons.MainMenu" + player.Hero, "UB" + player.Hero + " - UBAddons - by U.Boruto");
                Menu.AddGroupLabel("General Setting");
                Menu.CreatSlotHitChance(SpellSlot.R);
                Menu.AddGroupLabel("E Settings");
                Menu.Add("UBAddons.Warwick.E.Combo.Only", new CheckBox("Auto use on Combo Only"));
                Menu.Add("UBAddons.Warwick.E.Attack.Enable", new CheckBox("E on Enemy/Turret/Monster Attack"));
                Menu.Add("UBAddons.Warwick.E.Spell.Enable", new CheckBox("E on Enemy spell"));
                #endregion

                #region Combo
                ComboMenu = Menu.AddSubMenu("Combo", "UBAddons.ComboMenu" + player.Hero, "Settings your combo below");
                {
                    ComboMenu.CreatSlotCheckBox(SpellSlot.Q);
                    ComboMenu.Add("UBAddons.Warwick.Q.HP", new Slider("Min HP for Q behind target", 20));
                    ComboMenu.CreatSlotCheckBox(SpellSlot.W);
                    ComboMenu.CreatSlotCheckBox(SpellSlot.E);
                    ComboMenu.CreatSlotHitSlider(SpellSlot.E, 2, 1, 6);
                    ComboMenu.CreatSlotCheckBox(SpellSlot.R);
                }
                #endregion

                #region JungleClear
                JungleClearMenu = Menu.AddSubMenu("JungleClear", "UBAddons.JungleClear" + player.Hero, "Settings your jungleclear below");
                {
                    JungleClearMenu.CreatSlotCheckBox(SpellSlot.Q);
                    JungleClearMenu.CreatSlotCheckBox(SpellSlot.E);
                    JungleClearMenu.CreatManaLimit();
                }
                #endregion

                FleeMenu = Menu.AddSubMenu("Flee", "UBAddons.Flee" + player.Hero, "Setting your flee below");
                {
                    string BeginText = Variables.AddonName + "." + Player.Instance.Hero + ".";
                    FleeMenu.Add(BeginText + "Q.ToMinion", new CheckBox("AA to minion"));
                    FleeMenu.Add(BeginText + "Q.ToMonster", new CheckBox("AA to monster"));
                    FleeMenu.Add(BeginText + "Q.ToChamp", new CheckBox("AA to champ"));
                    FleeMenu.Add(BeginText + "Q.HP", new Slider("Min {0}% HP for AA champ & monster", 15));
                }

                #region Misc
                MiscMenu = Menu.AddSubMenu("Misc", "UBAddons.Misc" + player.Hero, "Settings your misc below");
                {
                    MiscMenu.AddGroupLabel("Interrupter settings");
                    MiscMenu.CreatDangerValueBox();
                    MiscMenu.CreatSlotCheckBox(SpellSlot.R, "Interrupter");
                    MiscMenu.AddGroupLabel("Killsteal settings");
                    MiscMenu.CreatSlotCheckBox(SpellSlot.Q, "KillSteal");
                    MiscMenu.CreatSlotCheckBox(SpellSlot.R, "KillSteal");
                }
                #endregion

                #region Drawings
                DrawMenu = Menu.AddSubMenu("Drawings", "UBAddons.Drawings" + player.Hero, "Settings your drawings below");
                {
                    DrawMenu.CreatDrawingOpening();
                    DrawMenu.CreatColorPicker(SpellSlot.Q);
                    DrawMenu.CreatColorPicker(SpellSlot.W);
                    DrawMenu.CreatColorPicker(SpellSlot.E);
                    DrawMenu.CreatColorPicker(SpellSlot.R);
                    DrawMenu.CreatColorPicker(SpellSlot.Unknown);
                }
                #endregion

                DamageIndicator.Initalize(MenuValue.Drawings.ColorDmg);
            }
            catch (Exception exception)
            {
                Debug.Print(exception.ToString(), Console_Message.Error);
            }
        }
        #endregion

        #region Boolean
        protected override bool EnableDraw
        {
            get { return MenuValue.Drawings.EnableDraw; }
        }

        protected override bool EnableDamageIndicator
        {
            get { return MenuValue.Drawings.DrawDamageIndicator; }
        }

        protected override bool IsAutoHarass
        {
            get { return false; }
        }
        #endregion

        #region Misc
        protected override void OnGapcloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs args)
        {
            return;          
        }

        protected override void OnInterruptable(Obj_AI_Base sender, Interrupter.InterruptableSpellEventArgs args)
        {
            if (sender == null || !sender.IsEnemy || !sender.IsValidTarget() || !MenuValue.Misc.dangerValue.Contains(args.DangerLevel)) return;
            if (R.IsReady() && MenuValue.Misc.RI && R.IsInRange(sender))
            {
                R.Cast(sender);
            }
        }

        protected override void OnUnkillableMinion(Obj_AI_Base target, Orbwalker.UnkillableMinionArgs args)
        {
            return;
        }
        protected static float CalculatorRatio()
        {
            if (player.MoveSpeed <= 400)
            {
                return 1.9f;
            }
            if (player.MoveSpeed <= 500)
            {
                return 1.85f;
            }
            if (player.MoveSpeed <= 530)
            {
                return 1.9f;
            }
            if (player.MoveSpeed <= 550)
            {
                return 1.95f;
            }
            if (player.MoveSpeed <= 600)
            {
                return 1.95f;
            }
            return 2.18f;
        }
        #endregion

        #region Damage

        #region DamageRaw
        public static float QDamage(Obj_AI_Base target)
        {
            return player.CalculateDamageOnUnit(target, DamageType.Magical, new[] { 0f, 0.06f, 0.07f, 0.08f, 0.09f, 0.1f }[Q.Level] * target.MaxHealth + 1.2f * player.TotalAttackDamage + 0.9f * player.TotalMagicalDamage);
        }
        public static float WDamage(Obj_AI_Base target)
        {
            return 0;
        }
        public static float EDamage(Obj_AI_Base target)
        {
            return 0;
        }
        public static float RDamage(Obj_AI_Base target)
        {
            return player.CalculateDamageOnUnit(target, DamageType.Magical, new[] { 0f, 175f, 350f, 525f }[R.Level] + 1.675f * player.FlatPhysicalDamageMod);
        }
        #endregion

        internal static float HandleDamageIndicator(Obj_AI_Base target, SpellSlot? slot = null)
        {
            if (target == null)
            {
                return 0;
            }
            switch (slot)
            {
                case SpellSlot.Q:
                    return QDamage(target);
                case SpellSlot.W:
                    return WDamage(target);
                case SpellSlot.E:
                    return EDamage(target);
                case SpellSlot.R:
                    return RDamage(target);
                default:
                    {
                        float damage = 0f;
                        if (Q.IsReady())
                        {
                            damage = damage + QDamage(target);
                        }
                        if (W.IsReady())
                        {
                            damage = damage + WDamage(target);
                        }
                        if (E.IsReady())
                        {
                            damage = damage + EDamage(target);
                        }
                        if (R.IsReady())
                        {
                            damage = damage + RDamage(target);
                        }
                        if (Orbwalker.CanAutoAttack)
                        {
                            damage = damage + player.GetAutoAttackDamage(target, true);
                        }
                        return damage;
                    }
            }
        }
        #endregion

        #region Modes
        protected override void PermaActive()
        {
            Modes.PermaActive.Execute();
        }

        protected override void Combo()
        {
            Modes.Combo.Execute();
        }

        protected override void Harass()
        {
            if (player.IsUnderEnemyturret()) return;
            Modes.Harass.Execute();
        }

        protected override void LaneClear()
        {
            Modes.LaneClear.Execute();
        }

        protected override void JungleClear()
        {
            Modes.JungleClear.Execute();
        }

        protected override void LastHit()
        {
            Modes.LastHit.Execute();
        }

        protected override void Flee()
        {
            Modes.Flee.Execute();
        }
        #endregion

        #region Drawings
        protected override void OnDraw(EventArgs args)
        {
            R.Range = (uint)(player.MoveSpeed * CalculatorRatio());
            if (!MenuValue.Drawings.EnableDraw) return;
            if (MenuValue.Drawings.DrawQ && (!MenuValue.Drawings.ReadyQ || Q.IsReady()))
            {
                Q.DrawRange(MenuValue.Drawings.ColorQ);
            }
            if (MenuValue.Drawings.DrawW && (!MenuValue.Drawings.ReadyW || W.IsReady()))
            {
                W.DrawRange(MenuValue.Drawings.ColorW);
            }
            if (MenuValue.Drawings.DrawE && (!MenuValue.Drawings.ReadyE || E.IsReady()))
            {
                E.DrawRange(MenuValue.Drawings.ColorE);
            }
            if (MenuValue.Drawings.DrawR && (!MenuValue.Drawings.ReadyR || R.IsReady()))
            {
                R.DrawRange(MenuValue.Drawings.ColorR);
            }
        }
        #endregion

        #region Menu Value
        protected internal static class MenuValue
        {
            internal static class General
            {
                public static int RHitChance { get { return Menu.GetSlotHitChance(SpellSlot.R); } }

                public static bool ComboOnly { get { return Orbwalker.ActiveModes.Combo.IsOrb() || !Menu.VChecked("UBAddons.Warwick.E.Combo.Only"); } }

                public static bool EOnAttack { get { return ComboOnly && Menu.VChecked("UBAddons.Warwick.E.Attack.Enable"); } }

                public static bool EOnSpell { get { return ComboOnly && Menu.VChecked("UBAddons.Warwick.E.Spell.Enable"); } }

            }

            internal static class Combo
            {
                public static bool UseQ { get { return ComboMenu.GetSlotCheckBox(SpellSlot.Q); } }

                public static int HP { get { return ComboMenu.VSliderValue("UBAddons.Warwick.Q.HP"); } }

                public static bool UseE { get { return ComboMenu.GetSlotCheckBox(SpellSlot.E); } }

                public static int EHit { get { return ComboMenu.GetSlotHitSlider(SpellSlot.E); } }

                public static bool UseR { get { return ComboMenu.GetSlotCheckBox(SpellSlot.R); } }
            }
            internal static class JungleClear
            {

                public static bool UseQ { get { return JungleClearMenu.GetSlotCheckBox(SpellSlot.Q); } }

                public static bool UseE { get { return JungleClearMenu.GetSlotCheckBox(SpellSlot.E); } }

                public static int ManaLimit { get { return JungleClearMenu.GetManaLimit(); } }
            }
            internal static class Flee
            {
                private static string BeginText = Variables.AddonName + "." + Player.Instance.Hero + ".";

                public static bool QMinion { get { return FleeMenu.VChecked(BeginText + "Q.ToMinion"); } }

                public static bool QMonster { get { return FleeMenu.VChecked(BeginText + "Q.ToMonster"); } }

                public static bool QChamp { get { return FleeMenu.VChecked(BeginText + "Q.ToChamp"); } }

                public static int HP { get { return FleeMenu.VSliderValue(BeginText + "Q.HP"); } }
            }

            internal static class Misc
            {
                public static bool QKS { get { return MiscMenu.GetSlotCheckBox(SpellSlot.Q, Misc_Menu_Value.KillSteal.ToString()); } }

                public static bool RKS { get { return MiscMenu.GetSlotCheckBox(SpellSlot.R, Misc_Menu_Value.KillSteal.ToString()); } }

                public static bool Idiot { get { return MiscMenu.PreventIdiotAntiGap(); } }

                public static DangerLevel[] dangerValue { get { return MiscMenu.GetDangerValue(); } }

                public static bool RI { get { return MiscMenu.GetSlotCheckBox(SpellSlot.R, Misc_Menu_Value.Interrupter.ToString()); } }
            }

            internal static class Drawings
            {

                public static bool EnableDraw { get { return DrawMenu.VChecked(Variables.AddonName + "." + player.Hero + ".EnableDraw"); } }

                public static bool DrawQ { get { return DrawMenu.GetDrawCheckValue(SpellSlot.Q); } }

                public static bool ReadyQ { get { return DrawMenu.GetOnlyReady(SpellSlot.Q); } }

                public static SharpDX.Color ColorQ { get { return DrawMenu.GetColorPicker(SpellSlot.Q).ToSharpDX(); } }

                public static bool DrawW { get { return DrawMenu.GetDrawCheckValue(SpellSlot.W); } }

                public static bool ReadyW { get { return DrawMenu.GetOnlyReady(SpellSlot.W); } }

                public static SharpDX.Color ColorW { get { return DrawMenu.GetColorPicker(SpellSlot.W).ToSharpDX(); } }


                public static bool DrawE { get { return DrawMenu.GetDrawCheckValue(SpellSlot.E); } }

                public static bool ReadyE { get { return DrawMenu.GetOnlyReady(SpellSlot.E); } }

                public static SharpDX.Color ColorE { get { return DrawMenu.GetColorPicker(SpellSlot.E).ToSharpDX(); } }

                public static bool DrawR { get { return DrawMenu.GetDrawCheckValue(SpellSlot.R); } }

                public static bool ReadyR { get { return DrawMenu.GetOnlyReady(SpellSlot.R); } }

                public static SharpDX.Color ColorR { get { return DrawMenu.GetColorPicker(SpellSlot.R).ToSharpDX(); } }

                public static bool DrawDamageIndicator { get { return DrawMenu.GetDrawCheckValue(SpellSlot.Unknown); } }

                public static Color ColorDmg { get { return DrawMenu.GetColorPicker(SpellSlot.Unknown); } }

            }
        }
        #endregion
    }
}

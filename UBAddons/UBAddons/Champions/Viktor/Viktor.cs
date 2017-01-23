using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using UBAddons.General;
using UBAddons.Libs;
using UBAddons.Libs.Dictionary;
using UBAddons.Log;

namespace UBAddons.Champions.Viktor
{
    internal class Viktor : ChampionPlugin
    {
        protected static AIHeroClient player = Player.Instance;
        protected static bool QUpgrade { get; set; }
        protected static bool WUpgrade { get; set; }
        protected static bool EUpgrade { get; set; }

        protected static Spell.Targeted Q { get; set; }
        protected static Spell.Skillshot W { get; set; }
        protected static Spell.Skillshot E { get; set; }
        protected static Spell.SimpleSkillshot E1 { get; set; }

        protected static Spell.Skillshot R { get; set; }

        protected static Menu Menu { get; set; }
        protected static Menu ComboMenu { get; set; }
        protected static Menu HarassMenu { get; set; }
        protected static Menu LaneClearMenu { get; set; }
        protected static Menu JungleClearMenu { get; set; }
        protected static Menu LasthitMenu { get; set; }
        protected static Menu MiscMenu { get; set; }
        protected static Menu DrawMenu { get; set; }

        static Viktor()
        {
            Q = new Spell.Targeted(SpellSlot.Q, 650, DamageType.Magical);
            W = new Spell.Skillshot(SpellSlot.W, 700, SkillShotType.Circular, 250, int.MaxValue, 300)
            {
                AllowedCollisionCount = int.MaxValue,
            };
            E = new Spell.Skillshot(SpellSlot.E, 1225, SkillShotType.Linear, 50, 1050, 100)
            {
                AllowedCollisionCount = int.MaxValue,
            };
            E1 = new Spell.SimpleSkillshot(SpellSlot.E, 525, DamageType.Magical)
            {
                CastDelay = 50,
            };
            R = new Spell.Skillshot(SpellSlot.R, 700, SkillShotType.Circular, 250, int.MaxValue, 450)
            {
                AllowedCollisionCount = int.MaxValue,
            };

            DamageIndicator.DamageDelegate = HandleDamageIndicator;
            ImmobileTracker.Initialize();
        }

        protected override void CreateMenu()
        {
            try
            {
                #region Mainmenu
                Menu = MainMenu.AddMenu("UB" + player.Hero, "UBAddons.MainMenu" + player.Hero, "UB" + player.Hero + " - UBAddons - by U.Boruto");
                Menu.AddGroupLabel("General Setting");
                Menu.CreatSlotHitChance(SpellSlot.W);
                Menu.Add("UBAddons.Viktor.E.Direction", new CheckBox("Allow use E direction"));
                Menu.AddLabel("Tips: Turn on direction will make your E look like human");
                Menu.CreatSlotHitChance(SpellSlot.E);
                Menu.CreatSlotHitChance(SpellSlot.R);
                Menu.AddGroupLabel("Immobile Setting");
                Menu.Add("UBAddons.Viktor.W.CC", new CheckBox("W CC stacks"));
                Menu.Add("UBAddons.Viktor.W.Teleport", new CheckBox("W Teleport"));
                Menu.Add("UBAddons.Viktor.W.Revive", new CheckBox("W Revive"));

                #endregion

                #region Combo
                ComboMenu = Menu.AddSubMenu("Combo", "UBAddons.ComboMenu" + player.Hero, "UB" + player.Hero + " - Settings your combo below");
                {
                    ComboMenu.CreatSlotCheckBox(SpellSlot.Q);
                    ComboMenu.CreatSlotCheckBox(SpellSlot.W, null, false);
                    ComboMenu.CreatSlotHitSlider(SpellSlot.W, 1, 1, 5);
                    ComboMenu.CreatSlotCheckBox(SpellSlot.E);
                    ComboMenu.CreatSlotCheckBox(SpellSlot.R);
                    ComboMenu.CreatSlotHitSlider(SpellSlot.R, 2, 1, 5);
                }
                #endregion

                #region Harass
                HarassMenu = Menu.AddSubMenu("Harass", "UBAddons.HarassMenu" + player.Hero, "UB" + player.Hero + " - Settings your harass below");
                {
                    HarassMenu.CreatSlotCheckBox(SpellSlot.Q);
                    HarassMenu.CreatSlotCheckBox(SpellSlot.W, null, false);
                    HarassMenu.CreatSlotHitSlider(SpellSlot.W, 1, 1, 5);
                    HarassMenu.CreatSlotCheckBox(SpellSlot.E);
                    HarassMenu.CreatManaLimit();
                }
                #endregion

                #region LaneClear
                LaneClearMenu = Menu.AddSubMenu("LaneClear", "UBAddons.LaneClear" + player.Hero, "UB" + player.Hero + " - Settings your laneclear below");
                {
                    LaneClearMenu.CreatLaneClearOpening();
                    LaneClearMenu.CreatSlotCheckBox(SpellSlot.Q, null, false);
                    LaneClearMenu.CreatSlotCheckBox(SpellSlot.W, null, false);
                    LaneClearMenu.AddLabel("W only work if you upgraded");
                    LaneClearMenu.CreatSlotHitSlider(SpellSlot.W, 5, 1, 10);
                    LaneClearMenu.CreatSlotCheckBox(SpellSlot.E, null, false);
                    LaneClearMenu.CreatSlotHitSlider(SpellSlot.E, 5, 1, 10);
                    LaneClearMenu.CreatManaLimit();
                }
                #endregion

                #region JungleClear
                JungleClearMenu = Menu.AddSubMenu("JungleClear", "UBAddons.JungleClear" + player.Hero, "UB" + player.Hero + " - Settings your jungleclear below");
                {
                    JungleClearMenu.CreatSlotCheckBox(SpellSlot.Q, null, false);
                    //JungleClearMenu.CreatSlotCheckBox(SpellSlot.W, null, false);
                    //JungleClearMenu.CreatSlotHitSlider(SpellSlot.W, 1, 1, 6);
                    JungleClearMenu.CreatSlotCheckBox(SpellSlot.E, null, false);
                    JungleClearMenu.CreatManaLimit();
                }
                #endregion

                #region Lasthit
                LasthitMenu = Menu.AddSubMenu("Lasthit", "UBAddons.Lasthit" + player.Hero, "UB" + player.Hero + " - Settings your unkillable minion below");
                {
                    LasthitMenu.CreatLasthitOpening();
                    LasthitMenu.CreatSlotCheckBox(SpellSlot.Q);
                    LasthitMenu.CreatSlotCheckBox(SpellSlot.E, null, false);
                    LasthitMenu.CreatManaLimit();
                }
                #endregion

                #region Misc
                MiscMenu = Menu.AddSubMenu("Misc", "UBAddons.Misc" + player.Hero, "UB" + player.Hero + " - Settings your misc below");
                {
                    MiscMenu.AddGroupLabel("Anti Gapcloser settings");
                    MiscMenu.CreatMiscGapCloser();
                    MiscMenu.CreatSlotCheckBox(SpellSlot.Q, "GapCloser");
                    MiscMenu.CreatSlotCheckBox(SpellSlot.W, "GapCloser");
                    MiscMenu.CreatSlotCheckBox(SpellSlot.E, "GapCloser");
                    MiscMenu.AddGroupLabel("Interrupter settings");
                    MiscMenu.CreatDangerValueBox();
                    MiscMenu.CreatSlotCheckBox(SpellSlot.W, "Interrupter");
                    MiscMenu.CreatSlotCheckBox(SpellSlot.R, "Interrupter", false);
                    MiscMenu.AddGroupLabel("Killsteal settings");
                    MiscMenu.CreatSlotCheckBox(SpellSlot.Q, "KillSteal");
                    MiscMenu.CreatSlotCheckBox(SpellSlot.E, "KillSteal");
                    MiscMenu.CreatSlotCheckBox(SpellSlot.R, "KillSteal");
                    MiscMenu.AddGroupLabel("Deny Recall settings");
                    MiscMenu.CreatSlotCheckBox(SpellSlot.Q, Misc_Menu_Value.DenyRecall.ToString());
                    MiscMenu.CreatSlotCheckBox(SpellSlot.E, Misc_Menu_Value.DenyRecall.ToString());

                }
                #endregion

                #region Drawings
                DrawMenu = Menu.AddSubMenu("Drawings");
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

        #region Misc
        protected override void OnGapcloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs args)
        {
            if (sender == null || !sender.IsEnemy || !sender.IsValid) return;
            if (W.IsReady() && (MenuValue.Misc.Idiot ? player.Distance(args.End) <= 350 : W.IsInRange(args.End) || sender.IsAttackingPlayer) && MenuValue.Misc.WGap)
            {
                W.Cast(sender);
            }
        }

        protected override void OnInterruptable(Obj_AI_Base sender, Interrupter.InterruptableSpellEventArgs args)
        {
            if (sender == null || !sender.IsEnemy || !sender.IsValidTarget() || !MenuValue.Misc.GetdangerValue().Contains(args.DangerLevel)) return;
            if (W.IsReady() && MenuValue.Misc.WI)
            {
                W.Cast(sender);
            }
        }

        protected override void OnUnkillableMinion(Obj_AI_Base target, Orbwalker.UnkillableMinionArgs args)
        {
            if (target == null || target.IsInvulnerable || !target.IsValidTarget()) return;
            if (MenuValue.LastHit.PreventCombo && Orbwalker.ActiveModes.Combo.IsOrb()) return;
            if (MenuValue.LastHit.OnlyFarmMode && !Variables.IsFarm) return;
            if (player.ManaPercent < MenuValue.LastHit.ManaLimit) return;
            if (args.RemainingHealth <= DamageIndicator.DamageDelegate(target, SpellSlot.Q) && MenuValue.LastHit.UseQ && Q.IsReady())
            {
                var predHealth = Q.GetHealthPrediction(target);
                if (predHealth < float.Epsilon) return;
                Q.Cast(target);
            }
            if (args.RemainingHealth <= DamageIndicator.DamageDelegate(target, SpellSlot.E) && MenuValue.LastHit.UseE && E.IsReady())
            {
                var predHealth = E.GetHealthPrediction(target);
                if (predHealth < float.Epsilon) return;
                CastE(target);
            }
        }
        protected override void OnTeleport(Obj_AI_Base sender, Teleport.TeleportEventArgs args)
        {
            if (sender == null || !sender.IsEnemy || !sender.IsValidTarget() || args.Type != TeleportType.Recall) return;
            if (MenuValue.Misc.QRecall && Q.IsReady() && Q.IsInRange(sender))
            {
                Q.Cast(sender);
            }
            if (MenuValue.Misc.ERecall && E.IsReady() && E.IsInRange(sender))
            {
                CastE(sender);
            }
        }    
        internal static bool CastE(Obj_AI_Base target)
        {
            if (target == null || !E.IsReady())
            {
                E.SourcePosition = null;
                return false;
            }
            else
            {
                if (E1.IsInRange(target))
                {
                    E.SourcePosition = target.Position;
                    var otherchamp = EntityManager.Heroes.Enemies.Where(x => x != target);
                    var othertarget = E.GetTarget(otherchamp);
                    var pred = E.GetPrediction(target);
                    if (pred.CanNext(E, MenuValue.General.EHitChance, false))
                    {
                        if (othertarget == null)
                        {
                            Vector3[] location = target.Position.Symmetry(pred.UnitPosition, 335);
                            Vector3 start = E1.IsInRange(location[0]) ? location[0] : location[1];
                            Vector3 end = E1.IsInRange(location[0]) ? location[1] : location[0];
                            if (E.CastStartToEnd(end, start))
                            {
                                return true;
                            }
                            return false;
                        }
                        else
                        {
                            var pred2 = E.GetPrediction(othertarget);
                            if (pred2.CastPosition.Distance(target) <= E.Range - E1.Range)
                            {
                                if (E.CastStartToEnd(pred2.CastPosition, target.Position))
                                {
                                    return true;
                                }
                                return false;
                            }
                            else
                            {
                                Vector3[] location = target.Position.Symmetry(pred.UnitPosition, 335);
                                Vector3 start = E1.IsInRange(location[0]) ? location[0] : location[1];
                                Vector3 end = E1.IsInRange(location[0]) ? location[1] : location[0];
                                if (E.CastStartToEnd(end, start))
                                {
                                    return true;
                                }
                                return false;
                            }
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    E.SourcePosition = player.Position.Extend(target, E1.Range).To3DWorld();
                    var pred = E.GetPrediction(target);
                    Vector2 intersection1;
                    Vector2 intersection2;
                    int count = Geometry.LineCircleIntersection(player.Position.X, player.Position.Y, E1.Range, pred.CastPosition.To2D(), pred.CastPosition.Extend(target.Position, 600), out intersection1, out intersection2);
                    Vector3[] intersection = new[] { intersection1.To3DWorld(), intersection2.To3DWorld() };
                    if (pred.CanNext(E, MenuValue.General.EHitChance, false))
                    {
                        if (E1.IsInRange(pred.CastPosition))
                        {
                            if (E.CastStartToEnd(pred.CastPosition, player.Position.Extend(pred.CastPosition, E1.Range - 250).To3DWorld()))
                            {
                                return true;
                            }
                        }
                        else
                        {
                            if (count != 0 && E.Range - player.Distance(pred.CastPosition) > 100 && MenuValue.General.EDirection)
                            {
                                if (E.CastStartToEnd(pred.CastPosition, intersection.OrderBy(x => x.Distance(pred.CastPosition)).FirstOrDefault()))
                                {
                                    return true;
                                }
                            }
                            else if (E.CastStartToEnd(pred.CastPosition, player.Position.Extend(pred.CastPosition, E1.Range - 5).To3DWorld()))
                            {
                                return true;
                            }
                        }
                    }
                    return false;
                }
            }
        }      
        #endregion

        #region Damage

        #region DamageRaw
        protected static float QDamage(Obj_AI_Base target)
        {
            return player.GetSpellDamage(target, SpellSlot.Q, DamageLibrary.SpellStages.Default);
        }
        protected static float EDamage(Obj_AI_Base target)
        {
            var firstdamage = player.CalculateDamageOnUnit(target, DamageType.Magical, new float[] { 0f, 70f, 110f, 150f, 190f, 230f }[E.Level] + 0.5f * player.TotalMagicalDamage);
            var seconddamage = player.CalculateDamageOnUnit(target, DamageType.Magical, new float[] { 0f, 20f, 60f, 100f, 140f, 180f }[E.Level] + 0.7f * player.TotalMagicalDamage);
            if (!EUpgrade)
            {
                return firstdamage;
            }
            else
            {
                if (!(target is AIHeroClient))
                {
                    return firstdamage + seconddamage;
                }
                else
                {
                    if (ImmobileTracker.WillBeImmobile(target, 1))
                    {
                        return firstdamage + seconddamage;
                    }
                    else
                        return firstdamage;
                }
            }
        }
        protected static float RDamage(Obj_AI_Base target)
        {
            var champ = target as AIHeroClient;
            var raw = player.CalculateDamageOnUnit(target, DamageType.Magical, new float[] { 0f, 100f, 175f, 250f }[R.Level] + 0.5f * player.TotalMagicalDamage);
            if (champ != null)
            {
                if (champ.BrainIsCharged())
                {
                    return raw;
                }
                else
                    return raw + player.CalculateDamageOnUnit(target, DamageType.Magical, new float[] { 0f, 150f, 250f, 350f }[R.Level] + 0.6f * player.TotalMagicalDamage);
            }
            else
            {
                return raw;
            }
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
            get { return MenuValue.Harass.IsAuto; }
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
                E1.DrawRange(MenuValue.Drawings.ColorE);
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
                public static int WHitChance { get { return Menu.GetSlotHitChance(SpellSlot.W); } }

                public static bool EDirection { get { return Menu.VChecked("UBAddons.Viktor.E.Direction"); } }

                public static int EHitChance { get { return Menu.GetSlotHitChance(SpellSlot.E); } }

                public static int RHitChance { get { return Menu.GetSlotHitChance(SpellSlot.R); } }

                public static bool WCCStacks { get { return Menu.VChecked("UBAddons.Viktor.W.CC"); } }

                public static bool WTeleport { get { return Menu.VChecked("UBAddons.Viktor.W.Teleport"); } }

                public static bool WRevive { get { return Menu.VChecked("UBAddons.Viktor.W.Revive"); } }

            }
            internal static class Combo
            {
                public static bool UseQ { get { return ComboMenu.GetSlotCheckBox(SpellSlot.Q); } }

                public static bool UseW { get { return ComboMenu.GetSlotCheckBox(SpellSlot.W); } }

                public static int Whit { get { return ComboMenu.GetSlotHitSlider(SpellSlot.W); } }

                public static bool UseE { get { return ComboMenu.GetSlotCheckBox(SpellSlot.E); } }

                public static bool UseR { get { return ComboMenu.GetSlotCheckBox(SpellSlot.R); } }

                public static int Rhit { get { return ComboMenu.GetSlotHitSlider(SpellSlot.R); } }

            }

            internal static class Harass
            {
                public static bool UseQ { get { return HarassMenu.GetSlotCheckBox(SpellSlot.Q); } }

                public static bool UseW { get { return HarassMenu.GetSlotCheckBox(SpellSlot.W); } }

                public static int Whit { get { return HarassMenu.GetSlotHitSlider(SpellSlot.W); } }

                public static bool UseE { get { return HarassMenu.GetSlotCheckBox(SpellSlot.E); } }

                public static int ManaLimit { get { return HarassMenu.GetManaLimit(); } }

                public static bool IsAuto { get { return HarassMenu.GetHarassKeyBind(); } }
            }

            internal static class LaneClear
            {
                public static bool EnableIfNoEnemies { get { return LaneClearMenu.GetNoEnemyOnly(); } }

                public static int ScanRange { get { return LaneClearMenu.GetDetectRange(); } }

                public static bool OnlyKillable { get { return LaneClearMenu.GetKillableOnly(); } }

                public static bool UseQ { get { return LaneClearMenu.GetSlotCheckBox(SpellSlot.Q); } }

                public static bool UseW { get { return LaneClearMenu.GetSlotCheckBox(SpellSlot.W); } }

                public static int Whit { get { return LaneClearMenu.GetSlotHitSlider(SpellSlot.W); } }

                public static bool UseE { get { return LaneClearMenu.GetSlotCheckBox(SpellSlot.E); } }

                public static int Ehit { get { return LaneClearMenu.GetSlotHitSlider(SpellSlot.E); } }

                public static int ManaLimit { get { return LaneClearMenu.GetManaLimit(); } }
            }

            internal static class JungleClear
            {

                public static bool UseQ { get { return JungleClearMenu.GetSlotCheckBox(SpellSlot.Q); } }

                public static int Whit { get { return JungleClearMenu.GetSlotHitSlider(SpellSlot.W); } }

                public static bool UseE { get { return JungleClearMenu.GetSlotCheckBox(SpellSlot.E); } }

                public static int ManaLimit { get { return JungleClearMenu.GetManaLimit(); } }
            }

            internal static class LastHit
            {
                public static bool OnlyFarmMode { get { return LasthitMenu.OnlyFarmMode(); } }

                public static bool PreventCombo { get { return LasthitMenu.PreventCombo(); } }

                public static bool UseQ { get { return LasthitMenu.GetSlotCheckBox(SpellSlot.Q); } }

                public static bool UseE { get { return LasthitMenu.GetSlotCheckBox(SpellSlot.E); } }

                public static int ManaLimit { get { return LasthitMenu.GetManaLimit(); } }
            }

            internal static class Misc
            {
                public static bool QKS { get { return MiscMenu.GetSlotCheckBox(SpellSlot.Q, Misc_Menu_Value.KillSteal.ToString()); } }

                public static bool EKS { get { return MiscMenu.GetSlotCheckBox(SpellSlot.E, Misc_Menu_Value.KillSteal.ToString()); } }

                public static bool RKS { get { return MiscMenu.GetSlotCheckBox(SpellSlot.R, Misc_Menu_Value.KillSteal.ToString()); } }

                public static bool Idiot { get { return MiscMenu.PreventIdiotAntiGap(); } }

                public static bool WGap { get { return MiscMenu.GetSlotCheckBox(SpellSlot.W, Misc_Menu_Value.GapCloser.ToString()); } }

                public static DangerLevel[] GetdangerValue()
                {
                    return MiscMenu.GetDangerValue();
                }

                public static bool WI { get { return MiscMenu.GetSlotCheckBox(SpellSlot.W, Misc_Menu_Value.Interrupter.ToString()); } }

                public static bool RI { get { return MiscMenu.GetSlotCheckBox(SpellSlot.R, Misc_Menu_Value.Interrupter.ToString()); } }

                public static bool QRecall { get { return MiscMenu.GetSlotCheckBox(SpellSlot.Q, Misc_Menu_Value.DenyRecall.ToString()); } }

                public static bool ERecall { get { return MiscMenu.GetSlotCheckBox(SpellSlot.E, Misc_Menu_Value.DenyRecall.ToString()); } }

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

                public static System.Drawing.Color ColorDmg { get { return DrawMenu.GetColorPicker(SpellSlot.Unknown); } }
            }
        }
        #endregion
    }
    /// <summary>
    /// Credit for Icreative
    /// </summary>
    class ImmobileTracker : Viktor
    {
        private const int Width = 300;
        private const string BlitzcrankBuffName = "RocketGrab";
        private const string ThreshBuffName = "ThreshQ";
        private const string ThreshBuffName2 = "threshqfakeknockup";
        private const string AatroxDeath = "AatroxPassiveDeath";
        private const string AniviaRebirth = "rebirth";
        private const string ZileanBuffRevive = "chronorevive";
        private const string FioraW = "fioraw";
        private const string AngleRevive = "willrevive";
        private const string ZhonyasBuffName = "zhonyasringshield";
        private const string BardRBuffName = "bardrstasis";
        private const int ThreshKnockupDistance = 430;
        private const string TeleportName = "global_ss_teleport_target_red.troy";
        private static GameObject _lastTeleportObject;
        private static float _lastTeleportTime;

        private static readonly Dictionary<AIHeroClient, BuffInstance> BlitzcrankSenders =
            new Dictionary<AIHeroClient, BuffInstance>() ;

        private static readonly Dictionary<AIHeroClient, Tuple<BuffInstance, Vector3>> ThreshSenders =
            new Dictionary<AIHeroClient, Tuple<BuffInstance, Vector3>>();

        private static bool _containsThresh;
        private static bool _containsBard;

        public static void Initialize()
        {            
            Game.OnUpdate += Game_OnUpdate;
            GameObject.OnCreate += GameObject_OnCreate;
            //GameObject.OnDelete += GameObject_OnDelete;
            Obj_AI_Base.OnBuffGain += Obj_AI_Base_OnBuffGain;
            Obj_AI_Base.OnBuffLose += Obj_AI_Base_OnBuffLose;
            _containsThresh = EntityManager.Heroes.Allies.Any(h => h.Hero == Champion.Thresh);
            _containsBard = EntityManager.Heroes.AllHeroes.Any(h => h.Hero == Champion.Bard);
            if (EntityManager.Heroes.Enemies.Any(x => x.Hero.Equals(Champion.TwistedFate)))
            {
                AddRTracker(Champion.TwistedFate, "GateMarker_red.troy", 1300); //1.5 seconds
            }
            if (EntityManager.Heroes.Enemies.Any(x => x.Hero.Equals(Champion.Pantheon)))
            {
                AddRTracker(Champion.Pantheon, "Pantheon_Base_R_indicator_red.troy", 2800); //3 seconds
            }
            if (EntityManager.Heroes.Enemies.Any(x => x.Hero.Equals(Champion.Ryze)))
            {
                AddRTracker(Champion.Ryze, "Ryze_Base_R_End_Enemy.troy", 1800); //2 seconds          
                AddRTracker(Champion.Ryze, "Ryze_Base_R_Start_Enemy.troy", 0); //2 seconds
            }
            if (EntityManager.Heroes.Enemies.Any(x => x.Hero.Equals(Champion.TahmKench)))
            {
                AddRTracker(Champion.TahmKench, "TahmKench_Base_R_Target_Enemy.troy", 3800); //4 seconds
            }
        }

        public static void AddRTracker(Champion hero, string rObjectName, int timeToWait)
        {
            if (EntityManager.Heroes.Enemies.Any(h => h.Hero == hero))
            {
                GameObject rObject = null;
                var rObjectTime = 0;
                //var allyCastedR = false;
                Game.OnTick += delegate
                {
                    if (W.IsReady())
                    {
                        if (rObject != null)
                        {
                            if (rObjectTime > 0 && Core.GameTickCount - rObjectTime >= timeToWait &&
                                Core.GameTickCount - rObjectTime <= 4000)
                            {
                                if (MenuValue.General.WCCStacks || Orbwalker.ActiveModes.Combo.IsOrb())
                                {
                                    W.Cast(rObject.Position);
                                }
                            }
                        }
                    }
                };
                //Obj_AI_Base.OnProcessSpellCast += delegate(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
                //{
                //    var senderHero = sender as AIHeroClient;
                //    if (senderHero != null && senderHero.IsAlly && senderHero.Hero == hero)
                //    {
                //        if (args.Slot == SpellSlot.R)
                //        {
                //            allyCastedR = true;
                //            Core.DelayAction(delegate { allyCastedR = false; }, 10000);
                //        }
                //    }
                //};
                GameObject.OnCreate += delegate(GameObject sender, EventArgs args)
                {
                    if (sender.Name.Equals(rObjectName)/* && !allyCastedR*/)
                    {
                        rObject = sender;
                        rObjectTime = Core.GameTickCount;
                        Core.DelayAction(delegate
                        {
                            rObject = null;
                            rObjectTime = 0;
                        }, 5200);
                    }
                };
                GameObject.OnDelete += delegate(GameObject sender, EventArgs args)
                {
                    if (rObject != null && rObject.IdEquals(sender))
                    {
                        rObject = null;
                        rObjectTime = 0;
                    }
                };
            }
        }

        public static float GetWTime()
        {
            return 1.5f;
        }

        private static float GetWTime(Obj_AI_Base target)
        {
            return 1.5f + player.Distance(target) / W.Speed;
        }


        private static void Game_OnUpdate(EventArgs args)
        {
            if (W.IsReady())
            {
                foreach (var b in BlitzcrankSenders.Where(b => !b.Key.IsValidTarget() || !b.Value.IsValid))
                {
                    BlitzcrankSenders.Remove(b.Key);
                }
                foreach (var b in ThreshSenders.Where(b => !b.Key.IsValidTarget() || !b.Value.Item1.IsValid))
                {
                    ThreshSenders.Remove(b.Key);
                }
                if (MenuValue.General.WCCStacks || Orbwalker.ActiveModes.Combo.IsOrb())
                {
                    foreach (
                        var enemy in
                            EntityManager.Heroes.Enemies.Where(x => x.IsValid && WillBeImmobile(x, GetWTime()) && !BlitzcrankSenders.ContainsKey(x) && !ThreshSenders.ContainsKey(x)))
                    {
                        if (_containsThresh)
                        {
                            if (!enemy.HasBuff(ThreshBuffName2))
                            {
                                W.Cast(enemy.Position);
                            }
                        }
                        else
                        {
                            W.Cast(enemy.Position);
                        }
                    }
                    foreach (var castPosition in from dic in BlitzcrankSenders let blitz = EntityManager.Heroes.Allies.FirstOrDefault(h => h.IsValidTarget() && h.ChampionName.Equals(dic.Value.SourceName)) where blitz != null select blitz.Position.Extend(dic.Key.Position, 50).To3DWorld())
                    {
                        W.Cast(castPosition);
                    }
                    foreach (var castPosition in from tuple in ThreshSenders let thresh = EntityManager.Heroes.Allies.FirstOrDefault(h => h.IsValidTarget() && h.ChampionName.Equals(tuple.Value.Item1.SourceName)) where thresh != null let startPosition = tuple.Value.Item2 select thresh.Position + (startPosition - thresh.Position).Normalized() * Math.Max(thresh.Distance(startPosition) - ThreshKnockupDistance, 1f))
                    {
                        W.Cast(castPosition);
                    }
                }
                if (MenuValue.General.WTeleport)
                {
                    if (_lastTeleportObject != null && _lastTeleportTime > 0 &&
                        Core.GameTickCount - _lastTeleportTime >= 2700 && Core.GameTickCount - _lastTeleportTime <= 4000)
                    //4 seconds
                    {
                        W.Cast(_lastTeleportObject.Position);
                    }
                }
                if (MenuValue.General.WCCStacks)
                {
                    foreach (var enemy in EntityManager.Heroes.Enemies.Where(h => h.IsValid && !h.IsDead && h.HasBuff(ZhonyasBuffName)))
                    {
                        var buff = enemy.GetBuff(ZhonyasBuffName);
                        if (buff.EndTime - Game.Time <= 0.2f && buff.EndTime - buff.StartTime > 0f)
                        {
                            W.Cast(enemy.Position);
                        }
                    }
                    if (_containsBard)
                    {
                        foreach (var enemy in EntityManager.Heroes.Enemies.Where(h => h.IsValid && !h.IsDead && h.HasBuff(BardRBuffName)))
                        {
                            var buff = enemy.GetBuff(BardRBuffName);
                            if (buff.EndTime - Game.Time <= 0.2f && buff.EndTime - buff.StartTime > 0f)
                            {
                                W.Cast(enemy.Position);
                            }
                        }
                    }
                    if (MenuValue.General.WRevive)
                    {
                        if (EntityManager.Heroes.Enemies.Any(x => x.Hero.Equals(Champion.Aatrox)))
                        {
                            foreach (var enemy in EntityManager.Heroes.Enemies.Where(h => h.IsValid && h.HasBuff(AatroxDeath)))
                            {
                                var buff = enemy.GetBuff(AatroxDeath);
                                if (buff.EndTime - Game.Time <= 0.2f && buff.EndTime - buff.StartTime > 0f)
                                {
                                    W.Cast(enemy.Position);
                                }
                            }
                        }
                        if (EntityManager.Heroes.Enemies.Any(x => x.Hero.Equals(Champion.Anivia)))
                        {
                            foreach (var enemy in EntityManager.Heroes.Enemies.Where(h => h.IsValid && h.HasBuff(AniviaRebirth)))
                            {
                                var buff = enemy.GetBuff(AniviaRebirth);
                                if (buff.EndTime - Game.Time <= 0.2f && buff.EndTime - buff.StartTime > 0f)
                                {
                                    W.Cast(enemy.Position);
                                }
                            }
                        }
                        if (EntityManager.Heroes.Enemies.Any(x => x.Hero.Equals(Champion.Fiora)))
                        {
                            foreach (var enemy in EntityManager.Heroes.Enemies.Where(h => h.IsValid && h.HasBuff(FioraW)))
                            {
                                var buff = enemy.GetBuff(FioraW);
                                if (buff.EndTime - Game.Time <= 0.2f && buff.EndTime - buff.StartTime > 0f)
                                {
                                    W.Cast(enemy.Position);
                                }
                            }
                        }
                        if (EntityManager.Heroes.Enemies.Any(x => x.Hero.Equals(Champion.Zilean)))
                        {
                            foreach (var enemy in EntityManager.Heroes.Enemies.Where(h => h.IsValid && h.HasBuff(ZileanBuffRevive)))
                            {
                                var buff = enemy.GetBuff(ZileanBuffRevive);
                                if (buff.EndTime - Game.Time <= 0.2f && buff.EndTime - buff.StartTime > 0f)
                                {
                                    W.Cast(enemy.Position);
                                }
                            }
                        }
                    }
                }
            }
        }

        private static void Obj_AI_Base_OnBuffGain(Obj_AI_Base sender, Obj_AI_BaseBuffGainEventArgs args)
        {
            var senderHero = sender as AIHeroClient;
            var casterHero = args.Buff.Caster as AIHeroClient;
            if (senderHero != null && senderHero.IsEnemy)
            {
                if (casterHero != null && casterHero.IsAlly && EntityManager.Heroes.Allies.Any(h => h.ChampionName.Equals(args.Buff.SourceName)))
                {
                    if (args.Buff.SourceName.Equals("Blitzcrank") && args.Buff.DisplayName.Equals(BlitzcrankBuffName))
                    {
                        if (BlitzcrankSenders.ContainsKey(senderHero))
                        {
                            BlitzcrankSenders.Remove(senderHero);
                        }
                        BlitzcrankSenders.Add(senderHero, args.Buff);
                        Core.DelayAction(delegate
                        {
                            if (BlitzcrankSenders.ContainsKey(senderHero))
                            {
                                BlitzcrankSenders.Remove(senderHero);
                            }
                        }, (int)(1000 * (args.Buff.EndTime - args.Buff.StartTime + 0.1f)));
                    }
                    else if (args.Buff.SourceName.Equals("Thresh") && args.Buff.DisplayName.Equals(ThreshBuffName))
                    {
                        if (ThreshSenders.ContainsKey(senderHero))
                        {
                            ThreshSenders.Remove(senderHero);
                        }
                        ThreshSenders.Add(senderHero, new Tuple<BuffInstance, Vector3>(args.Buff, senderHero.ServerPosition));
                        Core.DelayAction(delegate
                        {
                            if (ThreshSenders.ContainsKey(senderHero))
                            {
                                ThreshSenders.Remove(senderHero);
                            }
                        }, (int)(1000 * (args.Buff.EndTime - args.Buff.StartTime + 0.1f)));
                    }
                }               
            }
        }

        private static void Obj_AI_Base_OnBuffLose(Obj_AI_Base sender, Obj_AI_BaseBuffLoseEventArgs args)
        {
            var senderHero = sender as AIHeroClient;
            var casterHero = args.Buff.Caster as AIHeroClient;
            if (senderHero != null && senderHero.IsEnemy && casterHero != null && casterHero.IsAlly)
            {
                if (args.Buff.SourceName.Equals("Blitzcrank") && args.Buff.DisplayName.Equals(BlitzcrankBuffName))
                {
                    if (BlitzcrankSenders.ContainsKey(senderHero))
                    {
                        BlitzcrankSenders.Remove(senderHero);
                    }
                }
                else if (args.Buff.SourceName.Equals("Thresh") && args.Buff.DisplayName.Equals(ThreshBuffName))
                {
                    if (ThreshSenders.ContainsKey(senderHero))
                    {
                        ThreshSenders.Remove(senderHero);
                    }
                }
                else if (args.Buff.Name.Equals(AngleRevive) && MenuValue.General.WRevive)
                {
                    W.Cast(senderHero);
                }
            }
        }
        private static void GameObject_OnCreate(GameObject sender, EventArgs args)
        {
            if (sender.Name.Equals(TeleportName))
            {
                _lastTeleportObject = sender;
                _lastTeleportTime = Core.GameTickCount;
                Core.DelayAction(delegate
                {
                    _lastTeleportObject = null;
                    _lastTeleportTime = 0;
                }, 5200);
            }
        }

        //private static void GameObject_OnDelete(GameObject sender, EventArgs args)
        //{
        //    if (sender.IdEquals(_lastTeleportObject))
        //    {
        //        //_lastTeleportObject = null;
        //        //_lastTeleportTime = 0;
        //    }
        //}
        public static bool WillBeImmobile(Obj_AI_Base target, float time)
        {
            var buffDuration = target.GetMovementBlockedDebuffDuration();
            return buffDuration > 0 && time <= buffDuration + (Width + target.BoundingRadius) / target.MoveSpeed;
        }
    }
}

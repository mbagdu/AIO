using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using System;
using UBAddons.Libs.Base;
using UBAddons.Log;
using UBAddons.General;

namespace UBAddons.Libs
{
    public abstract class ChampionPlugin : IHeroBase
    {
        private static int LastTick, Delay, LastTickdelay;
        /// <summary>
        /// Creat the Menu
        /// </summary>
        void IHeroBase.CreateMenu()
        {
            CreateMenu();
        }

        /// <summary>
        /// Put Drawing here
        /// </summary>
        /// <param name="args">EventArgs</param>
        void IHeroBase.OnDraw(EventArgs args)
        {
            try
            {
                OnDraw(args);
            }
            catch (Exception e)
            {
                Debug.Print(e.ToString(), Console_Message.Error);
            }
        }

        /// <summary>
        /// On Tick Action
        /// </summary>
        /// <param name="args">EventArgs</param>
        void IHeroBase.OnTick(EventArgs args)
        {
            if (Core.GameTickCount - LastTick >= 1000)
            {
                LastTick = Core.GameTickCount;
            }
            Delay += Core.GameTickCount - LastTickdelay;
            LastTickdelay = Core.GameTickCount;

            if (!CheckFps())
            {
                return;
            }
        
            PermaActive();

            if (Orbwalker.ActiveModes.Combo.IsOrb())
            {
                Combo();
            }
            if (Orbwalker.ActiveModes.Harass.IsOrb() && !Orbwalker.ActiveModes.Flee.IsOrb())
            {
                Harass();
            }
            if (Orbwalker.ActiveModes.LaneClear.IsOrb())
            {
                LaneClear();
            }
            if (Orbwalker.ActiveModes.JungleClear.IsOrb())
            {
                JungleClear();
            }
            if (Orbwalker.ActiveModes.LastHit.IsOrb())
            {
                LastHit();
            }
            if (Orbwalker.ActiveModes.Flee.IsOrb())
            {
                Flee();
            }
        }

        /// <summary>
        /// GapCloser
        /// </summary>
        /// <param name="sender"> Who gapcloser</param>
        /// <param name="args">EventArgs</param>
        void IHeroBase.OnGapcloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs args)
        {
            try
            {
                OnGapcloser(sender, args);
            }
            catch (Exception e)
            {
                Debug.Print(e.ToString(), Console_Message.Error);
            }
        }

        /// <summary>
        /// Interrupter
        /// </summary>
        /// <param name="sender">Who need Interrupter</param>
        /// <param name="args">EventArgs</param>
        void IHeroBase.OnInterruptable(Obj_AI_Base sender, Interrupter.InterruptableSpellEventArgs args)
        {
            try
            {
                OnInterruptable(sender, args);
            }
            catch (Exception e)
            {
                Debug.Print(e.ToString(), Console_Message.Error);
            }
        }

        /// <summary>
        /// Recall feature
        /// </summary>
        /// <param name="sender">Who is recalling/Teleporting</param>
        /// <param name="args">EventArgs</param>
        void IHeroBase.OnTeleport(Obj_AI_Base sender, Teleport.TeleportEventArgs args)
        {
            try
            {
                OnTeleport(sender, args);
            }
            catch (Exception e)
            {
                Debug.Print(e.ToString(), Console_Message.Error);
            }
        }

        /// <summary>
        /// Calcalute Unkillable Minion
        /// </summary>
        /// <param name="target">Wut minion</param>
        /// <param name="args">EventArgs</param>
        void IHeroBase.OnUnkillableMinion(Obj_AI_Base target, Orbwalker.UnkillableMinionArgs args)
        {
            try
            {
                OnUnkillableMinion(target, args);
            }
            catch (Exception e)
            {
                Debug.Print(e.ToString(), Console_Message.Error);
            }
        }

        /// <summary>
        /// Allow Draw
        /// </summary>
        bool IHeroBase.EnableDraw
        {
            get { return EnableDraw; }
        }

        /// <summary>
        /// Allow Damage Indicator
        /// </summary>
        bool IHeroBase.EnableDamageIndicator
        {
            get { return EnableDamageIndicator; }
        }

        protected abstract void CreateMenu();
        protected abstract void Combo();
        protected abstract void PermaActive();
        protected abstract void Harass();
        protected abstract void Flee();
        protected abstract void LaneClear();
        protected abstract void JungleClear();
        protected abstract void LastHit();
        protected abstract void OnDraw(EventArgs args);
        protected abstract void OnGapcloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs args);
        protected abstract void OnInterruptable(Obj_AI_Base sender, Interrupter.InterruptableSpellEventArgs args);
        protected abstract void OnTeleport(Obj_AI_Base sender, Teleport.TeleportEventArgs args);
        protected abstract void OnUnkillableMinion(Obj_AI_Base target, Orbwalker.UnkillableMinionArgs args);
        protected abstract bool EnableDraw { get; }
        protected abstract bool EnableDamageIndicator { get; }
        protected abstract bool IsAutoHarass { get; }

        public bool CheckFps()
        {
            if (!UBCore.CoreMenu.Core.VChecked("Core.Enable.FPS"))
            {
                return true;
            }
            var rate = UBCore.CoreMenu.Core.VSliderValue("Core.Calculate");
            if (Game.FPS < UBCore.CoreMenu.Core.VSliderValue("Core.Min.FPS"))
            {
                rate = Math.Min(10, UBCore.CoreMenu.Core.VSliderValue("Core.Calculate"));
            }
            if (Delay > 1000f / rate)
            {
                Delay = 0;
                return true;
            }
            return false;
        }

    }
}

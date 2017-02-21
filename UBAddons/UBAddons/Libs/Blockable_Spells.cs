using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Constants;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBAddons.General;

namespace UBAddons.Libs
{
    static class BlockableSpells
    {
        public static readonly HashSet<BlockableSpellData> BlockableSpellsHashSet = new HashSet<BlockableSpellData>
        {
            #region Ahri 
            new BlockableSpellData(Champion.Ahri, "[W] Fox-Fire", SpellSlot.W)
            {
                HasMissile = true,
            },
            new BlockableSpellData(Champion.Ahri, "[R] Spirit Rush", SpellSlot.R)
            {
                HasMissile = true,
            },
            #endregion

            #region Akali
            new BlockableSpellData(Champion.Akali, "[Q] Mark of the Assassin", SpellSlot.Q)
            {
                IsShield = true,
                HasMissile = true,
            },
            new BlockableSpellData(Champion.Akali, "[E] Crescent Slash", SpellSlot.E)
            {
                NeedsAdditionalLogics = true,
            },
            new BlockableSpellData(Champion.Akali, "[R] Shadow Dance", SpellSlot.R, true)
            {
                Gapclose = true,
            },
            #endregion
            
            #region Alistar
            new BlockableSpellData(Champion.Alistar, "[Q] Pulverize", SpellSlot.Q)
            {
                NeedsAdditionalLogics = true,
            },
            new BlockableSpellData(Champion.Alistar, "[W] Headbutt", SpellSlot.W)
            {
                Gapclose = true,
            },
            new BlockableSpellData(Champion.Alistar, "[E] Trample", SpellSlot.E)
            {
                NeedsAdditionalLogics = true,
                AdditionalBuffName = "alistareattack",
                IsShield = true,
            },
            #endregion

            #region Amumu
            new BlockableSpellData(Champion.Amumu, "[E] Tantrum", SpellSlot.E)
            {
                NeedsAdditionalLogics = true,
            },
            new BlockableSpellData(Champion.Amumu, "[R] Curse of the Sad Mummy", SpellSlot.R)
            {
                NeedsAdditionalLogics = true,
                IsShield = true,
                IsZhonya = true,
            },
            #endregion

            #region Anivia
            new BlockableSpellData(Champion.Anivia, "[E] Frostbite", SpellSlot.E)
            {
                HasMissile = true,
            },
            #endregion

            #region Annie
            new BlockableSpellData(Champion.Annie, "[Q] Disintegrate", SpellSlot.Q)
            {
                HasMissile = true,
            },
            new BlockableSpellData(Champion.Annie, "[W] Incinerate", SpellSlot.W),
            new BlockableSpellData(Champion.Annie, "[R] Summon Tibbers", SpellSlot.R, true),
            #endregion

            #region Azir
            new BlockableSpellData(Champion.Azir, "[R] Emperor's Divide", SpellSlot.R, true)
            {
                NeedsAdditionalLogics = true,
            },
            #endregion

            #region Bard
            new BlockableSpellData(Champion.Bard, "[R] Tempered Fate", SpellSlot.R)
            {
                NeedsAdditionalLogics = true,
                HasMissile = true,
            },
            #endregion

            #region Blitzcrank
            new BlockableSpellData(Champion.Blitzcrank, "[Q] Rocket Grab", SpellSlot.Q)
            {
                NeedsAdditionalLogics = true,
                HasMissile = true,
                IsShield = true,
            },
            new BlockableSpellData(Champion.Blitzcrank, "[E] Power Fist", SpellSlot.E)
            {
                NeedsAdditionalLogics = true,
                AdditionalBuffName = "powerfistattack",
            },
            new BlockableSpellData(Champion.Blitzcrank, "[R] Static Field", SpellSlot.R)
            {
                NeedsAdditionalLogics = true,
                HasMissile = true,
                IsShield = true,
            },
            #endregion

            #region Brand
            new BlockableSpellData(Champion.Brand, "[E] Conflagration", SpellSlot.E),
            new BlockableSpellData(Champion.Brand, "[R] Pyroclasm", SpellSlot.R, true)
            {
                HasMissile = true,
            },
            #endregion

            #region Braum
            new BlockableSpellData(Champion.Braum, "[Passive] Concussive Blows", SpellSlot.Unknown)
            {
                    NeedsAdditionalLogics = true,
                    AdditionalBuffName = "braumbasicattackpassiveoverride",
                    IsShield = true,
            },
            #endregion

            #region Caitlyn
            new BlockableSpellData(Champion.Caitlyn, "[R] Ace in the Hole", SpellSlot.R)
            {
                NeedsAdditionalLogics = true,
                HasMissile = true,
                IsShield = true,
                IsZhonya = true,
            },
            #endregion

            #warning Add Cammile

            #region Cassiopeia
            new BlockableSpellData(Champion.Cassiopeia, "[E] Twin Fang", SpellSlot.E)
            {
                NeedsAdditionalLogics = true,
                HasMissile = true,
                IsShield = true,
            },
            new BlockableSpellData(Champion.Cassiopeia, "[R] Petrifying Gaze", SpellSlot.R, true)
            {
                NeedsAdditionalLogics = true,
            },
            #endregion

            #region Chogath
            new BlockableSpellData(Champion.Chogath, "[R] Feast", SpellSlot.R)
            {
                IsShield = true,
                IsZhonya = true,
            },
            #endregion

            #region Darius
            new BlockableSpellData(Champion.Darius, "[E] Apprehend", SpellSlot.E),
            new BlockableSpellData(Champion.Darius, "[R] Noxian Guillotine", SpellSlot.R, true),
            #endregion

            #region Diana
            new BlockableSpellData(Champion.Diana, "[E] Moonfall", SpellSlot.E)
            {
                NeedsAdditionalLogics = true,
                IsShield = true,
            },
            new BlockableSpellData(Champion.Diana, "[R] Lunar Rush", SpellSlot.R, true),
            #endregion

            #region Ekko
            new BlockableSpellData(Champion.Ekko, "[E] Phase Dive", SpellSlot.E)
            {
                NeedsAdditionalLogics = true,
                AdditionalBuffName = "ekkoeattack",
            },
            #endregion

            #region Evelynn
            new BlockableSpellData(Champion.Evelynn, "[E] Ravage", SpellSlot.E)
            {
                NeedsAdditionalLogics = true,
                IsShield = true,
            },
            new BlockableSpellData(Champion.Evelynn, "[R] Agony's Embrace", SpellSlot.R, true)
            {
                NeedsAdditionalLogics = true,
            },
            #endregion

            #region FiddleSticks
            new BlockableSpellData(Champion.FiddleSticks, "[Q] Terrify", SpellSlot.Q)
            {
                IsShield = true,
            },
            new BlockableSpellData(Champion.FiddleSticks, "[E] Dark Wind", SpellSlot.E),
            #endregion

            #region Fiora
            new BlockableSpellData(Champion.Fiora, "[R] Grand Challenge", SpellSlot.R, true),
            #endregion

            #region Fizz
            new BlockableSpellData(Champion.Fizz, "[Q] Urchin Strike", SpellSlot.Q, true)
            {
                Gapclose = true,
            },
            #endregion

            #region Galio
            new BlockableSpellData(Champion.Galio, "[R] Idol of Durand", SpellSlot.R, true),
            #endregion

            #region Gangplank
            new BlockableSpellData(Champion.Gangplank, "[Q] Parrrley", SpellSlot.Q)
            {
                HasMissile = true,
            },
            #endregion

            #region Garen
            new BlockableSpellData(Champion.Garen, "[Q] Decisive Strike", SpellSlot.Q)
            {
                NeedsAdditionalLogics = true,
                AdditionalBuffName = "garenqattack"
            },
            new BlockableSpellData(Champion.Garen, "[R] Demacian Justice", SpellSlot.R, true),
            #endregion

            #region Gnar
            new BlockableSpellData(Champion.Gnar, "[R] GNAR!", SpellSlot.R, true),
            #endregion

            #region Gragas
            new BlockableSpellData(Champion.Gragas, "[W] Drunken Rage", SpellSlot.W)
            {
                NeedsAdditionalLogics = true,
                AdditionalBuffName = "drunkenrage"
            },
            #endregion

            #region Graves
            new BlockableSpellData(Champion.Graves, "[R] Collateral Damage", SpellSlot.R, true)
            {
                NeedsAdditionalLogics = true,
                HasMissile = true,
            },
            #endregion

            #region Hecarim
            new BlockableSpellData(Champion.Hecarim, "[E] Devastating Charge", SpellSlot.E)
            {
                NeedsAdditionalLogics = true,
                AdditionalBuffName = "hecarimrampattack"
            },
            new BlockableSpellData(Champion.Hecarim, "[R] Onslaught of Shadows", SpellSlot.R, true)
            {
                NeedsAdditionalLogics = true,
                HasMissile = true,
            },
            #endregion

            #region Illaoi
            new BlockableSpellData(Champion.Illaoi, "[W] Harsh Lesson", SpellSlot.W)
            {
                NeedsAdditionalLogics = true,
                AdditionalBuffName = "illaoiwattack"
            },
            #endregion

            #region Irelia
            new BlockableSpellData(Champion.Irelia, "[Q] Bladesurge", SpellSlot.Q)
            {
                Gapclose = true,
            },
            new BlockableSpellData(Champion.Irelia, "[E] Equilibrium Strike", SpellSlot.E),
            #endregion

            #region Janna
            new BlockableSpellData(Champion.Janna, "[W] Zephyr", SpellSlot.W)
            {
                HasMissile = true,
            },
            new BlockableSpellData(Champion.Janna, "[R] Monsoon", SpellSlot.R)
            {
                NeedsAdditionalLogics = true
            },
            #endregion

            #region JarvaniV
            new BlockableSpellData(Champion.JarvanIV, "[E-Q] Demacian Standard => Dragon Strike combo", SpellSlot.Q)
            {
                Gapclose = true,
                NeedsAdditionalLogics = true,
            },
            new BlockableSpellData(Champion.JarvanIV, "[R] Cataclysm", SpellSlot.R, true),
            #endregion

            #region Jax
            new BlockableSpellData(Champion.Jax, "[Q] Leap Strike", SpellSlot.Q)
            {
                Gapclose = true,
            },
            new BlockableSpellData(Champion.Jax, "[W] Empower", SpellSlot.W)
            {
                NeedsAdditionalLogics = true,
                AdditionalBuffName = "jaxempowertwo",
            },
            #endregion

            #region Jayce
            new BlockableSpellData(Champion.Jayce, "[Q] To The Skies!", SpellSlot.Q),
            new BlockableSpellData(Champion.Jayce, "[E] Thundering Blow", SpellSlot.E)
            {
                Gapclose = true,
            },
            #endregion

            #region Jhin
            new BlockableSpellData(Champion.Jhin, "[Passive] 4th auto attack", SpellSlot.Unknown)
            {
                IsSpellShield = false,
                IsShield = true,
                IsExhaust = true,
                IsZhonya = true,
                HasMissile = true,
                NeedsAdditionalLogics = true,
                AdditionalBuffName = "jhinpassiveattack"
            },
            new BlockableSpellData(Champion.Jhin, "[Q] Dancing Grenade", SpellSlot.Q)
            {
                HasMissile = true,
            },
            #endregion

            #region Kalista
            new BlockableSpellData(Champion.Kalista, "[E] Rend", SpellSlot.E)
            {
                NeedsAdditionalLogics = true,
                AdditionalBuffName = "kalistaexpungemarker"
            },
            #endregion

            #region Karma
            new BlockableSpellData(Champion.Karma, "[W] Focused Resolve", SpellSlot.W)
            {
                NeedsAdditionalLogics = true,
                AdditionalDelay = 1800,
            },
            #endregion

            #region Karthus
            new BlockableSpellData(Champion.Karthus, "[R] Requiem", SpellSlot.R, true)
            {
                NeedsAdditionalLogics = true,
                AdditionalDelay = 2800
            },
            #endregion

            #region Kassadin
            new BlockableSpellData(Champion.Kassadin, "[Q] Null Sphere", SpellSlot.Q)
            {
                HasMissile = true,
            },
            new BlockableSpellData(Champion.Kassadin, "[W] Nether Blade", SpellSlot.W)
            {
                NeedsAdditionalLogics = true,
                AdditionalBuffName = "netherblade"
            },
            #endregion

            #region Katarina
            new BlockableSpellData(Champion.Katarina, "[Q] Bouncing Blade", SpellSlot.Q)
            {
                HasMissile = true,
            },
            #endregion

            #region Kayle
            new BlockableSpellData(Champion.Kayle, "[Q] Reckoning", SpellSlot.Q)
            {
                HasMissile = true,
            },
            #endregion

            #region Kennen
            new BlockableSpellData(Champion.Kennen, "[Passive] Mark of the Storm", SpellSlot.Unknown)
            {
                NeedsAdditionalLogics = true,
                AdditionalBuffName = "kennenmegaproc"
            },
            new BlockableSpellData(Champion.Kennen, "[W] Electrical Surge", SpellSlot.W),
            new BlockableSpellData(Champion.Kennen, "[R] Slicing Maelstrom", SpellSlot.R, true),
            #endregion

            #region Khazik
            new BlockableSpellData(Champion.Khazix, "[Q] Taste Their Fear", SpellSlot.Q)
            {
                IsExhaust = true,
                IsShield = true,
            },
            #endregion

            #region Kindred
            new BlockableSpellData(Champion.Kindred, "[E] Mounting Dread", SpellSlot.E)
            {
                HasMissile = true,
            },
            #endregion

            #region Kled
            new BlockableSpellData(Champion.Kled, "[Q] Beartrap on a Rope", SpellSlot.Q)
            {
                NeedsAdditionalLogics = true,
                AdditionalBuffName = "kledqmark",
            },
            #endregion

            #region Kogmaw
            new BlockableSpellData(Champion.KogMaw, "[Passive] Icathian Surprise", SpellSlot.Unknown)
            {
                NeedsAdditionalLogics = true,
                AdditionalDelay = 3800
            },
            #endregion

            //new BlockableSpellData(Champion.Leblanc, "[R] Mimic", SpellSlot.R),
            #warning Add leblanc

            #region Leesin
            new BlockableSpellData(Champion.LeeSin, "[R] Dragon's Rage", SpellSlot.R, true),
            #endregion

            #region Leona
            new BlockableSpellData(Champion.Leona, "[Q] Shield of Daybreak", SpellSlot.Q)
            {
                NeedsAdditionalLogics = true,
                AdditionalBuffName = "leonashieldofdaybreakattack"
            },
            #endregion

            #region Lissandra
            new BlockableSpellData(Champion.Lissandra, "[W] Ring of Frost", SpellSlot.W)
            {
                NeedsAdditionalLogics = true
            },
            new BlockableSpellData(Champion.Lissandra, "[R] Frozen Tomb", SpellSlot.R, true),
            #endregion

            #region Lucian
            new BlockableSpellData(Champion.Lucian, "[Q] Piercing Light", SpellSlot.Q),
            #endregion

            #region Lulu
            new BlockableSpellData(Champion.Lulu, "[W] Whimsy (polymorph)", SpellSlot.W),
            new BlockableSpellData(Champion.Lulu, "[E] Help, Pix!", SpellSlot.E),
            #endregion

            #region Malphite
            new BlockableSpellData(Champion.Malphite, "[Q] Seismic Shard", SpellSlot.Q)
            {
                HasMissile = true,
            },
            new BlockableSpellData(Champion.Malphite, "[R] Unstoppable Force", SpellSlot.R)
            {
                NeedsAdditionalLogics = true
            },
            #endregion

            #region Malzahar
            new BlockableSpellData(Champion.Malzahar, "[R] Malefic Visions", SpellSlot.R),
            #endregion

            #region Maokai
            new BlockableSpellData(Champion.Maokai, "[W] Twisted Advance", SpellSlot.W)
            {
                Gapclose = true,
            },
            #endregion

            #region Master Yi
            new BlockableSpellData(Champion.MasterYi, "[Q] Alpha Strike", SpellSlot.Q)
            {
                Gapclose = true,
            },
            //new BlockableSpellData(Champion.MasterYi, "[Q] Alpha Strike (Has R buff)", SpellSlot.Q, true)
            //{
            //    NeedsAdditionalLogics = true,
            //    AdditionalBuffName = "highlander",
            //    Gapclose = true,
            //},
            #endregion

            #region Miss Fortune
            new BlockableSpellData(Champion.MissFortune, "[Q] Double Up", SpellSlot.Q)
            {
                HasMissile = true,
            },
            #endregion

            #region Mordekaiser
            new BlockableSpellData(Champion.Mordekaiser, "[Q] => 1st attack ", SpellSlot.Q)
            {
                NeedsAdditionalLogics = true,
                AdditionalBuffName = "mordekaiserqattack"
            },
            new BlockableSpellData(Champion.Mordekaiser, "[Q] => 2nd attack ", SpellSlot.Q)
            {
                NeedsAdditionalLogics = true,
                AdditionalBuffName = "mordekaiserqattack1"
            },
            new BlockableSpellData(Champion.Mordekaiser, "[Q] => 3rd attack ", SpellSlot.Q)
            {
                NeedsAdditionalLogics = true,
                AdditionalBuffName = "mordekaiserqattack2",
                IsShield = true,
            },
            new BlockableSpellData(Champion.Mordekaiser, "[R] Children of the Grave", SpellSlot.R),
            #endregion

            #region Morgana
            new BlockableSpellData(Champion.Morgana, "[R] Soul Shackles", SpellSlot.R)
            {
                NeedsAdditionalLogics = true,
                AdditionalBuffName = "soulshackles"
            },
            #endregion

            #region Nautilus
            new BlockableSpellData(Champion.Nautilus, "[R] Depth Charge", SpellSlot.R)
            {
                HasMissile = true,
                NeedsAdditionalLogics = true
            },
            #endregion

            #region Nasus
            new BlockableSpellData(Champion.Nasus, "[Q] Siphoning Strike", SpellSlot.Q)
            {
                NeedsAdditionalLogics = true,
                AdditionalBuffName = "nasusqattack"
            },
            new BlockableSpellData(Champion.Nasus, "[W] Wither", SpellSlot.W),
            #endregion

            #region Nami
            new BlockableSpellData(Champion.Nami, "[W] Ebb and Flow", SpellSlot.W)
            {
                HasMissile = true,
            },
            #endregion

            #region Nidalee
            new BlockableSpellData(Champion.Nidalee, "[Q] Takedown", SpellSlot.Q)
            {
                NeedsAdditionalLogics = true,
                AdditionalBuffName = "nidaleetakedownattack"
            },
            #endregion

            #region Nocturne
            new BlockableSpellData(Champion.Nocturne, "[E] Unspeakable Horror", SpellSlot.E)
            {
                NeedsAdditionalLogics = true,
                AdditionalBuffName = "nocturneunspeakablehorror"
            },
            #endregion

            #region Nunu
            new BlockableSpellData(Champion.Nunu, "[E] Ice Blast", SpellSlot.E)
            {
                HasMissile = true,
            },
            #endregion

            #region Olaf
            new BlockableSpellData(Champion.Olaf, "[E] Reckless Swing", SpellSlot.E),
            #endregion

            #region Pantheon
            new BlockableSpellData(Champion.Pantheon, "[W] Aegis of Zeonia", SpellSlot.W)
            {
                Gapclose = true,
            },
            #endregion

            #region Poppy
            new BlockableSpellData(Champion.Poppy, "[E] Heroic Charge", SpellSlot.E)
            {
                Gapclose = true,
            },
            #endregion

            #region Quinn
            new BlockableSpellData(Champion.Quinn, "[E] Vault", SpellSlot.E)
            {
                Gapclose = true,
            },
            #endregion

            #region Rammus
            new BlockableSpellData(Champion.Rammus, "[E] Puncturing Taunt", SpellSlot.E),
            #endregion

            #region Renekton
            new BlockableSpellData(Champion.Renekton, "[W] Cull the Meek", SpellSlot.W)
            {
                NeedsAdditionalLogics = true,
                AdditionalBuffName = "renektonexecute"
            },
            new BlockableSpellData(Champion.Renekton, "[Empowered W] Cull the Meek", SpellSlot.W)
            {
                NeedsAdditionalLogics = true,
                AdditionalBuffName = "renektonsuperexecute"
            },
            #endregion

            #region Ryze
            new BlockableSpellData(Champion.Ryze, "[W] Rune Prison", SpellSlot.W),
            #endregion

            #region Sejuani
            new BlockableSpellData(Champion.Sejuani, "[E] Flail of the Northern Winds", SpellSlot.E)
            {
                NeedsAdditionalLogics = true,
                AdditionalBuffName = "sejuanifrost"
            },
            #endregion

            #region Shaco
            new BlockableSpellData(Champion.Shaco, "[E] Two-Shiv Poison", SpellSlot.E)
            {
                HasMissile = true,
            },
            #endregion

            #region Shyvana
            new BlockableSpellData(Champion.Shyvana, "[Q] Twin Bite", SpellSlot.Q)
            {
                NeedsAdditionalLogics = true,
                AdditionalBuffName = "shyvanadoubleattackhit"
            },
            #endregion

            #region Signed
            new BlockableSpellData(Champion.Singed, "[E] Fling", SpellSlot.E),
            #endregion

            #region Skarner
            new BlockableSpellData(Champion.Skarner, "[E] Fracture => Empowered auto attack", SpellSlot.E)
            {
                NeedsAdditionalLogics = true,
                AdditionalBuffName = "skarnerpassiveattack"
            },
            new BlockableSpellData(Champion.Skarner, "[R] Impale", SpellSlot.R, true),
            #endregion

            #region Sona
            new BlockableSpellData(Champion.Sona, "[Q] Hymn of Valor", SpellSlot.Q)
            {
                NeedsAdditionalLogics = true,
                HasMissile = true,
            },
            #endregion

            #region Syndra
            new BlockableSpellData(Champion.Syndra, "[R] Unleashed Power", SpellSlot.R, true)
            {
                HasMissile = true,
            },
            #endregion

            #region Tahm Kench
            new BlockableSpellData(Champion.TahmKench, "[W] Devour", SpellSlot.W),
            #endregion

            #region Talon
            new BlockableSpellData(Champion.Talon, "[Q] Noxian Diplomacy", SpellSlot.Q)
            {
                Gapclose = true,
            },
            #endregion

            #region Teemo
            new BlockableSpellData(Champion.Teemo, "[Q] Blinding Dart", SpellSlot.Q)
            {
                HasMissile = true,
            },
            #endregion

            #region Tristana
            new BlockableSpellData(Champion.Tristana, "[E] Explosive Charge", SpellSlot.E)
            {
                HasMissile = true
            },
            new BlockableSpellData(Champion.Tristana, "[R] Buster Shot", SpellSlot.R)
            {
                HasMissile = true,
                IsShield = true,
                IsZhonya = true,
            },
            #endregion

            #region Trundle
            new BlockableSpellData(Champion.Trundle, "[Q] Chomp", SpellSlot.Q)
            {
                NeedsAdditionalLogics = true,
                AdditionalBuffName = "trundleq"
            },
            new BlockableSpellData(Champion.Trundle, "[R] Subjugate", SpellSlot.R),
            #endregion

            #region Trundle
            new BlockableSpellData(Champion.TwistedFate, "[W] Gold Card", SpellSlot.W)
            {
                NeedsAdditionalLogics = true,
                AdditionalBuffName = "goldcardpreattack",
                HasMissile = true,
            },
            new BlockableSpellData(Champion.TwistedFate, "[W] Red Card", SpellSlot.W)
            {
                NeedsAdditionalLogics = true,
                AdditionalBuffName = "redcardpreattack",
                HasMissile = true,

            },
            #endregion

            #region Twitch
            new BlockableSpellData(Champion.Twitch, "[E] Contaminate", SpellSlot.E),
            #endregion

            #region Udyr
            new BlockableSpellData(Champion.Udyr, "[E] Bear Stance", SpellSlot.E)
            {
                NeedsAdditionalLogics = true,
                AdditionalBuffName = "udyrbearattack",
                IsShield = true,
            },
            #endregion

            #region Urgot
            new BlockableSpellData(Champion.Urgot, "[R] Hyper-Kinetic Position Reverser", SpellSlot.R, true),
            #endregion

            #region Vayne
            new BlockableSpellData(Champion.Vayne, "[E] Condemn", SpellSlot.E)
            {
                HasMissile = true,
            },
            #endregion

            #region Veigar
            new BlockableSpellData(Champion.Veigar, "[R] Primordial Burst", SpellSlot.R, true)
            {
                HasMissile = true,
            },
            #endregion

            #region Vi
            new BlockableSpellData(Champion.Vi, "[R] Assault and Battery", SpellSlot.R)
            {
                NeedsAdditionalLogics = true,
                Gapclose = true,
            },
            #endregion

            #region Viktor
            new BlockableSpellData(Champion.Viktor, "[Q] Siphon Power", SpellSlot.Q)
            {
                HasMissile = true,
            },
            new BlockableSpellData(Champion.Viktor, "[Q] Siphon Power => Empowered auto attack", SpellSlot.Q)
            {
                NeedsAdditionalLogics = true,
                AdditionalBuffName = "viktorqbuff"
            },
            #endregion

            #region Vladimir
            new BlockableSpellData(Champion.Vladimir, "[Q] Transfusion", SpellSlot.Q),
            #endregion

            #region Volibear
            new BlockableSpellData(Champion.Volibear, "[Q] Rolling Thunder", SpellSlot.Q)
            {
                NeedsAdditionalLogics = true,
                AdditionalBuffName = "volibearqattack"
            },
            new BlockableSpellData(Champion.Volibear, "[W] Frenzy", SpellSlot.W)
            {
                IsShield = true,
            },
            #endregion

            #region Xin Thông
            new BlockableSpellData(Champion.XinZhao, "[Q] Three Xinzhao Strike", SpellSlot.Q)
            {
                NeedsAdditionalLogics = true,
                AdditionalBuffName = "xenzhaothrust3"
            },
            new BlockableSpellData(Champion.XinZhao, "[R] Crescent Sweep", SpellSlot.R),
            #endregion

            #region Wukong
            new BlockableSpellData(Champion.MonkeyKing, "[Q] Crushing Blow", SpellSlot.Q)
            {
                NeedsAdditionalLogics = true,
                AdditionalBuffName = "monkeykingqattack"
            },
            new BlockableSpellData(Champion.MonkeyKing, "[E] Nimbus Strike", SpellSlot.E)
            {
                Gapclose = true,
            },
            #endregion

            #region Warwick
            new BlockableSpellData(Champion.Warwick, "[Q] Hungering Strike", SpellSlot.Q)
            {
                Gapclose = true,
            },
            #endregion

            #region Daxua
            new BlockableSpellData(Champion.Yasuo, "[E-Q] Sweeping Blade => Steel Tempest combo", SpellSlot.Q, true)
            {
                NeedsAdditionalLogics = true,
                AdditionalBuffName = "yasuoq3w",
            },
            #endregion

            #region Yorick
            new BlockableSpellData(Champion.Yorick, "[Q] Last Rites", SpellSlot.Q)
            {
                NeedsAdditionalLogics = true,
                AdditionalBuffName = "yorickqattack"
            },
            #endregion

            #region Zed
            new BlockableSpellData(Champion.Zed, "[R] Death Mark", SpellSlot.R)
            {
                NeedsAdditionalLogics = true,
                AdditionalDelay = 1500,
            },
            #endregion

            #region Zilean
            new BlockableSpellData(Champion.Zilean, "[Q] Time Bomb", SpellSlot.Q)
            {
                AdditionalBuffName = "ZileanQEnemyBomb",
            },
            new BlockableSpellData(Champion.Zilean, "[E] Time Warp", SpellSlot.E)
            #endregion
        };

        public delegate void OnBlockableSpellEvent(AIHeroClient sender, OnBlockableSpellEventArgs args);

        public static event OnBlockableSpellEvent OnBlockableSpell;
        public static void Initialize()
        {
            BlockableSpellsHashSet.RemoveWhere(x => EntityManager.Heroes.Enemies.All(k => k.Hero != x.ChampionName));
            if (BlockableSpellsHashSet.Count <= 0)
                return;

            Obj_AI_Base.OnProcessSpellCast += Obj_AI_Base_OnProcessSpellCast;
            Game.OnTick += Game_OnTick;
            Obj_AI_Base.OnBasicAttack += Obj_AI_Base_OnBasicAttack;
        }

        private static BlockableSpellData Getdata(Champion champion, SpellSlot spellslot)
        {
            return BlockableSpellsHashSet.FirstOrDefault(x => x.ChampionName.Equals(champion) && x.SpellSlot.Equals(spellslot));
        }
        private static void Invoke(AIHeroClient sender, SpellSlot spellSlot, string additionalName, bool isAutoAttack, int additionalDelay)
        {
            OnBlockableSpell?.Invoke(sender, new OnBlockableSpellEventArgs(sender.Hero, spellSlot, isAutoAttack, additionalDelay));
        }

        private static void Obj_AI_Base_OnBasicAttack(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            var enemy = sender as AIHeroClient;

            if (enemy == null || args.Target == null || !args.Target.IsMe)
                return;

            if (enemy.Hero == Champion.Tristana)
            {
                var buff = Player.Instance.Buffs.FirstOrDefault(x => x.Name.Equals("tristanaecharge", StringComparison.CurrentCultureIgnoreCase));

                if (buff != null && buff.Count >= 3)
                {
                    Invoke(enemy, SpellSlot.E, string.Empty, false, 0);
                    return;
                }
            }

            if (enemy.Hero == Champion.Kennen && args.IsAutoAttack())
            {
                var data = BlockableSpellsHashSet.FirstOrDefault(x => x.ChampionName == Champion.Kennen && x.SpellSlot == SpellSlot.Unknown);

                if (data == null)
                    return;

                var buff = Player.Instance.Buffs.FirstOrDefault(x => x.Name.Equals("kennenmarkofstorm", StringComparison.CurrentCultureIgnoreCase));

                if (buff != null && (buff.Count == 2) && args.SData.Name.Equals(data.AdditionalBuffName, StringComparison.CurrentCultureIgnoreCase))
                {
                    Invoke(enemy, data.SpellSlot, data.AdditionalBuffName, true, data.AdditionalDelay);
                    return;
                }
            }

            if (enemy.Hero == Champion.Katarina && args.IsAutoAttack())
            {
                var data = BlockableSpellsHashSet.FirstOrDefault(x => x.ChampionName == Champion.Katarina && x.SpellSlot == SpellSlot.Unknown);

                if (data == null)
                    return;

                var buff = Player.Instance.Buffs.FirstOrDefault(x => x.Name.Equals(data.AdditionalBuffName, StringComparison.CurrentCultureIgnoreCase));

                if (buff != null)
                {
                    Invoke(enemy, data.SpellSlot, data.AdditionalBuffName, true, data.AdditionalDelay);
                    return;
                }
            }

            if (enemy.Hero == Champion.Jax && args.IsAutoAttack())
            {
                var data = BlockableSpellsHashSet.FirstOrDefault(x => x.ChampionName == Champion.Jax && x.SpellSlot == SpellSlot.W);

                if (data == null)
                    return;

                var buff = enemy.Buffs.FirstOrDefault(x => x.Name.Equals(data.AdditionalBuffName, StringComparison.CurrentCultureIgnoreCase));

                if (buff != null)
                {
                    Invoke(enemy, data.SpellSlot, data.AdditionalBuffName, true, data.AdditionalDelay);
                    return;
                }
            }

            if (enemy.Hero == Champion.Renekton && args.IsAutoAttack())
            {
                foreach (var data in
                        from data in
                            BlockableSpellsHashSet.Where(
                                x => x.ChampionName == Champion.Renekton && x.SpellSlot == SpellSlot.W)
                        let buff = enemy.Buffs.FirstOrDefault(
                                x =>
                                    x.Name.Equals(data.AdditionalBuffName, StringComparison.CurrentCultureIgnoreCase))
                        where buff != null
                        select data)
                {
                    Invoke(enemy, data.SpellSlot, data.AdditionalBuffName, true, data.AdditionalDelay);
                    return;
                }
            }

            if (enemy.Hero == Champion.Rengar && args.IsAutoAttack())
            {
                foreach (
                    var data in
                        from data in BlockableSpellsHashSet.Where(
                                x => x.ChampionName == Champion.Rengar && x.SpellSlot == SpellSlot.Q)
                        let buff =
                            enemy.Buffs.Find(
                                x =>
                                    x.Name.Equals(data.AdditionalBuffName, StringComparison.CurrentCultureIgnoreCase))
                        where buff != null
                        select data)
                {
                    Invoke(enemy, data.SpellSlot, data.AdditionalBuffName, true, data.AdditionalDelay);
                    return;
                }
            }

            if (enemy.Hero == Champion.Kassadin && args.IsAutoAttack())
            {
                var data = BlockableSpellsHashSet.FirstOrDefault(x => x.ChampionName == Champion.Kassadin && x.SpellSlot == SpellSlot.W);

                if (data == null)
                    return;

                var buff = enemy.Buffs.FirstOrDefault(x => x.Name.Equals(data.AdditionalBuffName, StringComparison.CurrentCultureIgnoreCase));

                if (buff != null)
                {
                    Invoke(enemy, data.SpellSlot, data.AdditionalBuffName, true, data.AdditionalDelay);
                    return;
                }
            }

            foreach (var blockableSpellData in BlockableSpellsHashSet.Where(x => x.ChampionName == enemy.Hero && !string.IsNullOrWhiteSpace(x.AdditionalBuffName) && args.SData.Name.Equals(x.AdditionalBuffName, StringComparison.CurrentCultureIgnoreCase)))
            {
                Invoke(enemy, blockableSpellData.SpellSlot, blockableSpellData.AdditionalBuffName, true, blockableSpellData.AdditionalDelay);
            }
        }

        private static void Game_OnTick(EventArgs args)
        {
            if (EntityManager.Heroes.Enemies.Any(x => x.Hero == Champion.KogMaw))
            {
                var enemy = EntityManager.Heroes.Enemies.FirstOrDefault(x => x.Hero == Champion.KogMaw);

                if (enemy == null)
                    return;

                var buff = enemy.Buffs.FirstOrDefault(x => x.Name.Equals("kogmawicathiansurprise", StringComparison.CurrentCultureIgnoreCase));

                if (buff != null && ((buff.EndTime - Game.Time) * 1000 < 350) && (enemy.Distance(Player.Instance) < 370))
                {
                    Invoke(enemy, SpellSlot.Unknown, string.Empty, false, 0);
                }
            }
            if (EntityManager.Heroes.Enemies.Any(x => x.Hero == Champion.Karthus))
            {
                var buff = Player.Instance.Buffs.FirstOrDefault(x => x.Name.Equals("karthusfallenonetarget", StringComparison.CurrentCultureIgnoreCase));

                if (buff != null && buff.Caster.GetType() == typeof(AIHeroClient) && (buff.EndTime - Game.Time) * 1000 < 350)
                {
                    Invoke(buff.Caster as AIHeroClient, SpellSlot.R, string.Empty, false, 0);
                }
            }
            if (EntityManager.Heroes.Enemies.Any(x => x.Hero == Champion.Tristana))
            {
                var buff = Player.Instance.Buffs.FirstOrDefault(x => x.Name.Equals("tristanaecharge", StringComparison.CurrentCultureIgnoreCase));

                if (buff != null && buff.Caster.GetType() == typeof(AIHeroClient) && ((buff.EndTime - Game.Time) * 1000 < 350))
                {
                    Invoke(buff.Caster as AIHeroClient, SpellSlot.E, string.Empty, false, 0);
                }
            }

            if (EntityManager.Heroes.Enemies.Any(x => x.Hero == Champion.Morgana))
            {
                var data = BlockableSpellsHashSet.FirstOrDefault(x => x.ChampionName == Champion.Morgana && x.SpellSlot == SpellSlot.R);

                if (data == null)
                    return;

                var buff = Player.Instance.Buffs.FirstOrDefault(x => x.Name.Equals(data.AdditionalBuffName, StringComparison.CurrentCultureIgnoreCase));

                if (buff != null && ((buff.EndTime - Game.Time) * 1000 < 350))
                {
                    var morgana = buff.Caster as AIHeroClient;

                    if (morgana == null)
                        return;

                    Invoke(morgana, SpellSlot.R, data.AdditionalBuffName, false, 0);
                }
            }

            if (EntityManager.Heroes.Enemies.Any(x => x.Hero == Champion.Kled))
            {
                var data = BlockableSpellsHashSet.FirstOrDefault(x => x.ChampionName == Champion.Kled && x.SpellSlot == SpellSlot.Q);

                if (data == null)
                    return;

                var buff = Player.Instance.Buffs.FirstOrDefault(x => x.Name.Equals(data.AdditionalBuffName, StringComparison.CurrentCultureIgnoreCase));

                if (buff != null && ((buff.EndTime - Game.Time) * 1000 < 350))
                {
                    var kled = buff.Caster as AIHeroClient;

                    if (kled == null)
                        return;

                    Invoke(kled, SpellSlot.Q, data.AdditionalBuffName, false, 0);
                }
            }

            if (EntityManager.Heroes.Enemies.Any(x => x.Hero == Champion.Nocturne))
            {
                var data = BlockableSpellsHashSet.FirstOrDefault(x => x.ChampionName == Champion.Nocturne && x.SpellSlot == SpellSlot.E);

                if (data == null)
                    return;

                var buff = Player.Instance.Buffs.FirstOrDefault(x => x.Name.Equals(data.AdditionalBuffName, StringComparison.CurrentCultureIgnoreCase));

                if (buff != null && ((buff.EndTime - Game.Time) * 1000 < 350))
                {
                    var nocturne = buff.Caster as AIHeroClient;

                    if (nocturne == null)
                        return;

                    Invoke(nocturne, SpellSlot.E, data.AdditionalBuffName, false, 0);
                }
            }

            if (EntityManager.Heroes.Enemies.Any(x => x.Hero == Champion.Zilean))
            {
                var data = BlockableSpellsHashSet.FirstOrDefault(x => x.ChampionName == Champion.Zilean && x.SpellSlot == SpellSlot.Q);

                if (data == null)
                    return;

                var buff = Player.Instance.Buffs.FirstOrDefault(x => x.Name.Equals(data.AdditionalBuffName, StringComparison.CurrentCultureIgnoreCase));

                if (buff != null && ((buff.EndTime - Game.Time) * 1000 < 350))
                {
                    var zilean = buff.Caster as AIHeroClient;

                    if (zilean == null)
                        return;

                    Invoke(zilean, SpellSlot.Q, data.AdditionalBuffName, false, 0);
                }
            }
        }

        private static void Obj_AI_Base_OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (BlockableSpellsHashSet.Count == 0)
            {
                Console.WriteLine("[DEBUG] Not found any spells that can be blocked ...");
                return;
            }

            var enemy = sender as AIHeroClient;

            if (enemy == null)
                return;

            foreach (var blockableSpellData in BlockableSpellsHashSet.Where(data => data.ChampionName == enemy.Hero))
            {
                if (!blockableSpellData.NeedsAdditionalLogics && args.Target != null && args.Target.IsMe && args.Slot == blockableSpellData.SpellSlot)
                {
                    Invoke(enemy, args.Slot, blockableSpellData.AdditionalBuffName, false, 0);
                }
                else if (blockableSpellData.NeedsAdditionalLogics)
                {
                    if (args.SData.IsAutoAttack() && args.Target != null && args.Target.IsMe && !string.IsNullOrWhiteSpace(blockableSpellData.AdditionalBuffName) &&
                        blockableSpellData.AdditionalBuffName == args.SData.Name.ToLowerInvariant())
                    {
                        OnBlockableSpell?.Invoke(enemy, new OnBlockableSpellEventArgs(Getdata(enemy.Hero, args.Slot), false));
                    }

                    switch (enemy.Hero)
                    {
                        case Champion.Azir:
                            {
                                if (args.Slot == blockableSpellData.SpellSlot && (enemy.Distance(Player.Instance) < 300))
                                {
                                    Invoke(enemy, args.Slot, blockableSpellData.AdditionalBuffName, false, 0);
                                }
                                break;
                            }
                        case Champion.Amumu:
                            {
                                if (args.Slot == blockableSpellData.SpellSlot &&
                                    (enemy.Distance(Player.Instance) < 1100))
                                {
                                    Invoke(enemy, args.Slot, blockableSpellData.AdditionalBuffName, false, 0);
                                }
                                break;
                            }
                        case Champion.Akali:
                            {
                                if (args.Slot == blockableSpellData.SpellSlot && (enemy.Distance(Player.Instance) < 325))
                                {
                                    Invoke(enemy, args.Slot, blockableSpellData.AdditionalBuffName, false, 0);
                                }
                                break;
                            }
                        case Champion.Bard:
                            {
                                if (args.Slot == blockableSpellData.SpellSlot &&
                                    new Geometry.Polygon.Circle(args.End, 325).IsInside(Player.Instance))
                                {
                                    Core.DelayAction(
                                        () => Invoke(enemy, args.Slot, blockableSpellData.AdditionalBuffName, false, 0),
                                        (int)Math.Max(enemy.Distance(Player.Instance) / 2000 * 1000 - 300, 0));
                                }
                                break;
                            }
                        case Champion.Diana:
                            {
                                if (args.Slot == blockableSpellData.SpellSlot &&
                                    new Geometry.Polygon.Circle(args.End, 225).IsInside(Player.Instance))
                                {
                                    Invoke(enemy, args.Slot, blockableSpellData.AdditionalBuffName, false, 0);
                                }
                                break;
                            }
                        case Champion.Caitlyn:
                            {
                                if (args.Slot == blockableSpellData.SpellSlot && args.Target != null && args.Target.IsMe)
                                {
                                    Core.DelayAction(
                                        () =>
                                            Invoke(enemy, args.Slot, blockableSpellData.AdditionalBuffName, false, 0),
                                        (int)
                                            Math.Max(
                                                enemy.Distance(Player.Instance) / args.SData.MissileSpeed * 1000 + 500, 0));
                                }
                                break;
                            }
                        case Champion.Gnar:
                            {
                                if (args.Slot == blockableSpellData.SpellSlot &&
                                    enemy.IsInRange(Player.Instance, 590))
                                {
                                    Invoke(enemy, args.Slot, blockableSpellData.AdditionalBuffName, false, 0);
                                }
                                break;
                            }
                        case Champion.Kalista:
                            {
                                if (args.Slot == blockableSpellData.SpellSlot &&
                                    Player.Instance.HasBuff(blockableSpellData.AdditionalBuffName))
                                {
                                    Invoke(enemy, args.Slot, blockableSpellData.AdditionalBuffName, false, 0);
                                }
                                break;
                            }
                        case Champion.JarvanIV:
                            {
                                if (args.Slot == blockableSpellData.SpellSlot)
                                {
                                    var flag =
                                        ObjectManager.Get<Obj_AI_Minion>()
                                            .FirstOrDefault(
                                                x => x.Name.Equals("beacon", StringComparison.CurrentCultureIgnoreCase));

                                    if (flag == null)
                                        continue;

                                    var endPos = enemy.Position.Extend(args.End, 790).To3D();
                                    var flagpolygon = new Geometry.Polygon.Circle(flag.Position, 150);
                                    var qPolygon = new Geometry.Polygon.Rectangle(enemy.Position, endPos, 180);
                                    var playerpolygon = new Geometry.Polygon.Circle(Player.Instance.Position,
                                        Player.Instance.BoundingRadius);

                                    for (var i = 0; i <= 800; i += 100)
                                    {
                                        if (flagpolygon.IsInside(enemy.Position.Extend(args.End, i)) &&
                                            playerpolygon.Points.Any(x => qPolygon.IsInside(x)))
                                        {
                                            Invoke(enemy, args.Slot, blockableSpellData.AdditionalBuffName, false, 0);
                                        }
                                    }
                                }
                                break;
                            }
                        case Champion.Blitzcrank:
                            {
                                switch (args.Slot)
                                {
                                    case SpellSlot.Q:
                                        const int speed = 1800;
                                        var eta = (int)(enemy.Distance(Player.Instance) / speed) - 250;
                                        var endPos = enemy.Position.Extend(args.End,
                                            enemy.Distance(args.End) > 1050 ? 1050 : enemy.Distance(args.End));

                                        Core.DelayAction(() =>
                                        {
                                            if (endPos.IsInRange(Player.Instance, 100))
                                            {
                                                Invoke(enemy, args.Slot, blockableSpellData.AdditionalBuffName, false, 0);
                                            }
                                        }, eta);
                                        break;
                                    case SpellSlot.R:
                                        Invoke(enemy, SpellSlot.R, blockableSpellData.AdditionalBuffName, false, 0);
                                        break;
                                    default:
                                        continue;
                                }
                                break;
                            }
                        case Champion.Warwick:
                            {
                                if ((args.Slot == blockableSpellData.SpellSlot ||
                                     args.SData.Name.Equals(blockableSpellData.AdditionalBuffName,
                                         StringComparison.CurrentCultureIgnoreCase)) &&
                                    ((args.End.Distance(Player.Instance) < 500) || args.Target.IsMe))
                                {
                                    Invoke(enemy, args.Slot, blockableSpellData.AdditionalBuffName, false, 0);
                                }
                                break;
                            }
                        case Champion.Cassiopeia:
                            {
                                if (args.Slot == blockableSpellData.SpellSlot)
                                {
                                    var endPos =
                                        enemy.Position.Extend(args.End,
                                            args.End.Distance(enemy) > 850 ? 850 : args.End.Distance(enemy))
                                            .To3D();

                                    var rPolygon = new Geometry.Polygon.Sector(enemy.Position, endPos, 850, 80 * (float)(Math.PI / 180F));
                                    var playerpolygon = new Geometry.Polygon.Circle(Player.Instance.Position,
                                        Player.Instance.BoundingRadius);

                                    if (playerpolygon.Points.Any(x => rPolygon.IsInside(x)))
                                    {
                                        Invoke(enemy, args.Slot, blockableSpellData.AdditionalBuffName, false, 0);
                                    }
                                }
                                break;
                            }
                        case Champion.Evelynn:
                            {
                                if (args.Slot == blockableSpellData.SpellSlot)
                                {
                                    var endPos =
                                        enemy.Position.Extend(args.End,
                                            args.End.Distance(enemy) > 650 ? 650 : args.End.Distance(enemy))
                                            .To3D();

                                    if (endPos.IsInRange(Player.Instance, 500))
                                    {
                                        Invoke(enemy, args.Slot, blockableSpellData.AdditionalBuffName, false, 0);
                                    }
                                }
                                break;
                            }
                        case Champion.Janna:
                            {
                                if (args.Slot == blockableSpellData.SpellSlot &&
                                    enemy.IsInRange(Player.Instance, 875))
                                {
                                    Invoke(enemy, args.Slot, blockableSpellData.AdditionalBuffName, false, 0);
                                }
                                break;
                            }
                        case Champion.Lissandra:
                            {
                                if (args.Slot == blockableSpellData.SpellSlot &&
                                    enemy.IsInRange(Player.Instance, 900))
                                {
                                    Invoke(enemy, args.Slot, blockableSpellData.AdditionalBuffName, false, 0);
                                }
                                break;
                            }
                        case Champion.Malphite:
                            {
                                if (args.Slot == blockableSpellData.SpellSlot)
                                {
                                    var speed = enemy.MoveSpeed - 335 + 1835;
                                    var eta = (int)(enemy.Distance(Player.Instance) / speed * 1000 + 250) -
                                              400;
                                    var endPos = enemy.Position.Extend(args.End,
                                        enemy.Distance(args.End) > 1000
                                            ? 1000
                                            : enemy.Distance(args.End));

                                    Core.DelayAction(() =>
                                    {
                                        if (endPos.IsInRange(Player.Instance, 300))
                                        {
                                            Invoke(enemy, args.Slot, blockableSpellData.AdditionalBuffName, false, 0);
                                        }
                                    }, eta);
                                }
                                break;
                            }
                        case Champion.Sona:
                            {
                                if (args.Slot == blockableSpellData.SpellSlot)
                                {
                                    var speed = enemy.MoveSpeed - 335 + 1835;
                                    var eta = (int)(enemy.Distance(Player.Instance) / speed * 1000 + 250) -
                                              400;
                                    var endPos = enemy.Position.Extend(args.End,
                                        enemy.Distance(args.End) > 1000
                                            ? 1000
                                            : enemy.Distance(args.End));

                                    Core.DelayAction(() =>
                                    {
                                        if (endPos.IsInRange(Player.Instance, 300))
                                        {
                                            Invoke(enemy, args.Slot, blockableSpellData.AdditionalBuffName, false, 0);
                                        }
                                    }, eta);
                                }
                                break;
                            }
                        case Champion.Sejuani:
                            {
                                if (args.Slot == blockableSpellData.SpellSlot &&
                                    Player.Instance.HasBuff(blockableSpellData.AdditionalBuffName))
                                {
                                    Invoke(enemy, args.Slot, blockableSpellData.AdditionalBuffName, false, 0);
                                }
                                break;
                            }
                        case Champion.Graves:
                            {
                                if (args.Slot == blockableSpellData.SpellSlot)
                                {
                                    var endPos = enemy.Position.Extend(args.End, 1000).To3D();
                                    var rPolygon = new Geometry.Polygon.Rectangle(enemy.Position,
                                        endPos, 130);
                                    var playerpolygon =
                                        new Geometry.Polygon.Circle(Player.Instance.Position,
                                            Player.Instance.BoundingRadius);

                                    if (playerpolygon.Points.Any(x => rPolygon.IsInside(x)))
                                    {
                                        Invoke(enemy, args.Slot, blockableSpellData.AdditionalBuffName, false, 0);
                                    }
                                }
                                break;
                            }
                        case Champion.Hecarim:
                            {
                                if (args.Slot == blockableSpellData.SpellSlot)
                                {
                                    var endPos = enemy.Position.Extend(args.End, 1000).To3D();
                                    var rPolygon = new Geometry.Polygon.Rectangle(
                                        enemy.Position, endPos, 300);
                                    var playerpolygon =
                                        new Geometry.Polygon.Circle(Player.Instance.Position,
                                            Player.Instance.BoundingRadius);

                                    if (playerpolygon.Points.Any(x => rPolygon.IsInside(x)))
                                    {
                                        Invoke(enemy, args.Slot, blockableSpellData.AdditionalBuffName, false, 0);
                                    }
                                }
                                break;
                            }
                        case Champion.Nautilus:
                            {
                                if (args.Slot == blockableSpellData.SpellSlot &&
                                    args.Target != null && args.Target.IsMe)
                                {
                                    Core.DelayAction(
                                        () => Invoke(enemy, args.Slot, blockableSpellData.AdditionalBuffName, false, 0),
                                        (int)Math.Max(
                                            enemy.Distance(Player.Instance) /
                                            args.SData.MissileSpeed * 1000 - 300, 0));
                                }
                                break;
                            }
                        case Champion.Vi:
                            {
                                if (args.Slot == blockableSpellData.SpellSlot &&
                                    args.Target != null && args.Target.IsMe)
                                {
                                    Core.DelayAction(() =>
                                    {
                                        if (enemy.Distance(Player.Instance) < 350)
                                            Invoke(enemy, args.Slot, blockableSpellData.AdditionalBuffName, false, 0);

                                    },
                                        (int)Math.Max(enemy.Distance(Player.Instance) /
                                                       args.SData.MissileSpeed * 1000 - 400,
                                            0));
                                }
                                break;
                            }
                        case Champion.Yasuo:
                            {
                                if (args.SData.Name.Equals(
                                    blockableSpellData.AdditionalBuffName,
                                    StringComparison.CurrentCultureIgnoreCase) &&
                                    enemy.IsInRange(Player.Instance, 380))
                                {
                                    Invoke(enemy, args.Slot, blockableSpellData.AdditionalBuffName, false, 0);
                                }
                                break;
                            }
                        case Champion.Morgana:
                            {
                                if (
                                    args.Slot == blockableSpellData.SpellSlot &&
                                    enemy.IsInRange(Player.Instance, 600))
                                {
                                    Invoke(enemy, args.Slot, blockableSpellData.AdditionalBuffName, false, 0);
                                }
                                break;
                            }
                        case Champion.Zed:
                            {
                                if (args.Slot ==
                                    blockableSpellData.SpellSlot && args.Target != null && args.Target.IsMe)
                                {
                                    Core.DelayAction(
                                        () => Invoke(enemy, args.Slot, blockableSpellData.AdditionalBuffName, false, 0),
                                        300);
                                }
                                break;
                            }
                    }
                }
            }
        }

        public class OnBlockableSpellEventArgs : EventArgs
        {
            public Champion ChampionName { get; private set; }
            public bool IsAutoAttack { get; }
            public SpellSlot SpellSlot { get; }
            public int AdditionalDelay { get; private set; }
            public bool HasMissile { get; set; }
            public bool Gapclose { get; set; }
            public bool IsZhonya { get; set; }
            public bool IsSpellShield { get; set; } = true;
            public bool IsExhaust { get; set; }
            public bool IsShield { get; set; }

            public OnBlockableSpellEventArgs(Champion championName, SpellSlot spellSlot, bool isAutoAttack, int additionalDelay)
            {
                ChampionName = championName;
                SpellSlot = spellSlot;
                IsAutoAttack = isAutoAttack;
                AdditionalDelay = additionalDelay;
            }
            public OnBlockableSpellEventArgs(BlockableSpellData data, bool isAutoAttack)
            {
                ChampionName = data.ChampionName;
                SpellSlot = data.SpellSlot;
                IsAutoAttack = isAutoAttack;
                AdditionalDelay = data.AdditionalDelay;
                HasMissile = data.HasMissile;
                Gapclose = data.Gapclose;
                IsZhonya = data.IsZhonya;
                IsShield = data.IsShield;
                IsExhaust = data.IsExhaust;
                IsSpellShield = data.IsSpellShield;
            }
        }

        public class BlockableSpellData
        {
            public Champion ChampionName { get; set; }
            public bool NeedsAdditionalLogics { get; set; }
            public string AdditionalBuffName { get; set; }
            public SpellSlot SpellSlot { get; set; }
            public bool HasMissile { get; set; }
            public bool Gapclose { get; set; }
            public bool IsZhonya { get; set; }
            public bool IsSpellShield { get; set; } = true;
            public bool IsExhaust { get; set; }
            public bool IsShield { get; set; }

            public string SpellName { get; set; }
            public int AdditionalDelay { get; set; }
            public BlockableSpellData() { }
            public BlockableSpellData(Champion championName, string spellName, SpellSlot spellSlot, bool isAll = false)
            {
                ChampionName = championName;
                SpellSlot = spellSlot;
                SpellName = spellName;
                if (isAll)
                {
                    IsExhaust = isAll;
                    IsShield = isAll;
                    IsSpellShield = isAll;
                    IsZhonya = isAll;
                }
            }
        }
    }
}

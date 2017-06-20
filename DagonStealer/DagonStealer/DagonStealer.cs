using System;
using Ensage;
using Ensage.Common;
using Ensage.Common.Extensions;
using Ensage.Common.Menu;

namespace DagonStealer
{
    internal class DagonStealer
    {
        #region Static Fields

        public static Hero Me;

        private static Item Dagon;
        private static int Damage;

        public static Menu Menu;
        private static bool loaded;

        private static int manaNeeded;
        private static bool Sleeped = false;

        #endregion

        #region OnLoad
        public static void OnLoad()
        {
            loaded = false;
            Dagon = null;

            MenuGenerator.Load();

            Game.OnUpdate += OnUpdate;
        }

        #endregion

        #region OnUpdate

        private static void OnUpdate(EventArgs args)
        {
            if (!loaded)
            {
                Me = ObjectManager.LocalHero;
                if (!Game.IsInGame || Me == null)
                    return;

                loaded = true;
            }

            if (!Menu.Item("cast.quick.enable").GetValue<bool>())
                return;

            if (Me.IsInvisible() && !Menu.Item("cast.quick.invise.enable").GetValue<bool>())
                return;

            if (!Game.IsInGame || Me == null)
            {
                Dagon = null;
                loaded = false;
                return;
            }

            if (Game.IsPaused)
                return;

            Hero target = TargetSelector.ClosestToMouse(Me, 550);

            if (target == null)
                return;

            for (int i = 1; Dagon == null && i <= 5; i++)
            {
                switch (i)
                {
                    case 1:
                        Dagon = Me.FindItem("item_dagon");
                        Damage = 400;
                        break;
                    case 2:
                        Dagon = Me.FindItem("item_dagon_2");
                        Damage = 500;
                        break;
                    case 3:
                        Dagon = Me.FindItem("item_dagon_3");
                        Damage = 600;
                        break;
                    case 4:
                        Dagon = Me.FindItem("item_dagon_4");
                        Damage = 700;
                        break;
                    case 5:
                        Dagon = Me.FindItem("item_dagon_5");
                        Damage = 800;
                        break;
                }
            }

            if (Dagon == null)
                return;

            manaNeeded = (int) Dagon.ManaCost;

            if (Me.HasModifier("Rune Doubledamage"))
                Damage *= 2;

            if (Me.HasModifier("Rune Arcane"))
                manaNeeded -= (int)(manaNeeded * 0.4f);

            /* Delay realisation */
            if (Menu.Item("cast.delay").GetValue<Slider>().Value > 0)
            {
                if (!Sleeped && Utils.SleepCheck("dagonDelay"))
                {
                    Utils.Sleep(Menu.Item("cast.delay").GetValue<Slider>().Value, "dagonDelay");
                    Sleeped = true;
                    return;
                }
                if (Sleeped && !Utils.SleepCheck("dagonDelay"))
                    return;
                if (Sleeped && Utils.SleepCheck("dagonDelay"))
                    Sleeped = false;
            } else if (Sleeped)
                Sleeped = false;
            /* END */

            if (manaNeeded <= Me.Mana)
                DagonCast(target);

            Dagon = null;
        }

        #endregion

        #region DagonCast

        public static void DagonCast(Hero targetHero)
        {
            if (targetHero.Health > Menu.Item("health.trigger.health").GetValue<Slider>().Value && Menu.Item("health.trigger.enable").GetValue<bool>())
                return;

            if (Dagon.Cooldown == 0.0f)
            {
                var distance =
                    Math.Sqrt(
                        Math.Pow(targetHero.Position.X - Me.Position.X, 2)
                        + Math.Pow(targetHero.Position.Y - Me.Position.Y, 2));

                if (distance > 0 && distance <= Dagon.CastRange && (float)Damage - (float)Damage * targetHero.MagicDamageResist >= targetHero.Health)
                    Dagon.UseAbility(targetHero);
            }
        }

        #endregion
    }
}

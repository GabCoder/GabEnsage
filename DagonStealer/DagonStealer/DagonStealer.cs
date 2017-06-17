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
                {
                    return;
                }
                loaded = true;
            }

            if (!Game.IsInGame || Me == null)
            {
                Dagon = null;
                loaded = false;
                return;
            }

            if (Game.IsPaused)
            {
                return;
            }

            Hero target = TargetSelector.ClosestToMouse(Me, 250);

            if (target == null)
                return;

            for (int i = 1; Dagon == null; i++)
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
                    default:
                        return;
                }

            }

            if (Menu.Item("cast.quick.enable").GetValue<bool>() && Dagon.ManaCost <= Me.Mana)
                DagonCast(target);
        }

        #endregion

        #region DagonCast

        public static void DagonCast(Hero targetHero)
        {
            if (Menu.Item("health.trigger.enable").GetValue<bool>() &&
                targetHero.Health > Menu.Item("health.trigger.health").GetValue<Slider>().Value)
                return;

            if (Dagon != null && Dagon.Cooldown == 0.0f)
            {
                var distance =
                    Math.Sqrt(
                        Math.Pow(targetHero.Position.X - Me.Position.X, 2)
                        + Math.Pow(targetHero.Position.Y - Me.Position.Y, 2));

                if (distance > 0)
                {
                    if (distance <= Dagon.CastRange && (float)Damage - (float)Damage * targetHero.MagicDamageResist >= targetHero.Health)
                    {
                        Dagon.UseAbility(targetHero);
                    }
                }
            }
        }

        #endregion
    }
}

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

        public static Menu Menu;
        private static bool loaded;

        #endregion

        #region OnLoad
        public void OnLoad()
        {
            try
            {
                loaded = false;
                Dagon = null;

                MenuGenerator.Load();

                Game.OnUpdate += OnUpdate;
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: '{0}'", e);
            }
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
                Dagon = Me.FindItem("item_dagon");
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

            if (Dagon == null)
            {
                Dagon = Me.FindItem("item_dagon");
            }

            if (Menu.Item("cast.quick.enable").GetValue<bool>())
            {
                if (Utils.SleepCheck("dagonCheck"))
                {
                    DagonCast(Me.ClosestToMouseTarget());
                }
            }
        }

        #endregion

        #region DagonCast

        public static void DagonCast(Hero targetHero)
        {
            if (Dagon != null && Dagon.Cooldown == 0)
            {
                var distance =
                    Math.Sqrt(
                        Math.Pow(targetHero.Position.X - Me.Position.X, 2)
                        + Math.Pow(targetHero.Position.X - Me.Position.Y, 2));

                if (distance > 0)
                {
                    if (distance < Dagon.CastRange)
                    {
                        Dagon.UseAbility(targetHero);
                        Utils.Sleep(250, "dagonCheck");
                    }
                }
            }
        }

        #endregion
    }
}

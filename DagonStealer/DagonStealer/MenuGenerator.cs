using Ensage.Common.Menu;

namespace DagonStealer
{
    public class MenuGenerator
    {
        public static void Load()
        {
            DagonStealer.Menu = new Menu("Dagon Stealer", "dagon_stealer", true);

            DagonStealer.Menu.AddItem(new MenuItem("cast.quick.enable", "Enable Dagon Stealer").SetValue(true));
            DagonStealer.Menu.AddItem(new MenuItem("health.trigger.enable", "Enable Health-Cast").SetValue(false));
            DagonStealer.Menu.AddItem(new MenuItem("health.trigger.health", "Maximum health [Health-Cast]").SetValue(new Slider(150, 1, 1000)));

            /* In next update
            DagonStealer.Menu.AddItem(new MenuItem("cast.delay", "Delay (ms)").SetValue(new Slider(0, 0, 5000))); */

            DagonStealer.Menu.AddItem(new MenuItem("by.HriKer", "Made by HriKer"));

            DagonStealer.Menu.AddToMainMenu();
        }
    }
}

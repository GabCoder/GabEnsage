using Ensage.Common;
using Ensage.Common.Menu;

namespace DagonStealer
{
    public class MenuGenerator
    {
        public static void Load()
        {
            DagonStealer.Menu = new Menu("Dagon Stealer", "dagon_stealer", true);

            DagonStealer.Menu.AddItem(new MenuItem("cast.quick.enable", "Enable Dagon Stealer").SetValue(true));

            DagonStealer.Menu.AddItem(new MenuItem("seperator", ""));
            DagonStealer.Menu.AddItem(new MenuItem("by.gabCoder", "Made by GabCoder"));

            DagonStealer.Menu.AddToMainMenu();
        }
    }
}

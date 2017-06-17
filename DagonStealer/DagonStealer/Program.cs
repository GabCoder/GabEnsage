using System;

namespace DagonStealer
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                DagonStealer.OnLoad();
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: '{0}'", e);
            }
        }
    }
}

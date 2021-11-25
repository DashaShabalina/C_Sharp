using System;
namespace WorldOfWorms
{
    public class NameGenerator
    {
        private static int counter = 0;
        static public String GetName()
        {
            counter++;
            return "Worm" + counter;
        }
    }
}
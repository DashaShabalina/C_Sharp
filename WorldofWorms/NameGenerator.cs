using System;
namespace WorldOfWorms
{
    static class NameGenerator
    {
        private static int counter = 0;
        static public String GetName()
        {
            counter++;
            return "Worm" + counter;
        }
    }
}

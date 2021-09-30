namespace Generator
{
    static class IdGenerator
    {
        private static int counter = 0;

        static public int GetId()
        {
            counter++;
            return counter;
        }
    }
}

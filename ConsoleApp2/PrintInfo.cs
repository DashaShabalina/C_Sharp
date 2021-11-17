using System.Text;

namespace WorldOfWorms
{
    public class PrintInfo
    {
        public PrintInfo(string Name, int x, int y, int move, StringBuilder foodInfo, Actions actions, int health)
        {
            WormName = Name;
            X = x;
            Y = y;
            CurrentMove = move;
            FoodInfo = foodInfo;
            CurrentActions = actions;
            Health = health;
        }
        public string WormName { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int CurrentMove { get; set; }
        public StringBuilder FoodInfo { get; set; }
        public Actions CurrentActions { get; set; }
        public int Health { get; set; }
    }
}

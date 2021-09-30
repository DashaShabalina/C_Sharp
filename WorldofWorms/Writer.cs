using System;
using System.IO;

namespace WorldOfWorms
{
    class Writer
    {
        private readonly StreamWriter sw;
        public Writer()
        {
            sw = new StreamWriter("out.txt", false, System.Text.Encoding.Default);
        }
        public Writer(string path)
        {
            sw = new StreamWriter(path, false, System.Text.Encoding.Default);
        }
        public void Write(string name, int x, int y)
        {
            sw.WriteLine($"Worms: [{name} ({x}, {y})]");
        }

        public void Close()
        {
            sw.Close();
        }
    }
}

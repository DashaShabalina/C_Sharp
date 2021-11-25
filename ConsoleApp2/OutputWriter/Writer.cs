using System;
using System.IO;
using System.Text;

namespace WorldOfWorms
{
    public interface IWriter
    {
        void Write(PrintInfo info);
        //void Close();
    }
    public class Writer : IWriter, IDisposable
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

        public void Write(PrintInfo info)
        {
            sw.WriteLine($"{info.CurrentMove} - Worms:[{info.WormName} ({info.X}, {info.Y})], Food: [{info.FoodInfo}], Actions - {info.CurrentActions}, Health - {info.Health}");
        }

        
        //public void Close()
        //{
        //    sw.Close();
        //}

        public void Dispose()
        {
            sw.Dispose();
        }
    }
}

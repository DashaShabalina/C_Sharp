using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldofWorms.Data
{
    public class BehaviorInfo
    {
        public int Order { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public int BehaviorId { get; set; }
        public Behavior Behavior { get; set; }
    }
}

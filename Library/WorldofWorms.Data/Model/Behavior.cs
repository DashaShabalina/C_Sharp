using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldofWorms.Data
{
    public class Behavior
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<BehaviorInfo> Steps { get; set; }
    }
}

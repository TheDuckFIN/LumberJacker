using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LumberJacker.Util
{
    public class Vector2
    {
        public int x { get; set; }
        public int y { get; set; }

        public Vector2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return "(" + this.x + ", " + this.y + ")";
        }
    }
}

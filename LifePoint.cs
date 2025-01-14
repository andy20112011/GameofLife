using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameofLife;

internal class LifePoint
{
    public long x { get; set; }
    public long y { get; set; }

    public LifePoint(long x, long y)
    {
        this.x = x;
        this.y = y;
    }
}

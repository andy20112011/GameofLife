using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameofLife;

internal class LifeRect
{
    public long x {  get; set; }
    public long y { get; set; }
    public long width { get; set; }
    public long height { get; set; }

    public LifeRect(long x, long y, long width, long height)
    {
        this.x = x;
        this.y = y;
        this.width = width;
        this.height = height;
    }
}

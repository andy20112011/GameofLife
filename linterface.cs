using System;
namespace GameofLife;
interface GameOfLife
{
    void Clear();
    bool this[long x, long y] { get; set; }
    bool this[LifePoint v] { get; set; }

    // Life boards almost always have far more dead cells than alive cells, we can get a win by only drawing the live cells
    void Step();
}
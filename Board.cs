using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameofLife;

internal class Board : GameOfLife
{
    // if scale = 0, every pixel represents 1 cell; if scale > 0, each pixel represents a block of cells that is 1 << scale; if scale < 0, each life cell is represented by a block
    // that is 1 << -scale wide.
    private int scale = 0;

    internal int cellSize = GameSettings.CellSize;
    internal int height = GameSettings.HEIGHT;
    internal int width = GameSettings.WIDTH;
    public bool[,] cells;
    internal int rows { get; }
    internal int columns { get; }


    public Board()
    {
        Clear();
        rows = width / cellSize;
        columns = height / cellSize;
    }

    private bool isValidPoint(long x, long y) =>
        x >= 0 && y >= 0 && x < width && y < height;

    public bool this[long x, long y]
    {
        get
        {
            if (isValidPoint(x, y))
            {
                return cells[x, y];
            }
            return false;
        }
        set
        {
            if (isValidPoint(x, y))
            {
                cells[x, y] = value;
            }
        }
    }

    public bool this[LifePoint v]
    {
        get => this[v.x, v.y];
        set => this[v.x, v.y] = value;
    }

    public int LivingNeighbor(long x, long y)
    {
        int sum = this[x - 1, y - 1] ? 1 : 0;
        sum += this[x - 1, y] ? 1 : 0;
        sum += this[x, y - 1] ? 1 : 0;
        sum += this[x, y + 1] ? 1 : 0;
        sum += this[x + 1, y] ? 1 : 0;
        sum += this[x + 1, y - 1] ? 1 : 0;
        sum += this[x + 1, y + 1] ? 1 : 0;
        sum += this[x - 1, y + 1] ? 1 : 0;
        return sum;
    }

    public void Clear()
    {
        cells = new bool[width, height];
        cells[10, 9] = true;
        cells[10, 10] = true;
        cells[10, 11] = true;
    }

    public void Step()
    {
        bool[,] newCells = new bool[width, height];
        for (long x = 0; x < width; x++)
        {
            for (long y = 0; y < height; y++)
            {
                int count = LivingNeighbor(x, y);
                newCells[x, y] = count == 3 || (cells[x, y] && count == 2);
            }
        }

        cells = newCells;
    }
}

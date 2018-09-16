using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Map
{
    private List<PlatformTiles> collisionTiles = new List<PlatformTiles>();
    public List<PlatformTiles> CollisionTiles
    {
        get
        {
            return collisionTiles;
        }
    }

    private List<BackgroundTiles> backgroundTiles = new List<BackgroundTiles>();
    public List<BackgroundTiles> BackgroundTiles
    {
        get
        {
            return backgroundTiles;
        }
    }

    private List<TrapTiles> trapTiles = new List<TrapTiles>();
    public List<TrapTiles> TrapTiles
    {
        get
        {
            return trapTiles;
        }
    }

    private int width, height;
    public int Width
    {
        get
        {
            return width;
        }
    }

    public int Height
    {
        get
        {
            return height;
        }
    }

    public Map()
    {
    }

    public void Generate(string[,] map, int size)
    {
        for (int x = 0; x < 16 ; x++)
        {
            for (int y = 0; y < 1000; y++)
            {
                string tile = map[y, x];

                if (tile == "0" || tile == "1" || tile == "2")
                    backgroundTiles.Add(new BackgroundTiles(tile, new Rectangle(x * size, y * size, size, size)));
                if (tile == "3")
                {
                    collisionTiles.Add(new PlatformTiles(tile, new Rectangle(x * size, y * size, size, size)));
                }
                if (tile == "4"||tile=="5")
                {
                    trapTiles.Add(new TrapTiles(tile, new Rectangle(x * size, y * size, size, size)));
                }
                    
                
                width = (x + 1) * size;
                height = (y + 1) * size;
            }
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (BackgroundTiles tile in backgroundTiles)
        {
            tile.Draw(spriteBatch);
        }

        foreach (PlatformTiles tile in collisionTiles)
        {
            tile.Draw(spriteBatch);
        }

        foreach (TrapTiles tile in trapTiles)
        {
            tile.Draw(spriteBatch);
        }
    }
}


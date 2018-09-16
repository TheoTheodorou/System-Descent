using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PlatformTiles : Tile
{
    public PlatformTiles(string i, Rectangle newRectangle)
    {
        texture = Content.Load<Texture2D>("tiles/tile" + i);
        this.Rectangle = newRectangle;
    }
}

public class BackgroundTiles : Tile
{
    public BackgroundTiles(string i, Rectangle newRectangle)
    {
        texture = Content.Load<Texture2D>("tiles/tile" + i);
        this.Rectangle = newRectangle;
    }
}

public class TrapTiles : Tile
{
    public TrapTiles(string i, Rectangle newRectangle)
    {
        texture = Content.Load<Texture2D>("traps/trap" + i);
        this.Rectangle = newRectangle;
    }
}
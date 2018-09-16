using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Tile
{
    public bool isVisible = true;
    protected Texture2D texture;
    private Rectangle rectangle;
    public Rectangle Rectangle
    {
        get
        {
            return rectangle;
        }
        protected set
        {
            rectangle = value;
        }
    }

    private static ContentManager content;
    public static ContentManager Content
    {
        protected get
        {
            return content;
        }
        set
        {
            content = value;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (isVisible)
            spriteBatch.Draw(texture, rectangle, Color.White);
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Bullet
{
    public Texture2D texture;
    public Vector2 position;
    public Vector2 velocity;
    public Vector2 origin;
    public bool isVisible;
    public bool isLeft;
    public Rectangle rectangle;

    public Bullet(Texture2D input_texture, bool is_left)
    {
        texture = input_texture;
        isVisible = false;
        isLeft = is_left;

    }

    public void Draw(SpriteBatch spriteBatch, Player player)
    {
        if (isLeft)
            spriteBatch.Draw(texture, position, null, Color.White, 0f, origin, 1f, SpriteEffects.FlipHorizontally, 0);
        else
            spriteBatch.Draw(texture, position, null, Color.White, 0f, origin, 1f, SpriteEffects.None, 0);
    }


    public void TrapCollsion(Rectangle newRectangle, int xOffset, int yOffset, TrapTiles tile, Player player)
    {
        if (rectangle.EnemyTouchTop(newRectangle) ||
            rectangle.EnemyTouchLeft(newRectangle) ||
            rectangle.EnemyTouchRight(newRectangle) ||
            rectangle.EnemyTouchBottom(newRectangle))
        {
            
            isVisible = false;
            tile.isVisible = false;
            player.score += 500;
       

        }




    }
}
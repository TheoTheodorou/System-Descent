using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Player : Character
{
    public Texture2D texture;
    public Vector2 velocity;
    public Vector2 position = new Vector2(640, 200);
    public Rectangle hitbox_rectangle;
    public Texture2D rectangle_texture;
    public int score;
    public int speed;

    public bool hasJumped = false;
    public bool isDead = false;
    public bool isLeft = true;

    public void Update(GameTime gameTime)
    {


        position += velocity;
        hitbox_rectangle = new Rectangle((int)position.X - 25, (int)position.Y - 44, 55, 85);

        if (velocity.Y < 8)
            velocity.Y += 0.5f;





    }


    public void PlatformCollsion(Rectangle newRectangle, int xOffset, int yOffset,float bottom_edge, float top_edge)
    {
        if (hitbox_rectangle.PlayerTouchTop(newRectangle))
        {
            position.Y = newRectangle.Y - 42;
            velocity.Y = 0;
            hasJumped = false;
        }

        if (hitbox_rectangle.PlayerTouchLeft(newRectangle))
        {
            position.X = newRectangle.X - hitbox_rectangle.Width + 20;
        }

        if (hitbox_rectangle.PlayerTouchRight(newRectangle))
        {
            position.X = newRectangle.X + newRectangle.Width + 35;
        }
        if (hitbox_rectangle.PlayerTouchBottom(newRectangle))
        {
            position.Y = newRectangle.Y + newRectangle.Height + 60;
            velocity.Y = 1;
        }

        if (position.X < 30)
        {
            position.X = 30;
        }

        if (position.X > 1280 -25)
        {
            position.X = 1280 - 25;
        }

        if (position.Y < top_edge+40)
        {
            isDead = true;
        }

        if (position.Y > bottom_edge+200)
        {
            isDead = true;
        }
    }


    public void TrapCollsion(Rectangle newRectangle, int xOffset, int yOffset)
    {
        if (hitbox_rectangle.PlayerTouchTop(newRectangle) ||
            hitbox_rectangle.PlayerTouchLeft(newRectangle) ||
            hitbox_rectangle.PlayerTouchRight(newRectangle) ||
            hitbox_rectangle.PlayerTouchBottom(newRectangle))
        {
            isDead = true;
        }



    }


}




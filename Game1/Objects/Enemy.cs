using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    class Enemy
    {
    private Texture2D texture;
    private Vector2 position = new Vector2(300, 300);
    private Vector2 velocity;
    private Rectangle rectangle; private bool hasJumped = false;

    public void Load(ContentManager Content)
    {
        texture = Content.Load<Texture2D>("SpriteSheet/enemy1_idle");
    }

    public void Update(GameTime gameTime)
    {
        position += velocity;
        rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

        Input();
        if (velocity.Y < 10)
            velocity.Y += 0.4f;
    }

    private void Input()
        {
        if (Keyboard.GetState().IsKeyDown(Keys.D))
            velocity.X = 3;
        else if (Keyboard.GetState().IsKeyDown(Keys.A))
            velocity.X = -3;
        else velocity.X = 0f;

        if (Keyboard.GetState().IsKeyDown(Keys.W) && hasJumped == false)
        {
            position.Y -= 5f;
            velocity.Y = -9f;
            hasJumped = true;
        }

        }

    public void Collision(Rectangle newRectangle, int xOffset, int yOffset)
    {
        if (rectangle.EnemyTouchTop(newRectangle))
        {
            rectangle.Y = newRectangle.Y - rectangle.Height;
            velocity.Y = 0;
            hasJumped = false;
        }

        if (rectangle.EnemyTouchLeft(newRectangle))
        {
            position.X = newRectangle.X - rectangle.Width - 2;
        }

        if (rectangle.EnemyTouchRight(newRectangle))
        {
            position.X = newRectangle.X + newRectangle.Width + 2;
        }
        if (rectangle.EnemyTouchBottom(newRectangle))
        {
            velocity.Y = 0;
        }

        if (position.X < 0)
        {
            position.X = 0;
        }

        if (position.X > xOffset - rectangle.Width)
        {
            position.X = xOffset - rectangle.Width;
        }

        if (position.Y < 0)
        {
            velocity.Y = 0f;
        }

        if (position.Y > yOffset - rectangle.Height)
        {
            position.Y = yOffset - rectangle.Height;
        }

    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(texture, rectangle, Color.White);
    }
}
    

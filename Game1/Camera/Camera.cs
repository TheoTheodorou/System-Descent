using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Camera
{
    private Matrix transform;
    public Matrix Transform
    {
        get { return transform; }
    }

    private Vector2 centre;
    private Viewport viewport;
    public float top_edge;
    public float bottom_edge;

    public Camera(Viewport newViewport)
    {
        viewport = newViewport;
        centre.Y = viewport.Height / 2;
        centre.X = viewport.Width / 2;
    }

    public void Update(Vector2 position, int xOffset, int yOffset, GameTime gameTime,Player player )
    {

        if (gameTime.TotalGameTime.Seconds > 0)
        {
            centre.Y += 0.0005f;
            player.speed = 1;
        }

        if(gameTime.TotalGameTime.Seconds > 25)
        {
            centre.Y += 0.0005f;
            player.speed = 2;
        }

        if (gameTime.TotalGameTime.Seconds > 40)
        {
            centre.Y += 0.0005f;
            player.speed = 3;
        }

        if (gameTime.TotalGameTime.Seconds > 60)
        {
            centre.Y += 0.0005f;
            player.speed = 3;
        }

        if (gameTime.TotalGameTime.Seconds > 80)
        {
            centre.Y += 0.005f;
            player.speed = 3;
        }

        if (gameTime.TotalGameTime.Seconds > 100)
        {
            centre.Y += 0.005f;
            player.speed = 3;
        }


        top_edge = centre.Y - (viewport.Height / 2);
        bottom_edge = centre.Y + 200;

        //if (position.Y < viewport.Height / 2)
        //    centre.Y = viewport.Height / 2;
        //else if (position.Y > yOffset - (viewport.Height / 2))
        //    centre.Y = yOffset - (viewport.Height / 2);

        //else centre.Y = position.Y;

        transform = Matrix.CreateTranslation(new Vector3(-centre.X + (viewport.Width / 2), -centre.Y + (viewport.Height / 2), 0));
    }
}
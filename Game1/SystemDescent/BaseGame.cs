using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

public class BaseGame : Game
{

    protected GraphicsDeviceManager graphics;
    protected SpriteBatch spriteBatch;
    protected InputHandler input;

    // Window width and height
    public int window_width = 1280;
    public int window_height = 720;

    public BaseGame()
    {
        graphics = new GraphicsDeviceManager(this);
        graphics.PreferredBackBufferWidth = 1280;
        graphics.PreferredBackBufferHeight = 720;
        Content.RootDirectory = "Content";


        // The InputHandler class is not part of XNA but has been written by Simon Schofield to help
        // parse user input. The class casn be found in the Solution Explorer
        input = new InputHandler();
    }

    protected override void Initialize()
    {
        this.IsMouseVisible = true;
        base.Initialize();
    }




    protected void quit_window()
    {
        this.Exit();
    }


}



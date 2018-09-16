
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
namespace Game1.ScreenManager
{
    public class TitleScreen : BaseGame
    {
        protected SpriteFont font;
        protected Texture2D button_texture;                         // Texture used for the buttons
        private Texture2D background_texture;                     // Texture used for the background
        protected List<Button> ButtonList = new List<Button>();    // Create a list of the buttons used

        protected override void LoadContent()
        {
            //Song menu = Content.Load<Song>("audio/menu");
            //MediaPlayer.Play(menu);

            // Load the textures
            spriteBatch = new SpriteBatch(GraphicsDevice);
            button_texture = Content.Load<Texture2D>("buttons/button");
            background_texture = Content.Load<Texture2D>("backgrounds/menu");
            font = Content.Load<SpriteFont>("fonts/arial");

            // Create the buttons -------------------------------------------------

            int button_middle = (window_width / 2) - (button_texture.Width / 2); // Define a center for the buttons

            Button button_newGame = new Button();   // Create new game button
            button_newGame.SetButtonData(button_middle,300, "new_game", button_texture, font, "NEW GAME");
            ButtonList.Add(button_newGame);

            Button button_exitGame = new Button();  // Create exit game button
            button_exitGame.SetButtonData(button_middle, 400, "exit", button_texture, font, "EXIT");
            ButtonList.Add(button_exitGame);

            base.LoadContent();
        }

        // Unloading content ------------------

        protected override void UnloadContent()
        {
            this.quit_window();
        }

        protected override void Update(GameTime gameTime)
        {

            // Allows the game to exit ------------------

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            RunningState.set_state(0);
            

            input.Update();                             // Update the inputs
            MouseState myMouseState = Mouse.GetState(); // Updates the mouse state

            //Go through each button currently on screen and change their status ------

            int limit = ButtonList.Count;

            for (int i = 0; i < limit; i++)
            {
                ButtonList[i].UpdateState(myMouseState);   //Update the button state based on where the mouse is pointing

                if (ButtonList[i].isPressed())             //Check if the button is pressed or not, if it is then the program needs to go onto the function list
                {
                    TitleFunctions(ButtonList[i].Get_Name());

                }

            }
            base.Update(gameTime);
        }



        // Determine what happens when a button is pressed ---

        protected void TitleFunctions(string button_ID)
        {
            if (button_ID == "new_game")
            {
                RunningState.setPreviousState(1);
                RunningState.set_state(2); // Continue to the running game screen
                quit_window();
            }
            else if (button_ID == "exit")
            {
                RunningState.set_state(0); // Exit the game
                quit_window();
            }
        }

        // Draw the objects to the screen -

        protected override void Draw(GameTime gameTime)
        {
            // Draw the sprites------------------------

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            spriteBatch.Draw(background_texture, new Rectangle(0, 0, 1280, 720), Color.White);  // Background


            for (int i = 0; i < ButtonList.Count; i++) // Draw every button in the array
            {
                ButtonList[i].DrawButton(spriteBatch);
            }


            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}


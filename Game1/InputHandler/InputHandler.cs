#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
#endregion






public class InputHandler
    {
        /// <summary>
        /// This class gets user input from Keyboard and Mouse, 
        /// it could extended to get input from the game pad too
        /// </summary>
        private KeyboardState prevKeyboardState;
        private KeyboardState keyboardState;

        private MouseState prevMouseState;
        private MouseState mouseState;
        private int mouseX, mouseY;

        public InputHandler()  
        {
          prevKeyboardState = Keyboard.GetState();
          prevMouseState = Mouse.GetState();
        }

        // keyboard stuff
        public bool IsKeyDown(Keys key)
        {
            return (keyboardState.IsKeyDown(key));
        }

        public bool IsHoldingKey(Keys key)
        {
            return (keyboardState.IsKeyDown(key) &&
                prevKeyboardState.IsKeyDown(key));
        }

        public bool WasKeyPressed(Keys key)
        {
            return (keyboardState.IsKeyDown(key) &&
                prevKeyboardState.IsKeyUp(key));
        }

        public bool HasReleasedKey(Keys key)
        {
            return (keyboardState.IsKeyUp(key) &&
                prevKeyboardState.IsKeyDown(key));
        }

        // mouse stuff
        public Vector2 getMousePos()
        {
            Vector2 v;
            v.X = mouseX;
            v.Y = mouseY;
            return v;
        }

        public bool isMouseButtonDown()
        {
            // returns the state of the left mouse button
            if (mouseState.LeftButton == ButtonState.Pressed) { return true; }

            return false;

        }

        public bool isMouseButtonClick()
        {
           // return true only directly after a mouse down click
           if(mouseState.LeftButton == ButtonState.Pressed &&
              prevMouseState.LeftButton == ButtonState.Released ) return true;
           return false;

        }

        // the update method, must be called from the main game.Update function
        public void Update()
        {
            //set our previous state to our new state
            prevKeyboardState = keyboardState;

            //get our new state
            keyboardState = Keyboard.GetState();

            prevMouseState = mouseState;
            mouseState = Mouse.GetState();
            mouseX = mouseState.X;
            mouseY = mouseState.Y;
        }
 

    }

   



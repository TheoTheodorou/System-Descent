using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Button
{
    private enum myState { Pressed, Hover, Nothing }    // Enumerator used to assign a set of constants to a list
    private myState myButtonState;                      // Holds the current state of the button

    private Texture2D texture;                   // The texture for the button
    private SpriteFont font;                     // Font used on the button

    private Rectangle rectangle;                 // Define a rectangle area for the button hitbox

    private string name;                         // Name of the button, which is used to determine what happens after the button is clicked
    private string text;                         // The text that may or may not be on the button
    private int width;
    private int height;
    private Vector2 position;
    private Color color;

    // Returns the current state of the button ---------------------------------------------------------

    private myState Get_State()
    {
        return myButtonState;
    }

    // Returns the name of the button ------------------------------------------------------------------

    public string Get_Name()
    {
        return name;
    }

    // Initialise values of the button with the data passed to it --------------------------------------

    public void SetButtonData(int input_x, int input_y, string input_name, Texture2D input_texture, SpriteFont input_font, string input_text)
    {
        name = input_name;
        text = input_text;
        font = input_font;
        texture = input_texture;
        height = texture.Height;
        width = texture.Width;
        position.X = input_x;
        position.Y = input_y;

        // Create a rectangle area to compare with mouse click

        rectangle.X = (int)position.X;
        rectangle.Y = (int)position.Y;
        rectangle.Height = (int)texture.Height;
        rectangle.Width = (int)texture.Width;
    }

    // Check to see if the button is pressed ----------------

    public bool isPressed()
    {
        if (Get_State() == myState.Pressed)
            return true;
        return false;
    }

    // Update the state of the button -----------------------

    public void UpdateState(MouseState input_mouse_state)
    {
        if (rectangle.Contains(input_mouse_state.Position.X, input_mouse_state.Position.Y)) // If cursor is over button
        {
            if (input_mouse_state.LeftButton == ButtonState.Pressed) // If button is left clicked
            {
                myButtonState = myState.Pressed;
                color = Color.Red;
            }
            else // Cursor is over button, but not clicked
            {
                myButtonState = myState.Hover;
                color = Color.DarkCyan;
            }
        }
        else
        {
            myButtonState = myState.Nothing;
            color = Color.Cyan;
        }
    }

    // Function to draw a button -------------------------

    public void DrawButton(SpriteBatch input_s)
    {
        input_s.Draw(texture, rectangle, color); // Draw button
        input_s.DrawString(font, text, CenterButtonText(), Color.Orange); // Draw the text on the button
    }

    // Function to center the text to be written on the button -----------------------

    private Vector2 CenterButtonText()
    {
        Vector2 text_position;
        Vector2 size = font.MeasureString(text);

        text_position.X = position.X + (rectangle.Width * 0.5f) - (size.X * 0.5f);
        text_position.Y = position.Y + (rectangle.Height * 0.5f) - (size.Y * 0.5f);

        return text_position;
    }
}
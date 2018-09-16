#region Using Statements
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
#endregion

// AnimatedSprite.  This class  handles the animation and drawing of a 2D animation
// based on a single imported texture, which is a single horizontal strip of 
// sequential images (cells). AnimatedSprite expects the cells of the sprite strip to be 
// square; the class then calculates the number of cells in the strip based ion the strip's pixel height
// and length 

public class AnimatedSpriteStrip
{
    // The tiled image from which we animate
    private Texture2D myCellsTexture;

    // Duration of time to show each frame.
    private float myFrameTime;

    //  is it looping... probably!
    private bool myIsLooping;

    // The amount of time in seconds that the current frame has been shown for.
    private float elapsedFrameTime;

    // The actual cell being addressed at this GameTime (0... numCells-1) 
    private int myFrameIndex;

    // counts from 0 to everupwards as the object lives on
    private int myFrameCounter;

    public Vector2 position;
    public Rectangle rectangle;
    private SpriteEffects mySpriteEffects;
    private float myDrawingDepth;

    public string myName;

    public AnimatedSpriteStrip(Texture2D texture, float frameTime, bool isLooping)
    {
        myCellsTexture = texture;
        myFrameTime = frameTime;
        myIsLooping = isLooping;
        elapsedFrameTime = 0.0f;
        myFrameIndex = 0;
        myFrameCounter = 0;
        mySpriteEffects = SpriteEffects.None;
        myDrawingDepth = 0.5f;
    }

    public void setName(string actionName)
    {
        myName = actionName;
    }

    public int FrameCount()
    {
        return myCellsTexture.Width / myCellsTexture.Height;
    }

    private Vector2 Origin()
    {
        return new Vector2(myCellsTexture.Height / 2.0f, myCellsTexture.Height / 2.0f);
    }


    public void setSpriteEffect(SpriteEffects se)
    {
        mySpriteEffects = se;
    }


    public void setDrawingDepth(float z)
    {
        myDrawingDepth = z;
    }


    public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Player player)
    {

        // Process passing time. ElapsedGameTime returns the amount of time elapsed since the last Update
        elapsedFrameTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (elapsedFrameTime > myFrameTime)
        {
            // Advance the frame index; looping or clamping as appropriate.
            myFrameCounter++;

            if (myIsLooping)
            {
                myFrameIndex = myFrameCounter % FrameCount();
            }
            else
            {
                // freezes on the last frame
                myFrameIndex = Math.Min(myFrameCounter, FrameCount() - 1);

            }

            elapsedFrameTime = 0.0f;
        }

        // Calculate the source rectangle of the current frame
        int cellWidth = myCellsTexture.Height;
        int leftMostPixel = myFrameIndex * cellWidth;
        Rectangle sourceRect = new Rectangle(leftMostPixel, 0, cellWidth, cellWidth);

        // Draw the current frame.
        // (bigTexture, posOnScreen, sourceRect in big texture, col, rotation, origin, scale, effect, depth)
        Vector2 orig = Origin();
        Vector2 myPosition = player.position;
        spriteBatch.Draw(myCellsTexture, myPosition, sourceRect, Color.White, 0.0f, orig, 1.0f, mySpriteEffects, myDrawingDepth);
    }


}
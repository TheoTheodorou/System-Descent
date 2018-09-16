using Game1.ScreenManager;
using System;
#if WINDOWS || LINUX

// Main -------------------

public static class Program
{
    [STAThread]

    static void Main()
    {
        ScreenSelect Screenselect = new ScreenSelect(); //Create an instance of ScreenSelect
    }
}


// Control which screen to run depending on the state of the game --------------------------

public class ScreenSelect
{
    public ScreenSelect()
    {
        RunningState.set_state(1); //Set to 1 for title

        do
        {
            switch (RunningState.get_state())
            {
                case (1): //1 for title screen
                    using (var game = new TitleScreen())
                        game.Run();
                    break;
                case (2): //2 for game screen
                    using (var game = new GameScreen())
                        game.Run();
                    break;
                case (3): //3 for death screen
                    using (var game = new DeathScreen())
                        game.Run();
                    break;
            }
        } while (RunningState.get_state() != 0); //Run the game until the running state is at 0, where the game will exit
    }
}

// Global value used to control the game depending on which state is is in ----------------------------------------------

public static class RunningState
{
    private static int state_ID; // Initialise state
    public static int previous_state;
    public static int score;

    //Set game state------------------------------------------

    public static void set_state(int state)
    {
        state_ID = state;
    }

    //Get game state-----------------------

    public static int get_state()
    {
        return state_ID;
    }

    public static void setPreviousState(int state)
    {
        previous_state = state;
    }

    public static int getPreviousState()
    {
        return previous_state;
    }

    public static void setScore(int input_score)
    {
        score = input_score;
    }

    public static int getScore()
    {
        return score;
    }
}
#endif


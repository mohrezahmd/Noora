using System;

public class Test { }

public class Game
{
    // Predefined event using Action with an integer parameter
    public event Action<int> ScoreUpdated;

    private int playerScore;

    public void UpdateScore(int newScore)
    {
        playerScore += newScore;

        // Raise the ScoreUpdated event when the score changes
        ScoreUpdated?.Invoke(playerScore);
    }
}

public class UI
{
    public void OnScoreUpdated(int newScore)
    {
        Console.WriteLine($"UI: Player's score updated to {newScore}");
    }
}

public class Program
{
    public static void Main()
    {
        Game game = new Game();
        UI ui = new UI();

        // Subscribe the UI's method to the ScoreUpdated event
        game.ScoreUpdated += ui.OnScoreUpdated;

        // Simulate score updates
        game.UpdateScore(10);
        game.UpdateScore(5);

        // Unsubscribe from the event
        game.ScoreUpdated -= ui.OnScoreUpdated;

        // This won't trigger the UI's method
        game.UpdateScore(8);
    }
}
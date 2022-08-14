
public class GameEngine {
    public GameBoard GameBoard;
    public List<Player> Players;

    public GameEngine()
    {
        GameBoard = GameBoard.Setup();
        Players = Player.Setup(GameBoard.RedSietch, GameBoard.YellowSietch, GameBoard.BlueSietch, GameBoard.GreenSietch);
    }

    public void Play()
    {
        var random = new Random();

        var firstPlayer = Players[random.Next(0, 4)];

        Console.WriteLine($"First player is {firstPlayer.Name}");

        var rollResult = RollToMove();

        Console.WriteLine($"Roll: {rollResult.Item1}, {rollResult.Item2}");

        var possibleMoves = GameBoard.GetPossibleMoves(firstPlayer.Characters, rollResult);

        Console.WriteLine("Possible moves:");
        foreach (var move in possibleMoves)
        {
            Console.WriteLine($"Character: {move.Item1.Name}, Board space: {move.Item2.Name}");
        }

        Console.WriteLine("Done");
    }

    public (int, int) RollToMove()
    {
        var random = new Random();

        return (random.Next(1, 7), random.Next(1, 7));
    }
}

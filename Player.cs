public class Player {
    public string Name { get; set; }
    public List<Character> Characters {get;set;}
    public int Spice {get;set;}
    public int Harvesters {get;set;}
    public Player? NextPlayer { get; set; }

    public static List<Player> Setup(BoardSpace redSietch, BoardSpace yellowSietch, BoardSpace blueSietch, BoardSpace greenSietch)
    {
        var player1 = new Player
        {
            Name = "Player 1",
            Spice = 6,
            Harvesters = 3,
            Characters = Character.SetupAtreides(redSietch)
        };

        var player2 = new Player
        {
            Name = "Player 2",
            Spice = 6,
            Harvesters = 3,
            Characters = Character.SetupEmpire(yellowSietch)
        };

        var player3 = new Player
        {
            Name = "Player 3",
            Spice = 6,
            Harvesters = 3,
            Characters = Character.SetupFremen(blueSietch)
        };

        var player4 = new Player
        {
            Name = "Player 4",
            Spice = 6,
            Harvesters = 3,
            Characters = Character.SetupHarkonnen(greenSietch)
        };

        player1.NextPlayer = player2;
        player2.NextPlayer = player3;
        player3.NextPlayer = player4;
        player4.NextPlayer = player1;

        return new List<Player> { player1, player2, player3, player4 };
    }
}

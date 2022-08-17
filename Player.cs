public class Player {
    public Player()
    {
        Spice = 6;
        Harvesters = 3;
        Cards = new List<Card>();
    }

    public string Name { get; set; }
    public List<Character> Characters {get;set;}
    public int Spice {get;set;}
    public int Harvesters {get;set;}
    public List<Card> Cards { get; set; }

    public static List<Player> Setup(BoardSpace redSietch, BoardSpace yellowSietch, BoardSpace blueSietch, BoardSpace greenSietch)
    {
        var player1 = new Player
        {
            Name = "Player 1",
            Characters = Character.SetupAtreides(redSietch)
        };

        var player2 = new Player
        {
            Name = "Player 2",
            Characters = Character.SetupEmpire(yellowSietch)
        };

        var player3 = new Player
        {
            Name = "Player 3",
            Characters = Character.SetupFremen(blueSietch)
        };

        var player4 = new Player
        {
            Name = "Player 4",
            Characters = Character.SetupHarkonnen(greenSietch)
        };

        return new List<Player> { player1, player2, player3, player4 };
    }
}

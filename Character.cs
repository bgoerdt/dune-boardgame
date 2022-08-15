public class Character {
    public string Name {get;set;}
    public Team Team {get;set;}
    public int Strength {get;set; }
    public int InitialStrength { get; set; }
    public int Guile {get;set;}
    public BoardSpace CurrentBoardSpace {get;set;}

    public bool IsInSietch =>
            Team == Team.ATREIDES && CurrentBoardSpace.Name == "Red Sietch"
                || Team == Team.EMPIRE && CurrentBoardSpace.Name == "Yellow Sietch"
                || Team == Team.FREMEN && CurrentBoardSpace.Name == "Blue Sietch"
                || Team == Team.HARKONNEN && CurrentBoardSpace.Name == "Green Sietch";

    public static List<Character> SetupAtreides(BoardSpace redSietch) {
        return new List<Character> {
            new Character {
                Name = "Paul Atreides",
                Team = Team.ATREIDES,
                Guile = 3,
                InitialStrength = 4,
                Strength = 4,
                CurrentBoardSpace = redSietch
            },
            new Character
            {
                Name = "Duke Leto Atreides",
                Team = Team.ATREIDES,
                Guile = 4,
                InitialStrength = 3,
                Strength = 3,
                CurrentBoardSpace = redSietch
            },
            new Character
            {
                Name = "Gurney Halleck",
                Team = Team.ATREIDES,
                Guile = 3,
                InitialStrength = 5,
                Strength = 5,
                CurrentBoardSpace = redSietch
            }
        };
    }

    public static List<Character> SetupFremen(BoardSpace blueSietch)
    {
        return new List<Character> {
            new Character
            {
                Name = "Chani",
                Team = Team.FREMEN,
                Guile = 3,
                InitialStrength = 4,
                Strength = 4,
                CurrentBoardSpace = blueSietch
            },
            new Character
            {
                Name = "Dr. Kynes",
                Team = Team.FREMEN,
                Guile = 4,
                InitialStrength = 3,
                Strength = 3,
                CurrentBoardSpace = blueSietch
            },
            new Character
            {
                Name = "Stilgar",
                Team = Team.FREMEN,
                Guile = 3,
                InitialStrength = 5,
                Strength = 5,
                CurrentBoardSpace = blueSietch
            }
        };
    }

    public static List<Character> SetupHarkonnen(BoardSpace greenSietch)
    {
        return new List<Character> {
            new Character
            {
                Name = "Feyd Rabban",
                Team = Team.HARKONNEN,
                Guile = 3,
                InitialStrength = 4,
                Strength = 4,
                CurrentBoardSpace = greenSietch
            },
            new Character
            {
                Name = "Baron Harkonnen",
                Team = Team.HARKONNEN,
                Guile = 4,
                InitialStrength = 3,
                Strength = 3,
                CurrentBoardSpace = greenSietch
            },
            new Character
            {
                Name = "Beast Rabban",
                Team = Team.HARKONNEN,
                Guile = 3,
                InitialStrength = 5,
                Strength = 5,
                CurrentBoardSpace = greenSietch
            }
        };
    }


    public static List<Character> SetupEmpire(BoardSpace yellowSietch)
    {
        return new List<Character> {
            new Character
            {
                Name = "Princess Irulan",
                Team = Team.EMPIRE,
                Guile = 3,
                InitialStrength = 4,
                Strength = 4,
                CurrentBoardSpace = yellowSietch
            },
            new Character
            {
                Name = "Emperor Shaddam IV",
                Team = Team.EMPIRE,
                Guile = 4,
                InitialStrength = 3,
                Strength = 3,
                CurrentBoardSpace = yellowSietch
            },
            new Character
            {
                Name = "Sardaukar Warrior",
                Team = Team.EMPIRE,
                Guile = 3,
                InitialStrength = 5,
                Strength = 5,
                CurrentBoardSpace = yellowSietch
            }
        };
    }
}

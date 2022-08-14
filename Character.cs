public class Character {
    public Character()
    {
        Strength = InitialStrength;
    }

    public string Name {get;set;}
    public Team Team {get;set;}
    public int Strength {get;set; }
    public int InitialStrength { get; set; }
    public int Guile {get;set;}
    public BoardSpace CurrentBoardSpace {get;set;}

    public static List<Character> SetupAtreides(BoardSpace redSietch) {
        return new List<Character> {
            new Character{
                Name = "Paul Atreides",
                Guile = 3,
                InitialStrength = 4,
                CurrentBoardSpace = redSietch
            },
            new Character
            {
                Name = "Duke Leto Atreides",
                Guile = 4,
                InitialStrength = 3,
                CurrentBoardSpace = redSietch
            },
            new Character
            {
                Name = "Gurney Halleck",
                Guile = 3,
                InitialStrength = 5,
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
                Guile = 3,
                InitialStrength = 4,
                CurrentBoardSpace = blueSietch
            },
            new Character
            {
                Name = "Dr. Kynes",
                Guile = 4,
                InitialStrength = 3,
                CurrentBoardSpace = blueSietch
            },
            new Character
            {
                Name = "Stilgar",
                Guile = 3,
                InitialStrength = 5,
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
                Guile = 3,
                InitialStrength = 4,
                CurrentBoardSpace = greenSietch
            },
            new Character
            {
                Name = "Baron Harkonnen",
                Guile = 4,
                InitialStrength = 3,
                CurrentBoardSpace = greenSietch
            },
            new Character
            {
                Name = "Beast Rabban",
                Guile = 3,
                InitialStrength = 5,
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
                Guile = 3,
                InitialStrength = 4,
                CurrentBoardSpace = yellowSietch
            },
            new Character
            {
                Name = "Emperor Shaddam IV",
                Guile = 4,
                InitialStrength = 3,
                CurrentBoardSpace = yellowSietch
            },
            new Character
            {
                Name = "Sardaukar Warrior",
                Guile = 3,
                InitialStrength = 5,
                CurrentBoardSpace = yellowSietch
            }
        };
    }
}

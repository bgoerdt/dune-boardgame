public class PossibleMove : IEquatable<PossibleMove>
{
    public Character Character { get; set; }
    public BoardSpace BoardSpace { get; set; }
    public bool UsesBothDie { get; set; }
    public int DieValue { get; set; }

    public bool Equals(PossibleMove? other)
    {
        return Character == other?.Character &&
            BoardSpace == other?.BoardSpace &&
            UsesBothDie == other?.UsesBothDie &&
            DieValue == other?.DieValue;
    }
}

public class GameBoard {
    public List<BoardSpace> BoardSpaces {get;set;}
    public BoardSpace RedSietch;
    public BoardSpace YellowSietch;
    public BoardSpace BlueSietch;
    public BoardSpace GreenSietch;

    public List<PossibleMove> GetPossibleMoves(List<Character> characters, (int, int) diceRoll)
    {
        var possibleMoves = new List<PossibleMove>();

        foreach (var character in characters)
        {
            possibleMoves.AddRange(
                GetPossibleMoves(character.CurrentBoardSpace, diceRoll.Item1)
                .Select(move => new PossibleMove { Character = character, BoardSpace = move, DieValue = diceRoll.Item1, UsesBothDie = false}));

            possibleMoves.AddRange(
                GetPossibleMoves(character.CurrentBoardSpace, diceRoll.Item2)
                .Select(move => new PossibleMove { Character = character, BoardSpace = move, DieValue = diceRoll.Item2, UsesBothDie = false }));

            possibleMoves.AddRange(
                GetPossibleMoves(character.CurrentBoardSpace, diceRoll.Item1 + diceRoll.Item2)
                .Select(move => new PossibleMove { Character = character, BoardSpace = move, DieValue = diceRoll.Item1 + diceRoll.Item2, UsesBothDie = true }));
        }

        return possibleMoves.Distinct().ToList();
    }

    private List<BoardSpace> GetPossibleMoves(BoardSpace currentBoardSpace, int spacesLeftToMove, List<BoardSpace>? accumulatedPossibleMoves = null, BoardSpace? previousBoardSpace = null)
    {
        accumulatedPossibleMoves ??= new List<BoardSpace>();

        if (spacesLeftToMove == 0)
        {
            accumulatedPossibleMoves.Add(currentBoardSpace);

            return accumulatedPossibleMoves;
        }

        if (currentBoardSpace.AltNext != null && previousBoardSpace != currentBoardSpace.AltNext)
        {
            GetPossibleMoves(currentBoardSpace.AltNext, spacesLeftToMove - 1, accumulatedPossibleMoves, currentBoardSpace);
        }

        GetPossibleMoves(currentBoardSpace.Next, spacesLeftToMove - 1, accumulatedPossibleMoves, currentBoardSpace);

        return accumulatedPossibleMoves;
    }

    public static GameBoard Setup() {
        var redSietch = new BoardSpace { Name = "Red Sietch", Type = BoardSpaceType.SIETCH };
        var yellowSietch = new BoardSpace { Name = "Yellow Sietch", Type = BoardSpaceType.SIETCH };
        var blueSietch = new BoardSpace { Name = "Blue Sietch", Type = BoardSpaceType.SIETCH };
        var greenSietch = new BoardSpace { Name = "Green Sietch", Type = BoardSpaceType.SIETCH };

        var spice1 = new BoardSpace { Name = "Spice (Red)", Type = BoardSpaceType.DESERT };
        var spice2 = new BoardSpace { Name = "Spice (Red/Yellow)", Type = BoardSpaceType.DESERT };
        var spice3 = new BoardSpace { Name = "Spice (Yellow)", Type = BoardSpaceType.DESERT };
        var spice4 = new BoardSpace { Name = "Spice (Yellow/Blue)", Type = BoardSpaceType.DESERT };
        var spice5 = new BoardSpace { Name = "Spice (Blue)", Type = BoardSpaceType.DESERT };
        var spice6 = new BoardSpace { Name = "Spice (Blue/Green)", Type = BoardSpaceType.DESERT };
        var spice7 = new BoardSpace { Name = "Spice (Green)", Type = BoardSpaceType.DESERT };
        var spice8 = new BoardSpace { Name = "Spice (Green/Red)", Type = BoardSpaceType.DESERT };

        var sandStorm = new BoardSpace { Name = "Sand Storm", Type = BoardSpaceType.DESERT };

        var worm1 = new BoardSpace { Name = "Worm (Yellow)", Type = BoardSpaceType.DESERT };
        var worm2 = new BoardSpace { Name = "Worm (Blue)", Type = BoardSpaceType.DESERT };
        var worm3 = new BoardSpace { Name = "Worm (Green)", Type = BoardSpaceType.DESERT };
        var worm4 = new BoardSpace { Name = "Worm (Red)", Type = BoardSpaceType.DESERT };

        var duel1 = new BoardSpace { Name = "Duel (Yellow/Blue)", Type = BoardSpaceType.DESERT };
        var duel2 = new BoardSpace { Name = "Duel (Blue/Green)", Type = BoardSpaceType.DESERT };
        var duel3 = new BoardSpace { Name = "Duel (Green/Red)", Type = BoardSpaceType.DESERT };

        var training1 = new BoardSpace { Name = "Training (Red)", Type = BoardSpaceType.CASTLE };
        var training2 = new BoardSpace { Name = "Training (Yellow)", Type = BoardSpaceType.CASTLE };
        var training3 = new BoardSpace { Name = "Training (Blue)", Type = BoardSpaceType.CASTLE };
        var training4 = new BoardSpace { Name = "Training (Green)", Type = BoardSpaceType.CASTLE };

        var smuggler = new BoardSpace { Name = "Smuggler", Type = BoardSpaceType.CASTLE };

        var spiceRaid1 = new BoardSpace { Name = "Spice Raid (Red/Yellow)", Type = BoardSpaceType.CASTLE };
        var spiceRaid2 = new BoardSpace { Name = "Spice Raid (Green/Red)", Type = BoardSpaceType.CASTLE };

        var traitor = new BoardSpace { Name = "Traitor", Type = BoardSpaceType.CASTLE };

        var poison1 = new BoardSpace { Name = "Poison (Blue/Yellow)", Type = BoardSpaceType.CASTLE };
        var poison2 = new BoardSpace { Name = "Poison (Green/Blue)", Type = BoardSpaceType.CASTLE };

        var beneGesserit = new BoardSpace { Name = "Bene Gesserit", Type = BoardSpaceType.CASTLE };

        var spaceGuild = new BoardSpace { Name = "Space Guild", Type = BoardSpaceType.CASTLE };

        redSietch.Next = spice1;
        redSietch.AltNext = training1;
        spice1.Next = sandStorm;
        sandStorm.Next = spice2;
        spice2.Next = worm1;
        worm1.Next = yellowSietch;
        yellowSietch.Next = spice3;
        yellowSietch.AltNext = training2;
        spice3.Next = duel1;
        duel1.Next = spice4;
        spice4.Next = worm2;
        worm2.Next = blueSietch;
        blueSietch.Next = spice5;
        blueSietch.AltNext = training3;
        spice5.Next = duel2;
        duel2.Next = spice6;
        spice6.Next = worm3;
        worm3.Next = greenSietch;
        greenSietch.Next = spice7;
        greenSietch.AltNext = training4;
        spice7.Next = duel3;
        duel3.Next = spice8;
        spice8.Next = worm4;
        worm4.Next = redSietch;

        training1.Next = smuggler;
        training1.AltNext = redSietch;
        smuggler.Next = spiceRaid1;
        spiceRaid1.Next = training2;
        training2.Next = traitor;
        training2.AltNext = yellowSietch;
        traitor.Next = poison1;
        poison1.Next = training3;
        training3.Next = beneGesserit;
        training3.AltNext = blueSietch;
        beneGesserit.Next = poison2;
        poison2.Next = training4;
        training4.Next = spaceGuild;
        training4.AltNext = greenSietch;
        spaceGuild.Next = spiceRaid2;
        spiceRaid2.Next = training1;

        return new GameBoard
        {
            BoardSpaces = new List<BoardSpace> {
                redSietch,
                yellowSietch,
                blueSietch,
                greenSietch,
                spice1,
                spice2,
                spice3,
                spice4,
                spice5,
                spice6,
                spice7,
                spice8,
                sandStorm,
                worm1,
                worm2,
                worm3,
                worm4,
                duel1,
                duel2,
                duel3,
                training1,
                training2,
                training3,
                training4,
                smuggler,
                spiceRaid1,
                spiceRaid2,
                traitor,
                poison1,
                poison2,
                beneGesserit,
                spaceGuild
            },
            RedSietch = redSietch,
            YellowSietch = yellowSietch,
            BlueSietch = blueSietch,
            GreenSietch = greenSietch
        };
    }
}
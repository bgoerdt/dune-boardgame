
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

        var currentPlayer = Players[random.Next(0, 4)];

        while (true)
        {
            Console.WriteLine($"Current player is {currentPlayer.Name}");

            var rollResult = RollToMove();

            Console.WriteLine($"Roll: {rollResult.Item1}, {rollResult.Item2}");

            var possibleFirstMoves = GameBoard.GetPossibleMoves(currentPlayer.Characters, rollResult);

            Console.WriteLine("Possible first moves:");
            foreach (var possibleMove in possibleFirstMoves)
            {
                Console.WriteLine($"Character: {possibleMove.Character.Name}, Board space: {possibleMove.BoardSpace.Name}, die used: {possibleMove.DieValue}, uses both die: {possibleMove.UsesBothDie}");
            }

            var firstMove = possibleFirstMoves[random.Next(0, possibleFirstMoves.Count)];

            Console.WriteLine($"{firstMove.Character.Name} moves {firstMove.DieValue} spaces to {firstMove.BoardSpace.Name}.");
            MoveCharacter(currentPlayer, firstMove.Character, firstMove.BoardSpace);

            if (IsWinner())
            {
                break;
            }

            if (!firstMove.UsesBothDie && currentPlayer.Characters.Count > 1)
            {
                var possibleSecondMoves = possibleFirstMoves
                    .Where(possibleMove => possibleMove.Character != firstMove.Character && possibleMove.DieValue != firstMove.DieValue)
                    .ToList();

                Console.WriteLine("Possible second moves:");
                foreach (var possibleMove in possibleSecondMoves)
                {
                    Console.WriteLine($"Character: {possibleMove.Character.Name}, Board space: {possibleMove.BoardSpace.Name}, die used: {possibleMove.DieValue}");
                }

                var secondMove = possibleSecondMoves[random.Next(0, possibleSecondMoves.Count)];

                MoveCharacter(currentPlayer, secondMove.Character, secondMove.BoardSpace);
            }

            if (IsWinner())
            {
                break;
            }

            currentPlayer = GetNextPlayer(currentPlayer);
        }

        Console.WriteLine("Game Over");
    }

    public Player GetNextPlayer(Player currentPlayer)
    {
        var playerIndex = Players.IndexOf(currentPlayer);

        var nextPlayer = playerIndex == Players.Count - 1 ? Players[0] : Players[playerIndex + 1];

        if (nextPlayer.Characters.Count == 0)
        {
            return GetNextPlayer(nextPlayer);
        }

        return nextPlayer;
    }

    public bool IsWinner()
    {
        var playersWithCharacters = Players.Where(player => player.Characters.Count != 0).ToList();

        if (playersWithCharacters.Count == 1)
        {
            Console.WriteLine($"{playersWithCharacters.First().Name} wins");
            return true;
        }

        return false;
    }

    public (int, int) RollToMove()
    {
        var random = new Random();

        return (random.Next(1, 7), random.Next(1, 7));
    }

    public void MoveCharacter(Player player, Character character, BoardSpace boardSpace)
    {
        character.CurrentBoardSpace = boardSpace;

        if (boardSpace.Name.StartsWith("Spice") && !boardSpace.Name.StartsWith("Spice Raid"))
        {
            player.Spice += player.Harvesters;

            Console.WriteLine($"Gained {player.Harvesters} Spice, now has {player.Spice} Spice");
        }
        else if (boardSpace.Name.StartsWith("Training"))
        {
            character.Strength++;

            Console.WriteLine($"{character.Name} gained 1 Strength, now has {character.Strength} Strength");
        }
        else if (boardSpace.Name == "Smuggler")
        {
            var random = new Random();

            var roll = random.Next(1, 9);
            player.Spice += roll;

            Console.WriteLine($"Smugger rolled and gained {roll} Spice, now has {player.Spice} Spice");
        }
        else if (boardSpace.Name.StartsWith("Duel"))
        {
            var charactersToDuel = GetCharactersToDuel(player);

            if (charactersToDuel.Count == 0)
            {
                Console.WriteLine("No characters to duel");
            }
            else
            {
                // TODO: player picks character to duel
                var random = new Random();
                var randomCharacterToDuel = charactersToDuel[random.Next(0, charactersToDuel.Count)];

                // TODO: implement rolls
                var whoWins = random.Next(0, 2);

                var characterToDie = whoWins == 1 ? character : randomCharacterToDuel;

                KillCharacter(characterToDie);
                Console.WriteLine($"{characterToDie.Name} lost duel and is now dead.");
            }
        }
        else if (boardSpace.Name.StartsWith("Worm"))
        {
            var charactersToAttack = GetCharactersForDesertAttack(player);

            if (charactersToAttack.Count == 0)
            {
                Console.WriteLine("No characters to attack by worm");
            }
            else
            {
                // TODO: player picks character to attack
                var random = new Random();
                var randomCharacterToAttack = charactersToAttack[random.Next(0, charactersToAttack.Count)];

                // TODO: implement harvesters vs player worm attack choice
                Console.WriteLine($"{randomCharacterToAttack.Name} is attacked by a worm");
                ExecuteDesertAttack(randomCharacterToAttack);
            }
        }
        else if (boardSpace.Name == "Sand Storm")
        {
            var charactersToAttack = GetCharactersForDesertAttack(player);

            if (charactersToAttack.Count == 0)
            {
                Console.WriteLine("No characters to attack by Sand Storm");
            }
            else
            {
                // TODO: implement harvester attack
                foreach (var characterToAttack in charactersToAttack)
                {
                    Console.WriteLine($"{characterToAttack.Name} is attacked by a Sand Storm");
                    ExecuteDesertAttack(characterToAttack);
                }
            }
        }
        else
        {
            Console.WriteLine("Board Space not yet supported");
        }
    }

    public void ExecuteDesertAttack(Character character)
    {
        var random = new Random();

        var roll = random.Next(0, 7);

        Console.WriteLine($"Rolled {roll} against {character.Name} Guile of {character.Guile}.");

        if (roll > character.Guile)
        {
            KillCharacter(character);
            Console.WriteLine($"{character.Name} does not survive.");
        }
        else
        {
            Console.WriteLine($"{character.Name} survives.");
        }
    }

    public void KillCharacter(Character character)
    {
        foreach (var player in Players)
        {
            if (player.Characters.Contains(character))
            {
                player.Characters.Remove(character);
                break;
            }
        }
    }

    public List<Character> GetCharactersToDuel(Player attackingPlayer)
    {
        return Players.Where(player => player != attackingPlayer)
            .SelectMany(player => player.Characters)
            .Where(character => !character.IsInSietch)
            .ToList();
    }

    public List<Character> GetCharactersForDesertAttack(Player attackingPlayer)
    {
        return Players.Where(player => player != attackingPlayer)
            .SelectMany(player => player.Characters)
            .Where(character => character.CurrentBoardSpace.Type == BoardSpaceType.DESERT)
            .ToList();
    }
}

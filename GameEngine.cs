public class GameEngine {
    public GameBoard GameBoard;
    public List<Player> Players;
    public CardDeck CardDeck;

    public GameEngine()
    {
        GameBoard = GameBoard.Setup();
        Players = Player.Setup(GameBoard.RedSietch, GameBoard.YellowSietch, GameBoard.BlueSietch, GameBoard.GreenSietch);
        CardDeck = new CardDeck();
    }

    public void Play()
    {
        var random = new Random();

        var currentPlayer = Players[random.Next(0, 4)];

        while (true)
        {
            Console.WriteLine($"Current player is {currentPlayer.Name}");

            // MOVE
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

                Console.WriteLine($"{secondMove.Character.Name} moves {secondMove.DieValue} spaces to {secondMove.BoardSpace.Name}.");
                MoveCharacter(currentPlayer, secondMove.Character, secondMove.BoardSpace);
            }

            if (IsWinner())
            {
                break;
            }

            // FIGHT

            // BUY
            var harvestersToBuy = Math.Min(random.Next(0, 4), currentPlayer.Spice);

            currentPlayer.Harvesters += harvestersToBuy;
            currentPlayer.Spice -= harvestersToBuy;

            Console.WriteLine($"Bought {harvestersToBuy} harvesters. Now has {currentPlayer.Harvesters} harvesters and {currentPlayer.Spice} spice");

            var cardsToBuy = Math.Min(Math.Min(random.Next(0, 4), currentPlayer.Spice), CardDeck.CardCount);

            currentPlayer.Spice -= cardsToBuy;

            var drawnCards = CardDeck.Draw(cardsToBuy);

            Console.WriteLine($"Bought {cardsToBuy} cards. Now has {currentPlayer.Spice} spice. Cards drawn: {string.Join(", ", drawnCards.Select(c => c.Name))}");

            var equipmentCards = drawnCards.Where(c => c.Type == CardType.EQUIPMENT).ToList();
            var unassignedCards = drawnCards.Where(c => c.Type == CardType.KANLY).ToList();

            if (equipmentCards.Count > 0)
            {
                foreach (var equipmentCard in equipmentCards)
                {
                    if (!TryAssignEquipmentCard(currentPlayer.Characters, equipmentCard))
                    {
                        unassignedCards.Add(equipmentCard);
                    }
                }
            }

            currentPlayer.Cards.AddRange(unassignedCards);

            // INVEST

            currentPlayer = GetNextPlayer(currentPlayer);
        }

        Console.WriteLine("Game Over");
    }

    public bool TryAssignEquipmentCard(List<Character> characters, Card equipmentCard)
    {
        foreach (var character in characters)
        {
            if (character.EquipmentCards.Any(card => card.Name == equipmentCard.Name))
            {
                return false;
            }

            character.EquipmentCards.Add(equipmentCard);
            Console.WriteLine($"{equipmentCard.Name} assigned to {character.Name}");
            return true;
        }

        Console.WriteLine($"{equipmentCard.Name} could not be assigned");
        return false;
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
        if (boardSpace.Name.StartsWith("Spice Raid"))
        {
            var random = new Random();

            // TODO: implement choose opponent
            var playerToRaid = GetRandomActiveOtherPlayer(player);

            var opponentRoll = random.Next(1, 7);
            var raidRoll = random.Next(1, 9);

            Console.WriteLine($"{playerToRaid.Name} is being raided and rolls {opponentRoll}. {player.Name} rolls {raidRoll}.");

            var spiceGained = raidRoll - opponentRoll;

            if (spiceGained > 0)
            {
                var spiceRaided = Math.Min(spiceGained, playerToRaid.Spice);
                player.Spice += spiceRaided;
                playerToRaid.Spice -= spiceRaided;
                Console.WriteLine($"{spiceRaided} Spice raided. {player.Name} now has {player.Spice} Spice. {playerToRaid.Name} now has {playerToRaid.Spice}.");
            }
            else
            {
                Console.WriteLine("Spice raid unsuccessful");
            }
        }
        else if (boardSpace.Name.StartsWith("Poison"))
        {
            var charactersToPoison = GetCharactersForAttack(player);
            var random = new Random();
            var randomCharacterToPoison = charactersToPoison[random.Next(0, charactersToPoison.Count)];

            Console.WriteLine($"{player.Name} is a attempting to poison {randomCharacterToPoison.Name}");

            var roll = random.Next(1, 7);

            Console.WriteLine($"Rolled {roll} against {randomCharacterToPoison.Name}'s Guile of {randomCharacterToPoison.Guile}.");
            if (roll > randomCharacterToPoison.Guile)
            {
                var damageRoll = random.Next(1, 7);

                var damage = Math.Min(damageRoll, randomCharacterToPoison.Strength);

                randomCharacterToPoison.Strength -= damage;

                Console.WriteLine($"Poison attempt succeeds. Rolled {damageRoll} for damage. {randomCharacterToPoison.Name}'s Strength is now {randomCharacterToPoison.Strength}");

                if (randomCharacterToPoison.Strength == 0)
                {
                    KillCharacter(randomCharacterToPoison);
                    Console.WriteLine($"{randomCharacterToPoison.Name} is dead.");
                }
            }
            else
            {
                Console.WriteLine("Poison attempt fails.");
            }
        }
        else if (boardSpace.Name.StartsWith("Training"))
        {
            character.Strength++;

            Console.WriteLine($"{character.Name} gained 1 Strength, now has {character.Strength} Strength");
        }
        else if (boardSpace.Name == "Space Guild")
        {
            var random = new Random();

            var randomBoardSpace = GameBoard.BoardSpaces[random.Next(0, GameBoard.BoardSpaces.Count)];

            Console.WriteLine($"Using Space Guild to move to {randomBoardSpace.Name}");

            MoveCharacter(player, character, randomBoardSpace);
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
            var charactersToDuel = GetCharactersForAttack(player);

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

    public List<Character> GetCharactersForAttack(Player attackingPlayer)
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

    public Player GetRandomActiveOtherPlayer(Player thisPlayer)
    {
        var activePlayers = Players.Where(player => thisPlayer != player && player.Characters.Count > 0).ToList();

        var random = new Random();

        return activePlayers[random.Next(0, activePlayers.Count)];
    }
}

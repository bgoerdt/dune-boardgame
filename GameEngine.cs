using System;

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

            // PLAY KANLY CARDS
            var shouldPlayKanlyCard = currentPlayer.Cards.Count(c => c.Type == CardType.KANLY) > 0 && random.Next(0, 2) == 1;

            if (shouldPlayKanlyCard)
            {
                var kanlyCardToPlay = currentPlayer.Cards.First(c => c.Type == CardType.KANLY);

                PlayKanlyCard(kanlyCardToPlay, currentPlayer);

                currentPlayer.Cards.Remove(kanlyCardToPlay);

                if (IsWinner())
                {
                    break;
                }
            }

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

    public void PlayKanlyCard(Card card, Player currentPlayer)
    {
        Console.WriteLine($"{currentPlayer.Name} plays {card.Name} Kanly card");

        if (card.Name == "Harvester Raid")
        {
            var playerToRaid = Players.FirstOrDefault(p => p != currentPlayer && p.Characters.Count > 0 && p.Harvesters > 0);

            if (playerToRaid == null)
            {
                Console.WriteLine("No players to raid");
                return;
            }

            var random = new Random();

            var raidRoll = random.Next(1, 9);
            var defenseRoll = random.Next(1, 7);

            var harvestersRaided = Math.Min(Math.Max(0, raidRoll - defenseRoll), playerToRaid.Harvesters);

            currentPlayer.Harvesters += harvestersRaided;
            playerToRaid.Harvesters -= harvestersRaided;

            Console.WriteLine($"{playerToRaid.Name}'s harvesters raided. {playerToRaid.Name} rolls {defenseRoll}, {currentPlayer.Name} rolls {raidRoll}. {currentPlayer.Name} gains {harvestersRaided} harvesters. {currentPlayer.Name} now has {currentPlayer.Harvesters}, {playerToRaid.Name} has {playerToRaid.Harvesters}");
        }
        else if (card.Name == "Secret Silo")
        {
            var random = new Random();

            var roll = random.Next(1, 7);

            currentPlayer.Spice += roll;

            Console.WriteLine($"Gains {roll} Spice, now has {currentPlayer.Spice}");
        }
        else if (card.Name == "Hunter-Seeker")
        {
            var random = new Random();

            var characters = GetCharactersForAttack(currentPlayer);

            var characterToAttack = characters[random.Next(0, characters.Count)];

            var roll = random.Next(1, 7);

            Console.WriteLine($"{characterToAttack.Name} is attacked. {currentPlayer.Name} rolls {roll} against {characterToAttack.Name}'s Guile of {characterToAttack.Guile}.");

            if (roll > characterToAttack.Guile)
            {
                Console.WriteLine($"{characterToAttack.Name} is killed.");

                if (characterToAttack.EquipmentCards.Count > 1)
                {
                    var equipmentCardToTake = characterToAttack.EquipmentCards[random.Next(0, characterToAttack.EquipmentCards.Count)];

                    characterToAttack.EquipmentCards.Remove(equipmentCardToTake);

                    if (!TryAssignEquipmentCard(currentPlayer.Characters, equipmentCardToTake))
                    {
                        currentPlayer.Cards.Add(equipmentCardToTake);
                    }
                }

                KillCharacter(characterToAttack);
            }
            else
            {
                Console.WriteLine($"{characterToAttack.Name} survives.");
            }
        }
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
        else if (boardSpace.Name.StartsWith("Spice Raid"))
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

            if (charactersToPoison.Count == 0)
            {
                Console.WriteLine("There are not characters that can be poisoned at this time");
                return;
            }

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
        else if (boardSpace.Name.Contains("Sietch"))
        {
            if (character.IsInSietch && character.Strength < character.InitialStrength)
            {
                character.Strength = character.InitialStrength;

                Console.WriteLine($"{character.Name}'s strength is restored to {character.InitialStrength}");
            }
        }
        else if (boardSpace.Name == "Traitor")
        {
            var playerToStealFrom = Players.FirstOrDefault(p => p.Cards.Count > 0);

            if (playerToStealFrom == null)
            {
                Console.WriteLine("There are not players with cards to steal.");
            }
            else
            {
                var random = new Random();
                var roll = random.Next(1, 7);
                var numberOfCardsToSteal = Math.Min(roll, playerToStealFrom.Cards.Count);

                Console.WriteLine($"{player.Name} rolled a {roll} and will steal {numberOfCardsToSteal} cards from {playerToStealFrom}");

                for (var index = 0; index < numberOfCardsToSteal; index++)
                {
                    var cardToSteal = playerToStealFrom.Cards[random.Next(0, playerToStealFrom.Cards.Count)];

                    playerToStealFrom.Cards.Remove(cardToSteal);

                    Console.WriteLine($"{player.Name} stole {cardToSteal.Name} from {playerToStealFrom.Name}");

                    if (cardToSteal.Type == CardType.KANLY || !TryAssignEquipmentCard(player.Characters, cardToSteal))
                    {
                        player.Cards.Add(cardToSteal);
                    }
                }
            }
        }
        else if (boardSpace.Name == "Bene Gesserit")
        {
            var characterToStealFrom = Players.SelectMany(p => p.Characters).FirstOrDefault(c => c.EquipmentCards.Count > 0);

            if (characterToStealFrom != null)
            {
                var cardToSteal = characterToStealFrom.EquipmentCards.First();

                characterToStealFrom.EquipmentCards.Remove(cardToSteal);

                Console.WriteLine($"{player.Name} steals {cardToSteal.Name} from {characterToStealFrom.Name}");

                if (!TryAssignEquipmentCard(player.Characters, cardToSteal))
                {
                    player.Cards.Add(cardToSteal);
                }
            }
            else
            {
                var playerToStealFrom = Players.FirstOrDefault(p => p.Cards.Count > 0);

                if (playerToStealFrom == null)
                {
                    Console.WriteLine("There are no cards available to steal");
                }

                var random = new Random();

                var cardToSteal = playerToStealFrom.Cards[random.Next(0, playerToStealFrom.Cards.Count)];

                playerToStealFrom.Cards.Remove(cardToSteal);

                Console.WriteLine($"{player.Name} stole {cardToSteal.Name} from {playerToStealFrom.Name}");

                if (cardToSteal.Type == CardType.KANLY || !TryAssignEquipmentCard(player.Characters, cardToSteal))
                {
                    player.Cards.Add(cardToSteal);
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

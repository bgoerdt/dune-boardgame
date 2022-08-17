public enum CardType
{
    EQUIPMENT,
    KANLY
}

public class Card {
    public string Name {get;set;}
    public string Description {get;set;}
    public CardType Type { get; set; }

    public static List<Card> Setup()
    {
        var gomJabbarDescription = "When fighting, use ONE extra 8-sided die. USE NO MORE THAN 6 DICE TOTAL.";
        var shieldDescription = "Protects your character from Lasguns.";
        var swordDescription = "When fighting, roll 8-sided dice INSTEAD of 6-sided dice.";
        var lasgunDescription = "Shoots one character 1 or 2 spaces ahead (clockwise). Use one 6-sided die. Roll LOWER than your character's strength to hit target. Roll 6-sided die again to determine damage. Opponent subtracts number rolled from character's strength.";
        var stillsuitDescription = "Increase your character's guile by 1 point when attacted by Worm or Sand Storm";
        var poisonDescription = "Use during fight ONLY after opponent receives 1 point of damage to his or her strength. Use 6-sided die. To Poison: Roll higher than opposing character's guile. Roll 6-sided die again to determine damage. Opponent subtracts number from character's strength. YOU MUST DISCARD THE POISON CARD AFTER USING IT.";
        var ornithopterDescription = "Move the character to whom it is assigned to any other castle space or desert space. Roll 6-sided die. To roll 1-3: safe landing, keep card. To roll 4-6: ornithopter is damaged; discard ornithopter card. Follow space's directions only if you use it on your turn in place of the dice throw.";
        var harvesterRaidDescription = "Choose any ONE opponent to raid. You roll 8-sided die; opponent rolls 6-sided die. To Win: Roll HIGHER than opponent, you steal up to the number of harvesters equal to the difference between your die roll and you opponent's.";
        var secretSiloDescription = "Roll 6-sided die and take, from the spice bank, the number of spice equal to the number rolled.";
        var hunterSeekerDescription = "Choose any ONE opponent's character to fight. Roll 6-sided die. To Win: Roll HIGHER than opposing character's guile. Remove eliminated character from board: take 1 Equipment card if eliminated character had any.";

        return new List<Card>
        {
            new Card { Type = CardType.EQUIPMENT, Name = "Gom Jabbar", Description = gomJabbarDescription },
            new Card { Type = CardType.EQUIPMENT, Name = "Gom Jabbar", Description = gomJabbarDescription },
            new Card { Type = CardType.EQUIPMENT, Name = "Gom Jabbar", Description = gomJabbarDescription },
            new Card { Type = CardType.EQUIPMENT, Name = "Gom Jabbar", Description = gomJabbarDescription },
            new Card { Type = CardType.EQUIPMENT, Name = "Shield", Description = shieldDescription },
            new Card { Type = CardType.EQUIPMENT, Name = "Shield", Description = shieldDescription },
            new Card { Type = CardType.EQUIPMENT, Name = "Shield", Description = shieldDescription },
            new Card { Type = CardType.EQUIPMENT, Name = "Shield", Description = shieldDescription },
            new Card { Type = CardType.EQUIPMENT, Name = "Sword", Description = swordDescription },
            new Card { Type = CardType.EQUIPMENT, Name = "Sword", Description = swordDescription },
            new Card { Type = CardType.EQUIPMENT, Name = "Sword", Description = swordDescription },
            new Card { Type = CardType.EQUIPMENT, Name = "Sword", Description = swordDescription },
            new Card { Type = CardType.EQUIPMENT, Name = "Sword", Description = swordDescription },
            new Card { Type = CardType.EQUIPMENT, Name = "Sword", Description = swordDescription },
            new Card { Type = CardType.EQUIPMENT, Name = "Sword", Description = swordDescription },
            new Card { Type = CardType.EQUIPMENT, Name = "Sword", Description = swordDescription },
            new Card { Type = CardType.EQUIPMENT, Name = "Lasgun", Description = lasgunDescription },
            new Card { Type = CardType.EQUIPMENT, Name = "Lasgun", Description = lasgunDescription },
            new Card { Type = CardType.EQUIPMENT, Name = "Lasgun", Description = lasgunDescription },
            new Card { Type = CardType.EQUIPMENT, Name = "Lasgun", Description = lasgunDescription },
            new Card { Type = CardType.EQUIPMENT, Name = "Stillsuit", Description = stillsuitDescription },
            new Card { Type = CardType.EQUIPMENT, Name = "Stillsuit", Description = stillsuitDescription },
            new Card { Type = CardType.EQUIPMENT, Name = "Stillsuit", Description = stillsuitDescription },
            new Card { Type = CardType.EQUIPMENT, Name = "Stillsuit", Description = stillsuitDescription },
            new Card { Type = CardType.EQUIPMENT, Name = "Poison", Description = poisonDescription },
            new Card { Type = CardType.EQUIPMENT, Name = "Poison", Description = poisonDescription },
            new Card { Type = CardType.EQUIPMENT, Name = "Poison", Description = poisonDescription },
            new Card { Type = CardType.EQUIPMENT, Name = "Poison", Description = poisonDescription },
            new Card { Type = CardType.EQUIPMENT, Name = "Poison", Description = poisonDescription },
            new Card { Type = CardType.EQUIPMENT, Name = "Poison", Description = poisonDescription },
            new Card { Type = CardType.EQUIPMENT, Name = "Poison", Description = poisonDescription },
            new Card { Type = CardType.EQUIPMENT, Name = "Poison", Description = poisonDescription },
            new Card { Type = CardType.EQUIPMENT, Name = "Ornithopter", Description = ornithopterDescription },
            new Card { Type = CardType.EQUIPMENT, Name = "Ornithopter", Description = ornithopterDescription },
            new Card { Type = CardType.EQUIPMENT, Name = "Ornithopter", Description = ornithopterDescription },
            new Card { Type = CardType.EQUIPMENT, Name = "Ornithopter", Description = ornithopterDescription },
            new Card { Type = CardType.KANLY, Name = "Harvester Raid", Description = harvesterRaidDescription },
            new Card { Type = CardType.KANLY, Name = "Harvester Raid", Description = harvesterRaidDescription },
            new Card { Type = CardType.KANLY, Name = "Harvester Raid", Description = harvesterRaidDescription },
            new Card { Type = CardType.KANLY, Name = "Harvester Raid", Description = harvesterRaidDescription },
            new Card { Type = CardType.KANLY, Name = "Harvester Raid", Description = harvesterRaidDescription },
            new Card { Type = CardType.KANLY, Name = "Secret Silo", Description = secretSiloDescription },
            new Card { Type = CardType.KANLY, Name = "Secret Silo", Description = secretSiloDescription },
            new Card { Type = CardType.KANLY, Name = "Secret Silo", Description = secretSiloDescription },
            new Card { Type = CardType.KANLY, Name = "Secret Silo", Description = secretSiloDescription },
            new Card { Type = CardType.KANLY, Name = "Secret Silo", Description = secretSiloDescription },
            new Card { Type = CardType.KANLY, Name = "Secret Silo", Description = secretSiloDescription },
            new Card { Type = CardType.KANLY, Name = "Hunter-Seeker", Description = hunterSeekerDescription },
            new Card { Type = CardType.KANLY, Name = "Hunter-Seeker", Description = hunterSeekerDescription },
            new Card { Type = CardType.KANLY, Name = "Hunter-Seeker", Description = hunterSeekerDescription },
            new Card { Type = CardType.KANLY, Name = "Hunter-Seeker", Description = hunterSeekerDescription },
            new Card { Type = CardType.KANLY, Name = "Hunter-Seeker", Description = hunterSeekerDescription }
        };
    }
}
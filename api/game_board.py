red_sietch = {"boardSpaceId": 1, "name": "Red Sietch", "type": "SIETCH"}
yellow_sietch = {"boardSpaceId": 2, "name": "Yellow Sietch", "type": "SIETCH"}
blue_sietch = {"boardSpaceId": 3, "name": "Blue Sietch", "type": "SIETCH"}
green_sietch = {"boardSpaceId": 4, "name": "Green Sietch", "type": "SIETCH"}
spice_1 = {"boardSpaceId": 5, "name": "Spice (Red)", "type": "DESERT"}
spice_2 = {"boardSpaceId": 6, "name": "Spice (Red/Yellow)", "type": "DESERT"}
spice_3 = {"boardSpaceId": 7, "name": "Spice (Yellow)", "type": "DESERT"}
spice_4 = {"boardSpaceId": 8, "name": "Spice (Yellow/Blue)", "type": "DESERT"}
spice_5 = {"boardSpaceId": 9, "name": "Spice (Blue)", "type": "DESERT"}
spice_6 = {"boardSpaceId": 10, "name": "Spice (Blue/Green)", "type": "DESERT"}
spice_7 = {"boardSpaceId": 11, "name": "Spice (Green)", "type": "DESERT"}
spice_8 = {"boardSpaceId": 12, "name": "Spice (Green/Red)", "type": "DESERT"}
sand_storm = {"boardSpaceId": 13, "name": "Sand Storm", "type": "DESERT"}
worm_1 = {"boardSpaceId": 14, "name": "Worm (Yellow)", "type": "DESERT"}
worm_2 = {"boardSpaceId": 15, "name": "Worm (Blue)", "type": "DESERT"}
worm_3 = {"boardSpaceId": 16, "name": "Worm (Green)", "type": "DESERT"}
worm_4 = {"boardSpaceId": 17, "name": "Worm (Red)", "type": "DESERT"}
duel_1 = {"boardSpaceId": 18, "name": "Duel (Yellow/Blue)", "type": "DESERT"}
duel_2 = {"boardSpaceId": 19, "name": "Duel (Blue/Green)", "type": "DESERT"}
duel_3 = {"boardSpaceId": 20, "name": "Duel (Green/Red)", "type": "DESERT"}
training_1 = {"boardSpaceId": 21, "name": "Training (Red)", "type": "CASTLE"}
training_2 = {"boardSpaceId": 22, "name": "Training (Yellow)", "type": "CASTLE"}
training_3 = {"boardSpaceId": 23, "name": "Training (Blue)", "type": "CASTLE"}
training_4 = {"boardSpaceId": 24, "name": "Training (Green)", "type": "CASTLE"}
smuggler = {"boardSpaceId": 25, "name": "Smuggler", "type": "CASTLE"}
spice_raid_1 = {"boardSpaceId": 26, "name": "Spice Raid (Red/Yellow", "type": "CASTLE"}
spice_raid_2 = {"boardSpaceId": 27, "name": "Spice Raid (Green/Red", "type": "CASTLE"}
traitor = {"boardSpaceId": 28, "name": "Traitor", "type": "CASTLE"}
poison_1 = {"boardSpaceId": 29, "name": "Poison (Blue/Yellow)", "type": "CASTLE"}
poison_2 = {"boardSpaceId": 30, "name": "Poison (Green/Blue)", "type": "CASTLE"}
bene_gesserit = {"boardSpaceId": 31, "name": "Bene Gesserit", "type": "CASTLE"}
space_guild = {"boardSpaceId": 32, "name": "Space Guild", "type": "CASTLE"}

board_spaces = []

def get_possible_moves(currentBoardSpaceId, spacesLeftToMove: int, accumulatedPossibleMoves: List(int), previousBoardSpaceId):
    if not accumulatedPossibleMoves:
        accumulatedPossibleMoves = []
#
#     accumulatedPossibleMoves ??= new List < BoardSpace > ();
#
# if (spacesLeftToMove == 0)
# {
#     accumulatedPossibleMoves.Add(currentBoardSpace);
#
#       return accumulatedPossibleMoves;
# }
#
# if (currentBoardSpace.AltNext != null & & previousBoardSpace != currentBoardSpace.AltNext)
# {
#   GetPossibleMoves(currentBoardSpace.AltNext, spacesLeftToMove - 1, accumulatedPossibleMoves, currentBoardSpace);
# }
#
#   GetPossibleMoves(currentBoardSpace.Next, spacesLeftToMove - 1, accumulatedPossibleMoves, currentBoardSpace);
#
# return accumulatedPossibleMoves;
# }

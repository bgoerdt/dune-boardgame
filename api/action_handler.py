import uuid

def create_game():
    return {
        "gameId": uuid.uuid4(),
        "players": []
    }

def add_player(game_state, new_player):
    return {
        **game_state,
        "players": [
            *game_state["players"],
            {
                "name": new_player.name,
                "spice": 6,
                "harvesters": 3,
                "cards": []
            }
        ]
    }

def handle_action(game_states, action):
    game_state = next(filter(lambda gs: gs["gameId"] == action.get("gameId"), game_states), None)

    if (action.get("type") == "CREATE_GAME"):
        game_state = create_game()
    elif (action.get("type") == "ADD_PLAYER"):
        game_state = add_player(game_state, action.get("payload"))

    game_states = [
        *filter(lambda gs: gs.get("gameId") != game_state.get("gameId"), game_states),
        game_state
    ]

    return game_states

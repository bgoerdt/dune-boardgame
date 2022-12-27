from action_handler import handle_action


def main():
    print('Hi, Arrakis')

    game_states = []

    game_states = handle_action(game_states, {"type": "CREATE_GAME"})

    game_id = game_states[0].get("gameId")

    print(game_states)

    game_states = handle_action(game_states, {"gameId": game_id, "type": "ADD_PLAYER", "payload": {"name": "Ben", "team": "ATREIDES"}})

    print(game_states)


if __name__ == '__main__':
    main()

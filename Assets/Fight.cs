using UnityEngine;

public enum FightState {
    WaitingForPlayerToSpawnHisFirstPokemon,
    PlayerTurn,
    EnemyTurn,
}

// Есть баги: игрок можеть нажимать на атаку / играть карты когда угодно.
// Нужно ограничить игрока ❌🗽
//
// По хорошему мы спавним бой через Instantiate и прокидываем
// в него все нужные поля. Это то как я вижу 🖌
public class Fight : MonoBehaviour {
    // Для позиционирования объектов:
    // FightArea -- parent,
    // PlayerPokemonSpawnPoint -- позиция внутри FightArea
    public GameObject FightArea;
    public Transform PlayerPokemonSpawnPoint;

    public FightState State;
    public Hand PlayerHand;

    public Pokemon playerPokemon;
    public Pokemon enemyPokemon;

    private void Start() {
        State = FightState.WaitingForPlayerToSpawnHisFirstPokemon;

        foreach (Card card in PlayerHand.Cards) {
            card.Clicked += OnCardPlayed;
        }
    }

    public void SwapPlayerPokemon(Pokemon newPokemon) {
        switch (State) {
            case FightState.WaitingForPlayerToSpawnHisFirstPokemon:
                playerPokemon = newPokemon;
                State = FightState.PlayerTurn;
                break;
            case FightState.PlayerTurn:
                Destroy(playerPokemon.gameObject);
                playerPokemon = newPokemon;
                break;
            case FightState.EnemyTurn:
                Debug.LogWarning("Нельзя играть карты во время хода противника");
                break;
            default:
                Debug.LogError("Что за стейт?");
                break;
        }
    }

    public void OnEnemyDamageAnimationFinished(Pokemon enemy) {
        if (State != FightState.EnemyTurn) {
            Debug.LogError("Вот такого быть не должно. Напишите kawaii-Code");
            return;
        }

        enemyPokemon.DamageAnimationFinished -= OnEnemyDamageAnimationFinished;
        enemyPokemon.Attack(playerPokemon);

        if (playerPokemon.Health.IsZero) {
            State = FightState.WaitingForPlayerToSpawnHisFirstPokemon;
        } else {
            State = FightState.PlayerTurn;
        }
    }

    public void PlayerPressedTheAttackButton() {
        if (State != FightState.PlayerTurn) {
            Debug.LogWarning("Атаковать сейчас нельзя.");
            return;
        }

        enemyPokemon.DamageAnimationFinished += OnEnemyDamageAnimationFinished;
        playerPokemon.Attack(enemyPokemon);
        State = FightState.EnemyTurn;
    }

    private void OnCardPlayed(Card card) {
        card.Clicked -= OnCardPlayed;
        // Должно быть не здесь, но уже 22 часа ночи, а я в поезде второй час
        // программирую (или 3-ий), короче немного устал, сами потом почините
        // или подумайте как это сделать лучше 😜
        PlayerHand.Cards.Remove(card);
        Destroy(card.gameObject);

        if (card.Pokemon == null) {
            Debug.LogError("Спеллы ещё не сделаны");
        } else {
            Pokemon pokemon = Instantiate(card.Pokemon, FightArea.transform);
            pokemon.transform.position = PlayerPokemonSpawnPoint.position;
            SwapPlayerPokemon(pokemon);
        }
    }
}

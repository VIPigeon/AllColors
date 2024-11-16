using UnityEngine;
using UnityEngine.UI;

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
    public Transform EnemyPokemonSpawnPoint;

    public FightState State;
    public FullDeck FullPlayerDeck;
    public PlayerHandInBattle PlayerHand;
    public EnemyAI Enemy;

    public Pokemon PlayerPokemon;
    public Pokemon EnemyPokemon;
    
    public Button AttackButton;
    public Button SecondaryAttackButton; // Которая непонятно зачем

    private void Start() {
        State = FightState.WaitingForEnemyToSpawnPokemon;
        DoEnemyTurn();
        State = FightState.WaitingForPlayerToSpawnHisFirstPokemon;
        PlayerHand.Construct(FullPlayerDeck);
    }
    
    private void OnEnable() {
        PlayerHand.CardPlayed += OnPlayerCardPlayed;
    }
    
    private void OnDisable() {
        PlayerHand.CardPlayed -= OnPlayerCardPlayed;
    }
    
    private void Update() {
        // Я пишу код в Notepad++ 😋, тут нет подсказок. А что делать?
        // JetBrains ушли из России 🤬. А как вы думаете? Так и живём. 🤐
        if (State != FightState.PlayerTurn) {
            AttackButton.interactable = false;
            SecondaryAttackButton.interactable = false;
        } else {
            AttackButton.interactable = true;
            SecondaryAttackButton.interactable = true;
        }
        
        if ((PlayerPokemon == null || PlayerPokemon.Card.CurrentHealth.IsZero) && PlayerHand.OutOfCards) {
            Debug.Log("Игрок проиграл ахахах");
        }
        if ((EnemyPokemon == null || EnemyPokemon.Card.CurrentHealth.IsZero) && Enemy.OutOfCards) {
            Debug.Log("Враг проиграл. Круто?");
        }
    }

    public void SwapPlayerPokemon(Pokemon newPokemon) {
        switch (State) {
            case FightState.WaitingForPlayerToSpawnHisFirstPokemon:
                PlayerPokemon = newPokemon;
                State = FightState.PlayerTurn;
                break;
            case FightState.PlayerTurn:
                PlayerHand.ReturnCard(PlayerPokemon.Card);
                Destroy(PlayerPokemon.gameObject);
                PlayerPokemon = newPokemon;
                break;
            case FightState.EnemyTurn:
                Debug.LogWarning("Нельзя играть карты во время хода противника");
                break;
            default:
                Debug.LogError("Что за стейт?");
                break;
        }
    }
    
    private void SwapEnemyPokemon(Pokemon newPokemon) {
        switch (State) {
            case FightState.WaitingForEnemyToSpawnPokemon:
                EnemyPokemon = newPokemon;
                break;
            case FightState.EnemyTurn:
                Enemy.ReturnCard(EnemyPokemon.Card);
                Destroy(EnemyPokemon.gameObject);
                EnemyPokemon = newPokemon;
                break;
            default:
                Debug.LogError($"Плохой стейт: {State}");
                break;
        }
    }

    private void OnPlayerCardPlayed(Card card) {
        if (card.Config.Type.IsSpell()) {
            Debug.LogError("Спеллы ещё не сделаны");
        } else {
            Pokemon pokemon = Instantiate(card.Config.Pokemon, FightArea.transform);
            pokemon.Construct(card);
            pokemon.transform.position = PlayerPokemonSpawnPoint.position;
            SwapPlayerPokemon(pokemon);
        }
    }
    
    public void PlayerPressedTheAttackButton() {
        if (State != FightState.PlayerTurn) {
            Debug.LogError("Атаковать сейчас нельзя.");
            return;
        }

        EnemyPokemon.ReadyToAttack += OnEnemyReadyToAttack;
        PlayerPokemon.Attack(EnemyPokemon);
        PlayerHand.Refill();
        
        if (EnemyPokemon.Card.CurrentHealth.IsZero) {
            State = FightState.WaitingForEnemyToSpawnPokemon;
        } else {
            State = FightState.EnemyTurn;
        }
    }
    
    private void DoEnemyTurn() {
        EnemyTurn turn = Enemy.DoTurn(EnemyPokemon, PlayerPokemon);
        switch (turn.Type) {
            case EnemyTurnType.Attack:
                EnemyPokemon.Attack(PlayerPokemon);
                break;
            case EnemyTurnType.PlayCard:
                Card card = turn.Card;
                Pokemon pokemon = Instantiate(card.Config.Pokemon, FightArea.transform);
                pokemon.Construct(card);
                pokemon.transform.position = EnemyPokemonSpawnPoint.position;
                SwapEnemyPokemon(pokemon);
                if (PlayerPokemon != null) {
                    EnemyPokemon.Attack(PlayerPokemon);
                }
                break;
            case EnemyTurnType.GiveUp:
                Debug.Log("Игрок победил в бою. Круто?");
                break;
            default:
                Debug.LogError($"Не умею обрабатывать ход {turn.Type}!");
                break;
        }
    }
    
    public void OnEnemyReadyToAttack(Pokemon enemy) {
        EnemyPokemon.ReadyToAttack -= OnEnemyReadyToAttack;
        DoEnemyTurn();
        if (PlayerPokemon.Card.CurrentHealth.IsZero) {
            State = FightState.WaitingForPlayerToSpawnHisFirstPokemon;
        } else {
            State = FightState.PlayerTurn;
        }
    }
}

using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

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
    public PlayerHandInBattle PlayerHand;
    public EnemyAI Enemy;

    public Pokemon PlayerPokemon;
    public Pokemon EnemyPokemon;
    
    public Button AttackButton;

    [Header("Визуальная шелуха")]
    public TextMeshProUGUI EnemyPokemonName;
    public TextMeshProUGUI Bonus1Text;
    public Image EnemyPokemonColor;
    public TextMeshProUGUI PlayerPokemonName;
    public TextMeshProUGUI Bonus2Text;
    public Image PlayerPokemonColor;

    private void Start() {
        Card enemyFirstCard = Enemy.FirstTurn();    
        Pokemon pokemon = Instantiate(enemyFirstCard.Config.Pokemon, FightArea.transform);
        pokemon.Construct(enemyFirstCard);
        pokemon.transform.position = EnemyPokemonSpawnPoint.position;
        EnemyPokemon = pokemon;
        
        State = FightState.WaitingForPlayerToSpawnHisFirstPokemon;
        PlayerHand.Construct(Singleton<FullDeck>.Instance);
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
        } else {
            AttackButton.interactable = true;
        }
        
        if ((PlayerPokemon == null || PlayerPokemon.Card.CurrentHealth.IsZero) && PlayerHand.OutOfCards) {
            Debug.Log("Игрок проиграл ахахах");
            SceneManager.LoadScene("Overworld");
        }
        
        if (EnemyPokemon.Card.CurrentHealth.IsZero) {        
            Debug.Log($"Враг {Enemy.CharacterID} проиграл. Круто?");
            Singleton<NPCState>.Instance.States[Enemy.CharacterID] = CharacterState.Defeated;
            SceneManager.LoadScene("Overworld");
        }
        
        if (EnemyPokemon == null || EnemyPokemon.Card.CurrentHealth.IsZero) {
            EnemyPokemonName.text = "*мертв*";
            EnemyPokemonColor.color = Color.gray;
        } else {
            EnemyPokemonName.text = EnemyPokemon.Card.Config.Name;
            EnemyPokemonColor.color = EnemyPokemon.Card.Config.Color;
        }
        
        if (PlayerPokemon == null || PlayerPokemon.Card.CurrentHealth.IsZero) {
            PlayerPokemonName.text = "*ждем покемона*";
            PlayerPokemonColor.color = Color.gray;
        } else {
            PlayerPokemonName.text = PlayerPokemon.Card.Config.Name;
            PlayerPokemonColor.color = PlayerPokemon.Card.Config.Color;
        }
        
        double bonus1 = 0.0;
        double bonus2 = 0.0;
        if (!(PlayerPokemon == null || PlayerPokemon.Card.CurrentHealth.IsZero) &&
            !(EnemyPokemon == null || EnemyPokemon.Card.CurrentHealth.IsZero))
        {
            bonus1 = ColorInfo.DamageBonuses[EnemyPokemon.Card.Config.ColorType][PlayerPokemon.Card.Config.ColorType];
            bonus2 = ColorInfo.DamageBonuses[PlayerPokemon.Card.Config.ColorType][EnemyPokemon.Card.Config.ColorType];
        }
        
        if (bonus1 == 0) {
            Bonus1Text.color = Color.gray;
            Bonus1Text.text = "0";
        } else {
            Bonus1Text.color = Color.green;
            Bonus1Text.text = $"x{bonus1}";
        }
        
        if (bonus2 == 0) {
            Bonus2Text.color = Color.gray;
            Bonus2Text.text = "0";
        } else {
            Bonus2Text.color = Color.green;
            Bonus2Text.text = $"x{bonus2}";
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

    private void OnPlayerCardPlayed(Card card) {
        if (card.Config.Type.IsSpell()) {
            switch (card.Config.Type) {
                case CardType.Hellfire:
                    EnemyPokemon.TakeDamage(5, card);
                    break;
                case CardType.Snow:
                    EnemyPokemon.AddEffect(EffectType.Snow, 2);
                    break;
                case CardType.Depression:
                    EnemyPokemon.AddEffect(EffectType.Depression, 1);
                    break;
                case CardType.Radioactive:
                    EnemyPokemon.AddEffect(EffectType.Poisoned, 3);
                    break;
                case CardType.PurpleHeart:
                    Debug.Log("Hi");
                    PlayerPokemon.Card.CurrentHealth.Restore();
                    PlayerPokemon.HealthView.Show(PlayerPokemon.Card.CurrentHealth);
                    break;
                default:
                    Debug.LogError($"Хрень {card.Config.Type}");
                    break;
            }
        } else {
            Pokemon pokemon = Instantiate(card.Config.Pokemon, FightArea.transform);
            pokemon.Construct(card);
            pokemon.transform.position = PlayerPokemonSpawnPoint.position;
            SwapPlayerPokemon(pokemon);
        }
    }
    
    public void PlayerPressedTheAttackButton() {
        EnemyPokemon.ReadyToAttack += OnEnemyReadyToAttack;
        PlayerPokemon.Attack(EnemyPokemon);
        PlayerHand.Refill();
        SwitchTurnToEnemy();
    }
    
    public void SwitchTurnToPlayer() {
        if (EnemyPokemon != null) {
            EnemyPokemon.EndTurn();
        }

        foreach (var card in PlayerHand.Hand.Hand) {
            if (card.Config.Type == CardType.LovestruckToad) {
                card.CurrentHealth.Value = Math.Min(card.CurrentHealth.Value + (int)1.6, card.CurrentHealth.MaxAmount);
            }
        }
        
        if (PlayerPokemon != null) {
            PlayerPokemon.BeginTurn();
        }
        
        if (PlayerPokemon.Card.CurrentHealth.IsZero) {
            State = FightState.WaitingForPlayerToSpawnHisFirstPokemon;
        } else {
            State = FightState.PlayerTurn;
        }
    }
    
    public void OnEnemyReadyToAttack(Pokemon enemy) {
        EnemyPokemon.ReadyToAttack -= OnEnemyReadyToAttack;
        DoEnemyTurn();
        SwitchTurnToPlayer();
    }
    
    private void DoEnemyTurn() {
        if (EnemyPokemon != null && EnemyPokemon.HasEffect(EffectType.Depression)) {
            SwitchTurnToPlayer();
            return;
        }
        
        EnemyTurn turn = Enemy.DoTurn(EnemyPokemon, PlayerPokemon);
        switch (turn.Type) {
            case EnemyTurnType.Attack:
                EnemyPokemon.Attack(PlayerPokemon);
                break;
            case EnemyTurnType.PlayCard:
                Card card = turn.Card;
                if (!card.Config.Type.IsSpell()) {
                    Debug.LogError("Не умею играть что-то кроме спеллов");
                    return;
                }
                Debug.Log($"Играю спелл {card} (нет)");
                break;
            case EnemyTurnType.GiveUp:
                Debug.Log("Враг сдался");
                break;
            default:
                Debug.LogError($"Не умею обрабатывать ход {turn.Type}!");
                break;
        }
    }
    
    public void SwitchTurnToEnemy() {
        if (PlayerPokemon != null) {
            PlayerPokemon.EndTurn();
        }
        if (EnemyPokemon != null) {
            EnemyPokemon.BeginTurn();
        }
        
        State = FightState.EnemyTurn;
    }
}

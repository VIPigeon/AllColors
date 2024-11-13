using UnityEngine;

public class Fight : MonoBehaviour
{
    public Pokemon playerPokemon;
    public Pokemon enemyPokemon;

    private bool _waitingForPlayerAttack = true;
    private bool _waitingForEnemyAttack = false;

    private void Update() {
        if (_waitingForPlayerAttack || _waitingForEnemyAttack) {
            return;
        }

        playerPokemon.Attack(enemyPokemon);
        _waitingForEnemyAttack = true;

        enemyPokemon.DamageAnimationFinished += OnEnemyDamageAnimationFinished;
    }

    public void OnEnemyDamageAnimationFinished(Pokemon enemy) {
        enemyPokemon.DamageAnimationFinished -= OnEnemyDamageAnimationFinished;
        enemyPokemon.Attack(playerPokemon);
        _waitingForPlayerAttack = true;
        _waitingForEnemyAttack = false;
    }

    public void PlayerPressedTheAttackButton() {
        _waitingForPlayerAttack = false;
    }
}

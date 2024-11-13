using System;
using UnityEngine;

public class Pokemon : MonoBehaviour {
    // Не разделяю точку зрения разделения view и model (каламбур типа)
    public Health Health;
    public Animator Animator;
    public int Damage = 3;

    public event Action<Pokemon> DamageAnimationFinished;

    // Кто захочет атаковать сам себя, будет иметь дело со мной 😠
    public void Attack(Pokemon otherPokemon) {
        otherPokemon.TakeDamage(Damage);
    }

    public void TakeDamage(int damage)
    {
        Health.Sub(damage);
        Animator.Play("Enemy_Pokemon_Animation");

        if (Health.IsZero) {
            Die();
        }
    }

    public void Die() {
        // И проиграть крутой эффект 😎
        Destroy(gameObject);
    }

    // Тупая прокидка ивентов от юнити. Принимаю предложения по другим способам.
    // Пишите на адрес kawaii-Code@boomerang2.com
    public void OnDamageAnimationFinished(AnimationEvent ev) {
        Animator.Play("Idle");
        DamageAnimationFinished?.Invoke(this);
    }
}
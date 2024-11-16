using System;
using UnityEngine;
using UnityEngine.UI;

public class Pokemon : MonoBehaviour {
    public Image Image; // временно
    
    // Не разделяю точку зрения разделения view и model
    // (каламбур типа "точка зрения" и "view" 🤣)
    public HealthView HealthView;
    public Animator Animator;
    public Card Card;

    public event Action<Pokemon> ReadyToAttack;

    public void Construct(Card card) {
        Card = card;
        Image.color = card.Config.Color;
        HealthView.SetColor(card.Config.Color);
        HealthView.Show(card.CurrentHealth);
    }

    // Кто захочет атаковать сам себя, будет иметь дело со мной 😠
    public void Attack(Pokemon otherPokemon) {
        otherPokemon.TakeDamage(Card.CurrentDamage);
    }

    public void TakeDamage(int damage) {
        Card.CurrentHealth.Sub(damage);
        HealthView.Show(Card.CurrentHealth);
        Animator.Play("Damaged");

        if (Card.CurrentHealth.IsZero) {
            Die();
        }
    }

    public void Die() {
        // И проиграть крутой эффект 😎
        Animator.Play("Death");
    }

    // Тупая прокидка ивентов от юнити. Принимаю предложения по другим способам.
    // Пишите на адрес kawaii-Code@boomerang2.com
    public void OnDamageAnimationFinished(AnimationEvent ev) {
        Animator.Play("Idle");
        ReadyToAttack?.Invoke(this);
    }
    
    public void OnDeathAnimationFinished(AnimationEvent ev) {
        ReadyToAttack?.Invoke(this);
        Destroy(gameObject);
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pokemon : MonoBehaviour {
    // Не разделяю точку зрения разделения view и model
    // (каламбур типа "точка зрения" и "view" 🤣)
    public HealthView HealthView;
    public Animator Animator;
    public Card Card;

    public List<Effect> ActiveEffects;
    public event Action<Pokemon> ReadyToAttack;

    public void Construct(Card card) {
        Card = card;
        HealthView.SetColor(card.Config.Color);
        HealthView.Show(card.CurrentHealth);
        ActiveEffects = new List<Effect>();
    }

    public void BeginTurn() {
        if (HasEffect(EffectType.Poisoned)) {
            TakeDamage(1, null);
        }
    }

    public void EndTurn() {
        List<Effect> effectsToBeDestroyed = new();
        foreach (Effect effect in ActiveEffects) {
            effect.TurnsLeft -= 1;
            if (effect.TurnsLeft == 0) {
                effectsToBeDestroyed.Add(effect);
            }
        }
        
        // ок
        foreach (Effect effect in effectsToBeDestroyed) {
            ActiveEffects.Remove(effect);
        }
    }

    public bool HasEffect(EffectType effectType) {
        foreach (Effect effect in ActiveEffects) {
            if (effect.Type == effectType) {
                return true;
            }
        }
        return false;
    }

    public void AddEffect(EffectType effectType, int turns) {
        ActiveEffects.Add(new Effect() {
            Type = effectType,
            TurnsLeft = turns,
        });
    }

    // Кто захочет атаковать сам себя, будет иметь дело со мной 😠
    public void Attack(Pokemon otherPokemon) {
        otherPokemon.TakeDamage(Card.CurrentDamage, Card);
    }

    public void TakeDamage(int damage, Card cardThatDamagedMe) {
        if (cardThatDamagedMe != null) {
            if (cardThatDamagedMe.Config.Type == CardType.BlueFrog) {
                AddEffect(EffectType.Poisoned, 2);
            }
            double damageBonus = ColorInfo.DamageBonuses[cardThatDamagedMe.Config.ColorType][Card.Config.ColorType];
            damage = (int)((double)damage * damageBonus);
        }
        
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
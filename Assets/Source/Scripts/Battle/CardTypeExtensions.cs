using UnityEngine;

public static class CardTypeExtensions {
    public static bool IsPokemon(this CardType cardType) {
        switch (cardType) {
            case CardType.Invalid:
                Debug.LogError("Кто-то забыл проставить CardType у карточки. Ай-яй-яй! 😠");
                return false;
            case CardType.SchoolBus:
            case CardType.BlueFrog:
            case CardType.LovestruckToad:
            case CardType.Squirrel:
            case CardType.FaintedFrog:
            case CardType.MechmatTyan:
                return true;
            default:
                return false;
        }
    }
    
    public static bool IsSpell(this CardType cardType) {
        switch (cardType) {
            case CardType.Invalid:
                Debug.LogError("Кто-то забыл проставить CardType у карточки. Ай-яй-яй! 😠");
                return false;
            case CardType.Hellfire:
            case CardType.Snow:
            case CardType.Depression:
            case CardType.Radioactive:
            case CardType.PurpleHeart:
                return true;
            default:
                return false;
        }
    }
}
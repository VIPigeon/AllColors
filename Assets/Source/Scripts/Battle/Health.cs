using System;

public class Health {
    public readonly int MaxAmount;
   
    private int _current;

    public int Value {
        get {
            return _current;
        }
        set {
            if (!(0 <= value && value <= MaxAmount)) {
                throw new ArgumentException($"Bad health value {value}");
            }

            _current = value;
        }
    }

    public bool IsZero => Value == 0;

    public Health(int maxAmount) {
        MaxAmount = maxAmount;
        _current = maxAmount;
    }

    public void Restore() {
        Value = MaxAmount;
    }

    public void Sub(int damage) {
        if (damage > Value) {
            Value = 0;
        } else {
            Value = Value - damage;
        }
    }
}

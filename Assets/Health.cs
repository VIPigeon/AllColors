using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {
    public int MaxAmount;
    public Slider Healthbar;
    
    private int _current;

    public int Value {
        get {
            return _current;
        }
        set {
            if (!(0 <= value && value <= MaxAmount)) {
                Debug.LogError($"Bad health value {value}");
            }

            _current = value;
            ShowOnHealthbar();
        }
    }

    public bool IsZero => Value == 0;

    private void Start() {
        _current = MaxAmount;
        ShowOnHealthbar();
    }

    public void Sub(int damage) {
        Value = Mathf.Max(Value - damage, 0);
    }

    private void ShowOnHealthbar() {
        Healthbar.value = (float)_current / (float)MaxAmount;
    }
}
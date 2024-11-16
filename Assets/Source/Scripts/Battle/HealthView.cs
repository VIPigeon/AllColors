using UnityEngine;
using UnityEngine.UI;

// Блин. Вот зачем. Ещё только не хватало HealthController. Ужас.
public class HealthView : MonoBehaviour {
    public Slider Healthbar;
    public Image Fill;

    public void Show(Health health) {
        Healthbar.value = (float)health.Value / (float)health.MaxAmount;
    }
    
    public void SetColor(Color color) {
        Fill.color = color;
    }
}
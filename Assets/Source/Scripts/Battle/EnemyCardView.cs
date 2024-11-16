using UnityEngine;

// Кайф 😁
public class EnemyCardView : MonoBehaviour {
    public GameObject CardObject;

    public void Disappear() {
        Destroy(gameObject);
    }
}
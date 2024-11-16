public class Card {
    public Health CurrentHealth;
    public int CurrentDamage;
    public CardConfig Config;
    
    public Card(CardConfig config) {
        Config = config;
        CurrentHealth = new Health(config.InitialHealth);
        CurrentDamage = config.InitialDamage;
    }
}

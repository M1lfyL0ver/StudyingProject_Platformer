public interface IHealth
{
    float CurrentHealth { get; }
    float MaxHealth { get; }
    float MinHealth { get; }

    event System.Action<float, float> HealthChanged;
}
namespace Chess.Interfaces
{
    public interface IDamageable
    {
        int CurrentHealth { get; }
        bool TakeDamage(int damageAmount);
    }
}
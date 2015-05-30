//Interface for enemies (model Interface)
//  contains points and implement IDestructible interface (all enemies are destructible)
namespace Asteroids.Interface
{
    interface IEnemy : IDestructible
    {
        int Points { get; set; }
    }
}

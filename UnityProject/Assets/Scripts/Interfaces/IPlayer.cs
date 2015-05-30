//Interface for player (model Interface)
//  contains score, flag for touchable and implement IDestructible interface (player is destructible)
namespace Asteroids.Interface
{
    public interface IPlayer : IDestructible
    {
        int Score{get;set;}
        bool isUntouchable { get; set; }
    }
}

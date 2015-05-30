//Interface for Bullet (model Interface)
//  provides information who is owner of the bullet
namespace Asteroids.Interface
{
    interface IBullet : IDestructible
    {
        float Range { get; set; }
        AbstractController Owner { get; set; }
    }
}

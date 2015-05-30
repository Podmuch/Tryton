//Interface for destructible objects (model Interface)
//  destruct method (params to explosion animation)
//  provides information is object destroyed
//  contains Lives
using UnityEngine;

namespace Asteroids.Interface
{
    public interface IDestructible
    {
        int Lives { get; set; }
        bool isDestroyed { get; set; }
        void Destruct(Sprite[] explosionSpriteArray);
    }
}

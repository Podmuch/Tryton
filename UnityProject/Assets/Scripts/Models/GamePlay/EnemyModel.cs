//Enemy model
//  abstract model for all enemies
using UnityEngine;
using Asteroids.Interface;

namespace Asteroids.MovableObject.Enemy
{
    //inherits from base abstract class for all MovableObject (movement)
    //implements IEnemy interface <- model interface (points and destroying)
    public abstract class EnemyModel : MovableObjectModel, IEnemy
    {
        public bool isDestroyed { get; set; }
        public int Points { get; set; }
        public int Lives { get; set; }

        public void Destruct(Sprite[] explosionSpriteArray)
        {
            DrawParams = explosionSpriteArray;
            isDestroyed = true;
            Lives--;
            speed = Vector2.zero;
        }
        public override void Move()
        {
            //Update position
            objectTransform.position += (Vector3)speed;
            Wrapping();
        }
    }
}

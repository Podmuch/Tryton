//Asteroid Model
//  provides appropriate constructor
using UnityEngine;

namespace Asteroids.MovableObject.Enemy.Asteroid
{
    //inherits from base abstract class for all Enemy Models (movement and destroying)
    public class AsteroidModel : EnemyModel
    {
        //set direction, speed and size
        public AsteroidModel(Transform _objectTransform, int _lives) 
        {
            isDestroyed = false;
            Lives = _lives;
            Points = 100;
            _objectTransform.localScale *= Lives;
            objectTransform = _objectTransform;
            int acuteAngle = Mathf.RoundToInt(objectTransform.eulerAngles.z) % 90;
            speed = new Vector2(Mathf.Cos(Mathf.Deg2Rad * (90 - acuteAngle)),
                                                 Mathf.Cos(Mathf.Deg2Rad * acuteAngle));
            maxSpeed=0.03f;
            speed = CorrectSpeedDirection(speed);
            speed=speed.normalized*maxSpeed;
        }
    }
}

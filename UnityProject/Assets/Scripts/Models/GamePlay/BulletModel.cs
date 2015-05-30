//Bullet Model
//  provides appropriate movement
using UnityEngine;
using Asteroids.Interface;

namespace Asteroids.MovableObject.Bullet
{
    //inherits from base abstract class for all MovableObject (movement)
    //implements IBullet interface <- model interface (provides information who is owner of bullet, and destroying method)
    public class BulletModel : MovableObjectModel, IBullet
    {
        public float Range { get; set; }
        //not used, the interface requirements
        public int Lives { get; set; }
        public bool isDestroyed { get; set; }
        //provides FriendlyFire feature
        public AbstractController Owner { get; set; }

        //set direction, speed and range
        public BulletModel(Transform _objectTransform) 
        {
            Range = 10;
            objectTransform = _objectTransform;
            int acuteAngle = Mathf.RoundToInt(objectTransform.eulerAngles.z) % 90;
            speed = new Vector2(Mathf.Cos(Mathf.Deg2Rad * (90 - acuteAngle)),
                                                 Mathf.Cos(Mathf.Deg2Rad * acuteAngle));
            maxSpeed=0.1f;
            speed = CorrectSpeedDirection(speed);
            speed=speed.normalized*maxSpeed;
        }

        public override void Move()
        {  
            //Update position
            objectTransform.position += (Vector3)speed;
            Range -= speed.magnitude;
            Wrapping();
        }
        
        //animation
        public void Destruct(Sprite[] explosionSpriteArray) 
        {
            DrawParams = explosionSpriteArray;
            speed = Vector2.zero;
        }
    }
}

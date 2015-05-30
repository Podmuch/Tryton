//Bullet Controller
//  creates Bullet Model and View
//  destroy Bullet when It hit something or range will end 
using UnityEngine;
using Asteroids.Interface;
using Asteroids.Static;

namespace Asteroids.MovableObject.Bullet
{
    //inherits from base abstract class for all MovableObject Controllers (movement and drawing)
    public class BulletController : MovableObjectController
    {
        //static explosions (sprites imitating explosion). 
        //This could have been in MovableObjectController because it is repeated in each inheriting controller,
        // but not every moving object must explode (as it is currently)
        private StaticExplosion explosion;
        protected override void Awake()
        {
            base.Awake();
            model = new BulletModel(transform);
            explosion = FindObjectOfType<StaticExplosion>();
        }

        protected override void Update()
        {
            base.Update();
            if ((model as BulletModel).Range < 0)
                Death();     
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            //checking if the collider is not destroyed (eg. Explosion)
            if(!(other.GetComponent<AbstractController>().model as IDestructible).isDestroyed)
                Death();
        }

        private void Death() 
        {
            //select explosion color (blue - player bullets, red - enemy bullets)
            (model as BulletModel).Destruct((model as BulletModel).Owner.model as IPlayer != null ? explosion.blueExplosion : explosion.redExplosion);
            Destroy(gameObject, 0.35f);
        }
    }
}

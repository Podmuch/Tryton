//EnemyShip Controller
//  creates EnemyShip Model and View
//  handles collisions
//  provides shooting
using UnityEngine;
using Asteroids.Interface;
using System.Timers;
using Asteroids.GamePlay;
using Asteroids.Static;
using Asteroids.MovableObject.Player;

namespace Asteroids.MovableObject.Enemy.EnemyShip
{
    //inherits from base abstract class for all MovableObject Controllers (movement and drawing)
    //implement IShooter interface <- Controller interface provides information who is owner of the bullet 
    class EnemyShipController : MovableObjectController, IShooter
    {
        //Bullet prefab
        public Transform bullet=null;
        //Explosion prefab <- particle system
        public Transform firstExplosion = null;
        //static explosions (sprites imitating explosion). 
        //This could have been in MovableObjectController because it is repeated in each inheriting controller,
        // but not every moving object must explode (as it is currently)
        private StaticExplosion finalExplosion;
        //static sounds (shooting sound)
        private StaticSound sound;

        //when the EnemyShip is created, it size is random. Model must be added after awakening.
        public void AddModel(int lives)
        {
            model = new EnemyShipModel(transform, lives);
            finalExplosion = FindObjectOfType<StaticExplosion>();
            sound = FindObjectOfType<StaticSound>();
        }

        protected override void Update()
        {
            base.Update();
            Shoot();
        }
        //allows count the period between the shoots
        public float lastShoot { get; set; }
        public bool isShoot
        {
            get
            {
                //destroyed object can't shoot
                return !(model as EnemyShipModel).isDestroyed && Time.realtimeSinceStartup - lastShoot > 0.5f;
            }
        }

        public void Shoot()
        {
            if (isShoot)
            {
                //sound (different than player)
                if (sound.sounds[3].isPlaying)
                    sound.sounds[3].Stop();
                sound.sounds[3].Play();
                //creates new bullets (angle shooting is random from range -45, 45 degrees)
                Quaternion bulletRotation = transform.rotation;
                bulletRotation.eulerAngles += new Vector3(0, 0, Random.Range(-45, 45));
                Transform bulletPointer = (Transform)Instantiate(bullet, transform.position, bulletRotation);
                //set owner of the bullet
                (bulletPointer.GetComponent<AbstractController>().model as IBullet).Owner = this;
                bulletPointer.Translate(0, 0.5f, 0);
                lastShoot = Time.realtimeSinceStartup;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            //checking if the object is not destroyed
            if (!(model as EnemyShipModel).isDestroyed)
            {
                switch (other.tag)
                {
                    case "Player":
                        IPlayer player = other.GetComponent<AbstractController>().model as IPlayer;
                        if (player != null)
                            player.Score += ((model as EnemyShipModel).Points * (model as EnemyShipModel).Lives);
                            //select explosion color (blue - player bullets, red - enemy bullets)
                            //  if it was the last life, EnemyShip will be destroyed
                            //  else EnemyShip only change size and lost life (different explosions)
                            (model as EnemyShipModel).Destruct(finalExplosion.redExplosion);
                            if ((model as EnemyShipModel).Lives < 1)
                            {
                                Destroy(gameObject, 1);
                                //static variable containing a number of EnemyShips on the scene (prevents the creation of new enemyships)
                                GamePlayController.NumberOfEnemyShipsInGame--;
                            }
                            else
                                Instantiate(firstExplosion, transform.position, transform.rotation);
                        break;
                    case "Bullet":
                        //Get bullet and owner interfaces
                        IBullet bulletPointer = other.GetComponent<AbstractController>().model as IBullet;
                        IPlayer bulletOwner = bulletPointer.Owner.model as IPlayer;
                        //If FriendlyFire is off, only player could destroy EnemyShip (bullet shouldn't be destroyed)
                        if ((bulletOwner != null || GamePlayController.EnemyFriendlyFire) && !bulletPointer.isDestroyed)
                        {
                            //destroy bullet
                            bulletPointer.isDestroyed = true;
                            //if Player is bullet owner, give him points
                            if (bulletOwner != null)
                                bulletOwner.Score += ((model as EnemyShipModel).Points * (model as EnemyShipModel).Lives);
                            //select explosion color (blue - player bullets, red - enemy bullets)
                            //  if it was the last life, EnemyShip will be destroyed
                            //  else EnemyShip only change size and lost life (different explosions)
                            (model as EnemyShipModel).Destruct(bulletOwner != null ? finalExplosion.blueExplosion : finalExplosion.redExplosion);
                            if ((model as EnemyShipModel).Lives < 1)
                            {
                                Destroy(gameObject, 1);
                                //static variable containing a number of EnemyShips on the scene (prevents the creation of new enemyships)
                                GamePlayController.NumberOfEnemyShipsInGame--;
                            }
                            else
                                Instantiate(firstExplosion, transform.position, transform.rotation);
                        }
                        break;
                }
            }
        }
    }
}

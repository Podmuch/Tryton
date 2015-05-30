//Asteroid Controller
//  creates Asteroids Model and View
//  splites Asteroid after hit
//  handles collisions
using UnityEngine;
using Asteroids.Static;
using Asteroids.Interface;
using Asteroids.GamePlay;

namespace Asteroids.MovableObject.Enemy.Asteroid
{
    //inherits from base abstract class for all MovableObject Controllers (movement and drawing)
    public class AsteroidController : MovableObjectController
    {
        //static explosions (sprites imitating explosion). 
        //This could have been in MovableObjectController because it is repeated in each inheriting controller,
        // but not every moving object must explode (as it is currently)
        private StaticExplosion explosion;

        //when the Asteroid is created, it size is random. Model must be added after awakening.
        public void AddModel(int lives)
        {
            model= new AsteroidModel(transform, lives);
            explosion = FindObjectOfType<StaticExplosion>();
        }

        //the Asteroid can be splited when is hit 
        private void Fragmentation(int fragments)
        {
            //Lives determine the size of the Asteroid (small asteroids can't be splited)
            if((model as AsteroidModel).Lives>0)
            {
                for(int i=0;i<fragments;i++)
                {
                    Transform pieceOfAsteroid = (Transform)Instantiate(transform, transform.position, transform.rotation);
                    pieceOfAsteroid.eulerAngles = new Vector3(0, 0, Random.Range(0, 360));
                    //localScale should be reset before adding model becouse It's multiplied by the number of lives
                    pieceOfAsteroid.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    pieceOfAsteroid.gameObject.GetComponent<AsteroidController>().AddModel((model as AsteroidModel).Lives);
                    //static variable containing a number of asteroids on the scene (prevents the creation of new asteroids)
                    GamePlayController.NumberOfAsteroidsInGame++;
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            //checking if the object is not destroyed
            if (!(model as AsteroidModel).isDestroyed)
            {
                switch (other.tag)
                {
                    case "Bullet":
                        //Get bullet and owner interfaces
                        IBullet bulletPointer=other.GetComponent<AbstractController>().model as IBullet;
                        IPlayer bulletOwner = bulletPointer.Owner.model as IPlayer;
                        //If FriendlyFire is off, only player could destroy Asteroid (bullet shouldn't be destroyed)
                        if ((bulletOwner != null || GamePlayController.EnemyFriendlyFire) && !bulletPointer.isDestroyed)
                        {
                            //destroy bullet
                            bulletPointer.isDestroyed = true;
                            //if Player is bullet owner, give him points
                            if(bulletOwner != null)
                                bulletOwner.Score += ((model as AsteroidModel).Points * (model as AsteroidModel).Lives);
                            //select explosion color (blue - player bullets, red - enemy bullets)
                            (model as AsteroidModel).Destruct(bulletOwner != null ? explosion.blueExplosion : explosion.redExplosion);
                            //Split the Asteroid into two pieces
                            Fragmentation(2);
                            Destroy(gameObject, 1);
                            //static variable containing a number of asteroids on the scene (prevents the creation of new asteroids)
                            GamePlayController.NumberOfAsteroidsInGame--;
                        }
                        break;
                }
            }
        }
    }
}

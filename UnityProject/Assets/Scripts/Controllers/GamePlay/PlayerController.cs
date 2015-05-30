//Player Controller
//  creates Player Model and View
//  handles collisions
//  provides shooting
//  provides Untouchable in first seconds after death
//  counts score
using UnityEngine;
using System.Timers;
using Asteroids.Interface;
using Asteroids.Static;
using Asteroids.GamePlay;

namespace Asteroids.MovableObject.Player
{
    //inherits from base abstract class for all MovableObject Controllers (movement and drawing)
    //implement IShooter interface <- Controller interface provides information who is owner of the bullet 
    public class PlayerController : MovableObjectController, IShooter
    {
        //Bullet prefab
        public Transform bullet;
        //Timer allows complete animation of dying and spawn player back
        private Timer DyingAnimationTimer;
        //static explosions (sprites imitating explosion). 
        //This could have been in MovableObjectController because it is repeated in each inheriting controller,
        // but not every moving object must explode (as it is currently)
        private StaticExplosion explosion;
        //static sounds (shooting sound)
        private StaticSound sound;
        //provides Untouchable in first seconds after death
        private float UntouchableTime;
        private float UntouchableTimeMax;
        //live bonus for 10000 points
        private int lastScoreInLiveBonus;
        protected override void Awake()
        {
            base.Awake();
            model = new PlayerModel(transform);
            explosion = FindObjectOfType<StaticExplosion>();
            sound = FindObjectOfType<StaticSound>();
            DyingAnimationTimer = new Timer(500);
            DyingAnimationTimer.Elapsed += AnimationTime;
            UntouchableTimeMax = 5.00f;
            UntouchableTime = UntouchableTimeMax+0.01f;
            lastScoreInLiveBonus = 0;
        }

        protected override void Update()
        {
            if ((model as PlayerModel).Lives > 0)
            {
                base.Update();
                //Shoot();
                Untouchable();
                BonusLive();
            }
        }
        //allows count the period between the shoots
        public float lastShoot { get; set; }
        public bool isShoot
        {
            get
            {
                //destroyed object can't shoot
                return !(model as PlayerModel).isDestroyed && Time.realtimeSinceStartup-lastShoot> 0.5f;
            }
        }
        public void Shoot()
        {
            if(isShoot)
            {
                //sound (different than enemyship)
                if (sound.sounds[2].isPlaying)
                    sound.sounds[2].Stop();
                sound.sounds[2].Play();
                //player shoots forward
                Transform bulletPointer=(Transform)Instantiate(bullet, transform.position, transform.rotation);
                //set owner of the bullet
                (bulletPointer.GetComponent<AbstractController>().model as IBullet).Owner = this;
                bulletPointer.Translate(0, 0.5f, 0);
                lastShoot = Time.realtimeSinceStartup;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            //checking if the player is not destroyed and is touchable
            if (!(model as PlayerModel).isDestroyed&&!(model as PlayerModel).isUntouchable)
            {
                switch (other.tag)
                {
                    case "Enemy":
                        if (!(other.GetComponent<AbstractController>().model as IEnemy).isDestroyed)
                            Death();
                        break;
                    case "Bullet":
                        IBullet bulletPointer = other.GetComponent<AbstractController>().model as IBullet;
                        //If FriendlyFire is off, only enemyShip could destroy player (bullet shouldn't be destroyed)
                        if ((bulletPointer.Owner.model as IPlayer == null || GamePlayController.PlayerFriendlyFire) &&!bulletPointer.isDestroyed)
                            Death();
                        break;
                }
            }
        }

        private void BonusLive()
        {
            if ((model as PlayerModel).Score - lastScoreInLiveBonus > 10000)
            {
                lastScoreInLiveBonus += 10000;
                (model as PlayerModel).Lives++;
            }
        }

        private void Untouchable()
        {
            if (UntouchableTime > 0)
            {
                if (UntouchableTime > UntouchableTimeMax)
                    transform.position = Vector3.zero;
                UntouchableTime -= Time.deltaTime;
                //animation
                if (UntouchableTime - Mathf.Floor(UntouchableTime) > 0.5f)
                    (renderer as SpriteRenderer).color -= new Color(0.1f, 0.1f, 0.1f, 0.0f);
                else
                    (renderer as SpriteRenderer).color += new Color(0.1f, 0.1f, 0.1f, 0.0f);
            }
            else
                //back to normal colors and disable untouchable
                if ((model as PlayerModel).isUntouchable)
                {
                    (model as PlayerModel).isUntouchable = false;
                    (renderer as SpriteRenderer).color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                }
        }
        private void Death()
        {
            (model as PlayerModel).Destruct(explosion.redExplosion);
            DyingAnimationTimer.Start();
        }

        //restore previous settings and set untouchable
        private void AnimationTime(object sender, ElapsedEventArgs e)
        {
            (model as PlayerModel).stopMove = false;
            (view as MovableObjectView).ResetView(spriteArray);
            (model as PlayerModel).isDestroyed = false;
            DyingAnimationTimer.Stop();
            UntouchableTime = UntouchableTimeMax + 0.01f;
            (model as PlayerModel).isUntouchable = true;
        }
    }
}

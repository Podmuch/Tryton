//Player model
//  provides player control and movement 
using UnityEngine;
using Asteroids.Interface;
using Asteroids.GamePlay;

namespace Asteroids.MovableObject.Player
{
    //inherits from base abstract class for all MovableObject (movement)
    //implements IPlayer interface <- model interface (points, untouchable and destroying)
    public class PlayerModel : MovableObjectModel, IPlayer
    {
        //IDestructible
        public bool isDestroyed { get; set; }
        public int Lives { get; set; }
        //IPlayer
        public int Score { get; set; }
        public bool isUntouchable { get; set; }
        //Movement properties
        public bool stopMove { get; set; }

        private float maxDistance = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f).magnitude;
        private float currentDistance;
        private float acuteAngle;
        //calculation of the rotation angle from the position of the mouse / touch
        private float Angle
        {
            get
            {
                Vector2 mapSize=new Vector2(GamePlayModel.rightBottomCorner.x-GamePlayModel.leftTopCorner.x,
                                            GamePlayModel.leftTopCorner.y - GamePlayModel.rightBottomCorner.y);
                Vector2 touchPosition = (Input.touchCount > 0) ? Input.GetTouch(0).position : (Vector2)Input.mousePosition;
                //conversion pixel positions on the gameplay position
                touchPosition.x = touchPosition.x * mapSize.x / Screen.width - mapSize.x * 0.5f;
                touchPosition.y = touchPosition.y * mapSize.y / Screen.height - mapSize.y * 0.5f;
                touchPosition.x -= objectTransform.position.x;
                touchPosition.y -= objectTransform.position.y;
                currentDistance = touchPosition.magnitude;
                return ComputeAngle(touchPosition);
            }
        }
        //fixes to the rotation (which quarter)
        private float ComputeAngle(Vector2 Distance)
        {
            int Quarter;
            acuteAngle = Mathf.Rad2Deg;
            if (Distance.x < 0 && Distance.y < 0)
                Quarter = 1;
            else
            {
                if (Distance.x > 0 && Distance.y > 0)
                    Quarter = 3;
                else
                {
                    if (Distance.x > 0)
                        Quarter = 2;
                    else
                        Quarter = 0;
                }
            }
            if (Quarter % 2 == 0)
                acuteAngle *= Mathf.Acos(Mathf.Abs(Distance.y) / currentDistance);
            else
                acuteAngle *= Mathf.Acos(Mathf.Abs(Distance.x) / currentDistance);
            return acuteAngle + 90 * Quarter;
        }

        //calculation of the current acceleration from the distance to position of the mouse / touch
        private float CurrentAcceleration
        {
            get
            {
                return (maxDistance < currentDistance) ? acceleration : acceleration * currentDistance / maxDistance;
            }
        }
        //detect mouse/touch
        private bool isTapped
        {
            get
            {
                return Input.touchCount > 0 || Input.mousePresent;
            }
        }

        //initial player parameters
        public PlayerModel(Transform _objectTransform)
        {
            isUntouchable = true;
            isDestroyed = false;
            Lives = 5;
            Score = 0;
            objectTransform = _objectTransform;
            speed = Vector2.zero;
            maxSpeed = 0.05f;
            acceleration = 0.001f;
        }

        //Movement
        public override void Move()
        {
            if (stopMove)
                return;
            if (isTapped)
            {
                //Update Rotation if player is pressing key
                objectTransform.rotation = Quaternion.Euler(new Vector3(0, 0, Angle));
                //Update Speed       
                Vector2 deltaSpeed = new Vector2(Mathf.Cos(Mathf.Deg2Rad * (90 - acuteAngle)) * acceleration,
                                                    Mathf.Cos(Mathf.Deg2Rad * acuteAngle) * acceleration);
                deltaSpeed = CorrectSpeedDirection(deltaSpeed);
                if ((speed + deltaSpeed).magnitude < maxSpeed)
                    speed += deltaSpeed;
                else
                    speed = (deltaSpeed.normalized + speed.normalized).normalized * maxSpeed; 
            }
            //Update position
            objectTransform.position += (Vector3)speed;
            Wrapping();
        }
        //stops move, draws explosion and set that object is destroyed
        public void Destruct(Sprite[] explosionSpriteArray)
        {
            DrawParams = explosionSpriteArray;
            speed = Vector2.zero;
            isDestroyed = true;
            stopMove = true;
            Lives--;
        }
    }
}

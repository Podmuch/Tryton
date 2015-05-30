//MovableObject model
//  abstract model for all Movable Objects
using UnityEngine;
using Asteroids.GamePlay;

namespace Asteroids.MovableObject
{
    //inherits from base abstract class for all Models (drawing)
    public abstract class MovableObjectModel : AbstractModel
    {
        //movement parameters
        protected Transform objectTransform;
        protected Vector2 speed;
        protected float maxSpeed;
        protected float acceleration;

        //wrapping properties
        private bool isOutofLeft
        {
            get
            {
                return objectTransform.position.x < GamePlayModel.leftTopCorner.x;
            }
        }
        private bool isOutofRight
        {
            get
            {
                return objectTransform.position.x > GamePlayModel.rightBottomCorner.x;
            }
        }
        private bool isOutofTop
        {
            get
            {
                return objectTransform.position.y > GamePlayModel.leftTopCorner.y;
            }
        }
        private bool isOutofBottom
        {
            get
            {
                return objectTransform.position.y < GamePlayModel.rightBottomCorner.y;
            }
        }
        //necessary to determine the direction of movement
        private int whichQuarter(float rotation)
        {
            return Mathf.RoundToInt(rotation) / 90;
        }
        //wrapping (same for all movable objects)
        protected void Wrapping()
        {
            Vector3 currentPosition= objectTransform.position;
            if (isOutofLeft) currentPosition.x = GamePlayModel.rightBottomCorner.x - 0.2f;
            if (isOutofRight) currentPosition.x = GamePlayModel.leftTopCorner.x + 0.2f;
            if (isOutofTop) currentPosition.y = GamePlayModel.rightBottomCorner.y + 0.2f;
            if (isOutofBottom) currentPosition.y = GamePlayModel.leftTopCorner.y - 0.2f;
            objectTransform.position = currentPosition;
        }
        //fixes to the speed
        protected Vector2 CorrectSpeedDirection(Vector2 deltaSpeed)
        {
            float swapTmp;
            switch (whichQuarter(objectTransform.eulerAngles.z))
            {
                // 0 | 3
                // -----  Number of Quarter
                // 1 | 2
                case 2:
                    deltaSpeed.y = -deltaSpeed.y;
                    break;
                case 3:
                    swapTmp = deltaSpeed.x;
                    deltaSpeed.x = deltaSpeed.y;
                    deltaSpeed.y = swapTmp;
                    break;
                case 0:
                    deltaSpeed.x = -deltaSpeed.x;
                    break;
                case 1:
                    swapTmp = -deltaSpeed.x;
                    deltaSpeed.x = -deltaSpeed.y;
                    deltaSpeed.y = swapTmp;
                    break;
            }
            return deltaSpeed;
        }
        public abstract void Move();
    }
}

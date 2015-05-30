//MovableObject view (the same for all movable Objects)
//  getting parameters from model and change sprites in renderer <-animation
using UnityEngine;
using System.Timers;

namespace Asteroids.MovableObject
{
    //inherits from base abstract class for all Views (drawing)
    public class MovableObjectView : AbstractView
    {
        //Sprites <- initial is object sprites but when he is destroyed model sends explosion sprites
        private Sprite[] spriteArray;
        private SpriteRenderer renderer;
        private int currentSprite;
        //allows count the period between the shoots
        private float lastChangeSprite;
        private float changeSpriteRate;

        private bool isChangeSpriteAvailable
        {
            get
            {
                return Time.realtimeSinceStartup - lastChangeSprite > changeSpriteRate;
            }
        }

        public MovableObjectView(Renderer _renderer, Sprite[] _spriteArray)
        {
            renderer = _renderer as SpriteRenderer;
            spriteArray = _spriteArray;
            currentSprite = 0;
            changeSpriteRate = 0.05f;
        }
        //reset view (for player)
        public void ResetView(Sprite[] _spriteArray)
        {
            spriteArray = _spriteArray;
            currentSprite = 0;
            changeSpriteRate = 0.05f;
        }

        public override bool Draw(System.Object drawParams)
        {
            bool returnValue = false;
            if (isChangeSpriteAvailable)
            {
                //if params are null object isn't destroyed;
                if (drawParams != null)
                {
                    spriteArray = drawParams as Sprite[];
                    currentSprite = 0;
                    //faster animation (more Sprites available)
                    changeSpriteRate = 0.03f;
                    drawParams = null;
                    returnValue = true;
                }
                renderer.sprite = spriteArray[currentSprite];
                currentSprite = (currentSprite + 1) % spriteArray.Length;
                lastChangeSprite = Time.realtimeSinceStartup;
            }
            return returnValue;
        }
    }
}

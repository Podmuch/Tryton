//Abstract View
//  base view for all views
//  provides drawing (draw method)
using UnityEngine;

namespace Asteroids
{
    public abstract class AbstractView
    {
        protected Vector2 size;
        protected Vector2 margin;
        protected GUIStyle style;
        protected static float resolutionScale = (float)Screen.width / Screen.height;
        public abstract bool Draw(System.Object drawParams);
    }
}

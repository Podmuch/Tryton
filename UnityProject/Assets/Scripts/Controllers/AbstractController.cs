//Abstract Controller
//  base controller for all controllers
//  gets parameters from model and send it to view
using UnityEngine;

namespace Asteroids
{
    public abstract class AbstractController : MonoBehaviour
    {
        public AbstractModel model;
        protected AbstractView view;

        protected virtual void OnGUI()
        {
            if (model != null)
                if (view.Draw(model.DrawParams))
                    model.DrawParams = null;
        }
    }
}

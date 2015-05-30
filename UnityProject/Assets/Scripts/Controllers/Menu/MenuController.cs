//Menu Controller
//  create menu model and model view
using UnityEngine;

namespace Asteroids.Menu
{
    public class MenuController : AbstractController
    {
        public Texture2D normalButton;
        public Texture2D hoverButton;
        public Transform backgroundImage;
        public Texture2D Title;
        private void Awake()
        {
            model = new MenuModel(() => { Application.LoadLevel(1); });
            view = new MenuView(normalButton, hoverButton, backgroundImage, Title);
        }
    }
}
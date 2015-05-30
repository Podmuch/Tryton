//Menu View
//  displays title and play button
//  calls onClick when user clicked play button
using UnityEngine;
using System;

namespace Asteroids.Menu {
    //inherits from base abstract class for all Views (drawing)
    public class MenuView : AbstractView
    {
        private GUIStyle titleStyle;
        public MenuView(Texture2D _normalButton, Texture2D _hoverButton, Transform _backgroundImage, Texture2D _title)
        {
            size = new Vector2(Screen.width * 0.4f, Screen.height * 0.3f);
            margin = new Vector2(Screen.width * 0.5f, Screen.height * 0.6f);
            style = new GUIStyle();
            style.normal.background = _normalButton;
            style.hover.background = _hoverButton;
            titleStyle = new GUIStyle();
            titleStyle.normal.background = _title;
            float imageResolutionScale = 1024.0f / 2048.0f;
            _backgroundImage.localScale = new Vector3(resolutionScale * imageResolutionScale * _backgroundImage.localScale.y,
                                                        _backgroundImage.localScale.y, _backgroundImage.localScale.z);
        }

        //onClick is a parameter from model
        public override bool Draw(System.Object drawParams)
        {
            Action onClick = drawParams as Action;
            GUI.Box(new Rect(Screen.width * 0.5f, Screen.height * 0.05f, Screen.width * 0.4f, Screen.height * 0.5f), "", titleStyle);
            if (GUI.Button(new Rect(margin.x, margin.y, size.x, size.y), "", style))
                onClick();
            return false;
        }
    }
}

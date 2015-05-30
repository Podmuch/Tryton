//Gameplay view
//  getting parameters from model and draw them
using UnityEngine;
using Asteroids.Interface;
using System;

namespace Asteroids.GamePlay
{
    //inherits from base abstract class for all Views (drawing)
    public class GamePlayView : AbstractView
    {
        private int lastScore;
        private string nick;
        private Vector2 finalWindowSize;
        private Vector2 finalWindowMargin;
        //scales background image, set styles for gui (displaying score and points)
        public GamePlayView(Transform _backgroundImage)
        {
            float imageResolutionScale = 900.0f / 1600.0f;
            _backgroundImage.localScale = new Vector3(resolutionScale * imageResolutionScale * _backgroundImage.localScale.y,
                                                        _backgroundImage.localScale.y, _backgroundImage.localScale.z);
            size = new Vector2(250,45);
            finalWindowSize = new Vector2(250, 200);
            finalWindowMargin = new Vector2((Screen.width - finalWindowSize.x) * 0.5f, (Screen.height - finalWindowSize.y) * 0.5f);
            margin = Vector2.zero;
            style = new GUIStyle();
            style.normal.textColor = Color.white;
            style.wordWrap = false;
            style.fontSize = 20;
            style.alignment = TextAnchor.UpperCenter;
            nick = "Enter your Nick";
        }

        //drawing (controller calls this method in OnGUI())
        public override bool Draw(System.Object drawParams)
        {
            IPlayer player = drawParams as IPlayer;
            //if parameter is player model draw scores and lives
            if (player != null) { 
                GUI.Box(new Rect(margin.x, margin.y, size.x, size.y), "PLAYER", style);
                GUI.Box(new Rect(margin.x, margin.y + size.y * 0.5f, size.x * 0.5f, size.y * 0.5f), "LIVES: " + player.Lives, style);
                GUI.Box(new Rect(margin.x + size.x * 0.5f, margin.y + size.y * 0.5f, size.x * 0.5f, size.y * 0.5f), "SCORE: " + player.Score, style);
                lastScore=player.Score;
            }
            //else draw final Window which textfield for nick and button to save score to highscores and back to menu
            else
            {
                Action<string> onClick = drawParams as Action<string>;
                GUIStyle boxStyle = new GUIStyle(GUI.skin.box);
                boxStyle.fontSize = 30;
                boxStyle.alignment = TextAnchor.UpperCenter;
                GUI.Box(new Rect(finalWindowMargin.x, finalWindowMargin.y, finalWindowSize.x, finalWindowSize.y), "GAME OVER", boxStyle);
                GUI.Box(new Rect(finalWindowMargin.x, finalWindowMargin.y+finalWindowSize.y * 0.25f, finalWindowSize.x, finalWindowSize.y * 0.25f), "YOUR SCORE: " + lastScore, style);
                boxStyle.alignment = TextAnchor.LowerCenter;
                nick = GUI.TextField(new Rect(finalWindowMargin.x, finalWindowMargin.y + finalWindowSize.y * 0.5f, finalWindowSize.x, finalWindowSize.y * 0.25f), nick, boxStyle);
                //nick can't contain '+' and '*' becouse it used in highscore parser
                nick = nick.Split(new char[]{'+', '*'})[0];
                //max nick length
                if (nick.Length > 20) 
                    nick=nick.Substring(0, 20);
                if (GUI.Button(new Rect(finalWindowMargin.x, finalWindowMargin.y + finalWindowSize.y * 0.75f, finalWindowSize.x, finalWindowSize.y * 0.25f), "Continue", boxStyle))
                    onClick(nick);
            }
            return false;  
        }
    }
}

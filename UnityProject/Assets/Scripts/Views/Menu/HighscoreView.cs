//Highscore View
//  displays highscore table
using UnityEngine;
using System.Collections.Generic;

namespace Asteroids.Highscore
{
    //inherits from base abstract class for all Views (drawing)
    public class HighscoreView : AbstractView
    {
        private Vector2 recordSize;
        private Vector2 recordMargin;

        public HighscoreView()
        {
            size = new Vector2(Screen.width * 0.3f, Screen.height * 0.8f);
            margin = new Vector2(Screen.width * 0.1f, Screen.height * 0.1f);
            recordSize = new Vector2(Screen.width * 0.2f, Screen.height * 0.07f);
            recordMargin = new Vector2(Screen.width * 0.15f,Screen.height * 0.2f);
        }

        //drawParams is highscore list ( keyvaluepair list)
        public override bool Draw(System.Object drawParams){
            List<KeyValuePair<string,int>> highscores= drawParams as List<KeyValuePair<string,int>>;
            float recordOffset = 0;
            if(style==null)
                style = GUI.skin.box;
            style.wordWrap = false;
            style.fontSize = 20;
            GUI.Box(new Rect(margin.x, margin.y,size.x, size.y), "Highscore", style);
            style.fontSize = 10;
            //max ten scores is saving in highscore
            for (int i=0;i<10;i++)
            {
                //scale highscores if they are to large
                string record=highscores[i].Key + " " + highscores[i].Value.ToString();
                string record2 = highscores[i].Key;
                //all, only a few signs from nick or only score if display is really small
                record2 = (record2.Length>(int)recordSize.x/20)?record2.Substring(0,(int)recordSize.x/20):record2;
                record2=(recordSize.x <60)?highscores[i].Value.ToString():(record2+ "... " + highscores[i].Value.ToString());
                GUI.Box(new Rect(recordMargin.x, recordMargin.y + recordOffset, recordSize.x, recordSize.y), (recordSize.x <130)?record2:record, style);
                recordOffset += recordSize.y;
            }
            return false;
        }
    }
}

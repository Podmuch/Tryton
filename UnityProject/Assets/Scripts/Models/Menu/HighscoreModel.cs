//Highscore model
//  get highscores
//  add new record
//  save highscores
using UnityEngine;
using System.Collections.Generic;
using System.Text;

namespace Asteroids.Highscore
{
    //inherits from base abstract class for all Models (drawing)
    public class HighscoreModel : AbstractModel
    {

        private string highscore = null;
        //get or init highscores and draw them
        public HighscoreModel()
        {
            if (highscore == null)
            {
                highscore = PlayerPrefs.GetString("highscore");
                if (highscore == "") resetHighscore(10);
            }
            //send highscores to drawing
            DrawParams = ParseHighscore();
        }
        //add new record
        public void UpdateHighscore(KeyValuePair<string, int> newScore)
        {
            AddNewScore(newScore);
            PlayerPrefs.SetString("highscore", highscore);
        }

        //reset records
        public void resetHighscore(int length)
        {
            StringBuilder newHighscoreBuilder = new StringBuilder();
            List<KeyValuePair<string, int>> newHighscore = new List<KeyValuePair<string, int>>();
            for (int i = 0; i < length; i++)
            {
                newHighscore.Add(new KeyValuePair<string,int>("Random", 0));
                newHighscoreBuilder.Append("Random+0" + ((i != length-1) ? '*' : '\0'));
            }
            highscore = newHighscoreBuilder.ToString();
            PlayerPrefs.SetString("highscore", highscore);
        }

        //create new string to save it (if player scored to low, his record will be rejected)
        private void AddNewScore(KeyValuePair<string, int> newScore)
        {
            StringBuilder updatedHighscoreBuilder = new StringBuilder();
            string[] parsedHighscore = highscore.Split('*');
            int iterator = parsedHighscore.Length;
            bool isAdded= false;
            foreach (string record in parsedHighscore)
            {
                if (!isAdded)
                    isAdded =CompareRecords(updatedHighscoreBuilder, record, newScore, ref iterator);
                else
                    if (iterator != 1) updatedHighscoreBuilder.Append(record + '*');
                iterator--;
            }
            highscore = updatedHighscoreBuilder.ToString();
            highscore = highscore.Substring(0, highscore.Length - 1);
        }

        private bool CompareRecords(StringBuilder updatedHighscoreBuilder, string record, KeyValuePair<string, int> newScore, ref int iterator)
        {
            string[] recordCells = record.Split('+');
            if (newScore.Value > int.Parse(recordCells[1]))
            {
                updatedHighscoreBuilder.Append(newScore.Key + '+' + newScore.Value + '*');
                if (iterator != 1) updatedHighscoreBuilder.Append(record + '*');
                iterator--;
                return true;
            }
            updatedHighscoreBuilder.Append(record + '*');
            return false;
        }

        //change string to KeyValuePair list <- directory don't allow the same records
        private List<KeyValuePair<string, int>> ParseHighscore()
        {
            List<KeyValuePair<string, int>> returnDictionary = new List<KeyValuePair<string, int>>();
            foreach (string record in highscore.Split('*'))
            {
                string[] recordCells = record.Split('+');
                returnDictionary.Add(new KeyValuePair<string,int>(recordCells[0], int.Parse(recordCells[1])));
            }
            return returnDictionary;
        }
    }
}
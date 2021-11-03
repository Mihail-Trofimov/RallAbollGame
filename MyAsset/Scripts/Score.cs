using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RollABollGame
{
    public class Score : IView
    {
        private int _score;
        private int _scoreMax;
        private Text _text;
        public Score(Text text)
        {
            _text = text;
        }
        public void ScoreUp()
        {
            _score++;
            _text.text = _score.ToString();
        }
        public void ScoreMaxUp()
        {
            _scoreMax++;
        }
        public string DisplayScore()
        {
            return _score.ToString();
        }
        public string DisplayEndScore()
        {
            return _score.ToString() + " �� " + _scoreMax.ToString();
        }
    }
}
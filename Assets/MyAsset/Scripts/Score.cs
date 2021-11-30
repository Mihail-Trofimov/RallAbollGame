using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RollABollGame
{
    public class Score : IView
    {
        private int _score;
        public int Coin
        {
            get
            { return _score; }
            set
            { _score = value; }
        }
        private int _scoreMax;
        public int CoinMax
        {
            get
            { return _scoreMax; }
            set
            { _scoreMax = value; }
        }
        public Score()
        {
            _score = 0;
        }
        public void ScoreUp()
        {
            _score++;
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
            return _score.ToString() + " из " + _scoreMax.ToString();
        }
    }
}
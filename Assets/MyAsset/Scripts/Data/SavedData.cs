using System;
using System.Collections.Generic;
using UnityEngine;

namespace RollABollGame
{
    [Serializable]
    public sealed class SavedData
    {
        public Vector3Serializable PlayerPosition;
        public int PlayerScore;
        public int PlayerScoreMax;

        public void SavedPlayer(Vector3 position, int score, int scoreMax)
        {
            PlayerPosition = position;
            PlayerScore = score;
            PlayerScoreMax = scoreMax;
        }
        public List<int> KeysList = new List<int>();
        public void SavedKey(int key)
        {
            KeysList.Add(key);
        }
        public List<CoinSerializable> CoinList = new List<CoinSerializable>();
        public void SavedCoin(Coin coin)
        {
            Vector3 _pos = coin.transform.position;
            int _floor = Convert.ToInt32(coin.coinFloor);
            CoinList.Add(new CoinSerializable(_pos, _floor));
        }
    }
    [Serializable]
    public struct CoinSerializable
    {
        public Vector3Serializable CoinPosition;
        public int CoinFloor;

        public CoinSerializable(Vector3 position, int floor)
        {
            CoinPosition = position;
            CoinFloor = floor;
        }
    }
    [Serializable]
    public struct Vector3Serializable
    {
        public float X;
        public float Y;
        public float Z;

        private Vector3Serializable(float valueX, float valueY, float valueZ)
        {
            X = valueX;
            Y = valueY;
            Z = valueZ;
        }

        public static implicit operator Vector3(Vector3Serializable value)
        {
            return new Vector3(value.X, value.Y, value.Z);
        }

        public static implicit operator Vector3Serializable(Vector3 value)
        {
            return new Vector3Serializable(value.x, value.y, value.z);
        }

        public override string ToString() => $" (X = {X} Y = {Y} Z = {Z})";
    }
}


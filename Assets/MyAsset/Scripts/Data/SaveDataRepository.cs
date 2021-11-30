using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace RollABollGame
{
    public sealed class SaveDataRepository
    {
        private readonly IData<SavedData> _data;

        private const string _folderName = "dataSave";
        private const string _fileName = "data.bat";
        private readonly string _path;

        public SaveDataRepository()
        {
            _data = new SerializableXMLData<SavedData>();
            _path = Path.Combine(Application.dataPath, _folderName);
        }
        public bool ExistFile()
        {
            var file = Path.Combine(_path, _fileName);
            if (File.Exists(file))
                return true;
            return false;
        }
        public void Save(PlayerBase player, Score score, List<Coin> coins, List<int> keys)
        {
            if (!Directory.Exists(Path.Combine(_path)))
            {
                Directory.CreateDirectory(_path);
            }
            var saveGame = new SavedData
            {
                PlayerPosition = player.transform.position,
                PlayerScore = score.Coin,
                PlayerScoreMax = score.CoinMax,
            };
            foreach (Coin coin in coins)
            {
                saveGame.SavedCoin(coin);
            }
            foreach (int key in keys)
            {
                saveGame.SavedKey(key);
            }
            _data.Save(saveGame, Path.Combine(_path, _fileName));
        }

        public void Load(PlayerBase player, Score score, List<Coin> coins, List<Key> keys, Door[] doors, List<int> keysPlayer)
        {
            var file = Path.Combine(_path, _fileName);
            if (File.Exists(file))
            {
                var loadGame = _data.Load(file);

                player.transform.position = loadGame.PlayerPosition;
                score.Coin = loadGame.PlayerScore;
                score.CoinMax = loadGame.PlayerScoreMax;


                LoadCoin(loadGame.CoinList, coins, 0);
                Debug.Log("Load Coin 0 Floor Complete");
                LoadCoin(loadGame.CoinList, coins, 1);
                Debug.Log("Load Coin 1 Floor Complete");
                LoadCoin(loadGame.CoinList, coins, 2);
                Debug.Log("Load Coin 2 Floor Complete");

                LoadKey(keys, loadGame.KeysList, doors, keysPlayer);
                Debug.Log("Load Key Complete");
            }
        }

        private void LoadKey(List<Key> keysFromStartList, List<int> keysFromPreservList, Door[] doors, List<int> keysPlayer)
        {
            foreach (int key in keysFromPreservList)
            {
                keysPlayer.Add(key);
                foreach (Door door in doors)
                {
                    door.OpenDoor(key);
                }
                foreach (Key keyStart in keysFromStartList)
                {
                    if(Convert.ToInt32(keyStart.keyColor)==key)
                    {
                        keyStart.DestroyInteractiveObject();
                    }
                }
            }
        }


        private void LoadCoin(List<CoinSerializable> coinsFromPreservList, List<Coin> coinsFromStartList, int floor)
        {
            List<Coin> coinFloor = new List<Coin>();
            foreach (Coin coin in coinsFromStartList)
            {
                if (Convert.ToInt32(coin.coinFloor) == floor)
                {
                    coinFloor.Add(coin);
                }
            }
            List<CoinSerializable> loadCoin = new List<CoinSerializable>();
            foreach (CoinSerializable coin in coinsFromPreservList)
            {
                if (coin.CoinFloor == floor)
                {
                    loadCoin.Add(coin);
                }
            }
            for (int i = 0; i < loadCoin.Count; i++)
            {
                coinFloor[i].transform.position = loadCoin[i].CoinPosition;
            }
            for (int i = loadCoin.Count; i < coinFloor.Count; i++)
            {
                coinFloor[i].DestroyInteractiveObject();
            }
        }
    }
}


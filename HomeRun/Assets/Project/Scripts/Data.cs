using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Project.Scripts
{
    [CreateAssetMenu(fileName = "Data", menuName = "Data", order = 0)]
    public class Data : ScriptableObject
    {
        [SerializeField] private int coinAmount;
        [SerializeField] private List<Upgrade> levels;

        public int CoinAmount => coinAmount;

        public void AddCoins(int coins)
        {
            coinAmount += coins;
        }

        public void BuyLevel(int index)
        {
            coinAmount -= levels[index].cost[levels[index].Level];
            levels[index].LevelUp();
        }

        public bool CheckLevelPurchasable(int index)
        {
            return coinAmount >= levels[index].cost[levels[index].Level];
        }

        public Upgrade GetLevel(int index)
        {
            return levels[index];
        }

        public Upgrade GetLevel(Upgrades upgrade)
        {
            for (int i = 0; i < levels.Count; i++)
            {
                if (levels[i].upgrade == upgrade)
                    return levels[i];
            }

            return null;
        }
    }

    [Serializable]
    public class Upgrade
    {
        [SerializeField] internal Upgrades upgrade;
        [SerializeField] internal string name;
        [SerializeField] internal string description;
        [SerializeField] internal Sprite icon;
        [SerializeField] private int level;
        [SerializeField] internal List<int> cost;

        public int Level => level;

        public void LevelUp()
        {
            level++;
        }
    }

    [Serializable]
    public enum Upgrades
    {
        Strength,
        Magnet,
        MoneyWorth,
        Bouncyness,
        ForceStrength,
        MoreFans
    }
}
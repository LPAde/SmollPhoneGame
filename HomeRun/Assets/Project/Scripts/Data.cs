using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Project.Scripts
{
    [CreateAssetMenu(fileName = "Data", menuName = "Data", order = 0)]
    public class Data : ScriptableObject
    {
        [SerializeField] private int coinAmount;
        [SerializeField] private List<Upgrades> levels;

        public void AddCoins(int coins)
        {
            coinAmount += coins;
        }
    }

    [Serializable]
    struct Upgrades
    {
        [SerializeField] internal string name;
        [SerializeField] internal string description;
        [SerializeField] internal Sprite icon;
        [SerializeField] internal int level;
        [SerializeField] internal List<float> modifier;
    }
}
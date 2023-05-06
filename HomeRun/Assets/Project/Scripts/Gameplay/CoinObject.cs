using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Project.Scripts
{
    public class CoinObject : CollisionObject
    {
        [SerializeField] private int coinAmount;
        [SerializeField] private List<int> multipliers;

        protected override void DoSomething()
        {
            data.AddCoins(coinAmount);
        }

        protected override void Upgrade(int Level)
        {
            coinAmount *= multipliers[Level];
        }
    }
}
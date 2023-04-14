using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Project.Scripts
{
    public class CoinObject : CollisionObject
    {
        [SerializeField] private Data data;
        [SerializeField] private int coinAmount;

        protected override void DoSomething()
        {
            data.AddCoins(coinAmount);
        }
    }
}
using Assets.Project.Scripts.Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Project.Scripts.Upgrading
{
    public class Increaser : Upgradable
    {
        [SerializeField] private CollisionObject obj;
        [SerializeField] private List<int> multiplier;

        protected override void Upgrade(int Level)
        {
            obj.MultiplySpawnChance(multiplier[Level]);
        }
    }
}
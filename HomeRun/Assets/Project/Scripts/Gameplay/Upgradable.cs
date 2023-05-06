using Assets.Project.Scripts;
using UnityEngine;

namespace Assets.Project.Scripts.Gameplay
{
    public abstract class Upgradable : MonoBehaviour
    {
        [SerializeField] protected Data data;
        [SerializeField] private Upgrades upgrade;

        protected virtual void Start()
        {
            CheckUpgrade();
        }

        private void CheckUpgrade()
        {
            Upgrade(data.GetLevel(upgrade).Level);
        }

        protected abstract void Upgrade(int Level);
    }
}
using Assets.Project.Scripts.Gameplay;
using UnityEngine;

namespace Assets.Project.Scripts
{
    public abstract class CollisionObject : Upgradable
    {
        [SerializeField] private Animator anim;

        [Header("Spawning")]
        [SerializeField] private SpawnHeights spawnHeight;
        [SerializeField] private int spawnChance;

        public SpawnHeights SpawnHeight => spawnHeight;
        public int SpawnChance => spawnChance;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Ball"))
            {
                DoSomething();

                // Either plays animation or destroys itself.
                if (anim == null)
                    Destroy(gameObject);
                else
                    anim.SetTrigger("Hit");
            }
        }

        protected abstract void DoSomething();

        /// <summary>
        /// Multiplies the spawn chance with set value.
        /// </summary>
        /// <param name="multiplier"> What you want the spawn chance to be multiplied with. </param>
        public void MultiplySpawnChance(int multiplier)
        {
            spawnChance *= multiplier;
        }
    }
}

public enum SpawnHeights
{
    Everywhere = 0,
    Ground = 1,
    Sky = 2,
    Galaxy = 3
}
using UnityEngine;

namespace Assets.Project.Scripts
{
    public abstract class CollisionObject : MonoBehaviour
    {
        [SerializeField] private Animator anim;

        [Header("Spawning")]
        [SerializeField] private SpawnHeights spawnHeight;
        [SerializeField] private float maxDistance;
        [SerializeField] private float minDistance;
        [SerializeField] private float waggleFactor;

        public SpawnHeights SpawnHeight => spawnHeight;
        public float MaxDistance => maxDistance;
        public float MinDistance => minDistance;

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

        public void Waggle()
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + Random.Range(-waggleFactor, waggleFactor));

            if (transform.position.y < 0)
                Destroy(gameObject);
        }

        protected abstract void DoSomething();
    }
}

public enum SpawnHeights
{
    Everywhere,
    Ground,
    Sky,
    Galaxy
}
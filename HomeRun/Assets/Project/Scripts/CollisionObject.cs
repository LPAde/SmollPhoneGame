using UnityEngine;

namespace Assets.Project.Scripts
{
    public abstract class CollisionObject : MonoBehaviour
    {
        [SerializeField] private Animator anim;

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
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Project.Scripts
{
    public class BallBehaviour : MonoBehaviour
    {
        public static BallBehaviour Instance;

        [SerializeField] private GameObject ballFollowCam;
        [SerializeField] private Vector2 startVector;
        [SerializeField] private Vector2 startVelocity;
        
        [Header("Properties")]
        [SerializeField] private Rigidbody2D rigid;

        private void Awake()
        {
            if (Instance != null)
                Destroy(Instance);

            Instance = this;
        }

        private void Start()
        {
            startVector = transform.position;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                rigid.gravityScale = 0;
                transform.position = startVector;
                ballFollowCam.SetActive(false);
            }

            if (rigid.gravityScale == 1)
            {
                if (rigid.velocity == Vector2.zero)
                    SceneManager.LoadScene(0);
            }
            else
            {
                rigid.velocity = startVelocity;

                if (transform.position.x < -1)
                    transform.position = startVector;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Finish"))
            {
                Finish();
            }
        }

        public void Initialize()
        {
            rigid.gravityScale = 1;
            ballFollowCam.SetActive(true);
        }

        /// <summary>
        /// Adds force to the ball.
        /// </summary>
        /// <param name="force"> The direction and strength you want the force to be send. </param>
        public void AddForce(Vector2 force)
        {
            print("force start");
            // Ensure there are no negatives that hurt velocity.
            if (rigid.velocity.x < 0)
                rigid.velocity = new Vector2(0.1f, rigid.velocity.y);
            if (rigid.velocity.y < 0)
                rigid.velocity = new Vector2(rigid.velocity.x, 0.1f);
            print("actual force start");
            rigid.AddForce(force);
        }

        private void Finish()
        {
            SceneManager.LoadScene(0);
        }
    }
}
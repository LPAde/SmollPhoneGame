using Assets.Project.Scripts.Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Project.Scripts
{
    [DefaultExecutionOrder(-100)]
    public class BallBehaviour : Upgradable
    {
        public static BallBehaviour Instance;


        [SerializeField] private PhysicsMaterial2D physicsMat;
        [SerializeField] private List<float> modifiers;
        [SerializeField] private GameObject ballFollowCam;
        
        [Header("Math Stuff")]
        [SerializeField] private Vector2 startVector;
        [SerializeField] private Vector2 startVelocity;
        [SerializeField] private float minSpeed;
        [SerializeField] private Vector2 maxVelocity;

        [Header("Properties")]
        [SerializeField] private Rigidbody2D rigid;

        public Rigidbody2D Rigid => rigid;

        private void Awake()
        {
            if (Instance != null)
                Destroy(Instance);

            Instance = this;
        }

        protected override void Start()
        {
            base.Start();
            startVector = transform.position;
        }

        private void Update()
        {
            if (rigid.gravityScale == 1)
            {
                if (rigid.velocity.y == 0 && rigid.velocity.x < minSpeed)
                    SceneManager.LoadScene(0);
            }
            else
            {
                rigid.velocity = startVelocity;

                if (transform.position.x < -1)
                    transform.position = startVector;
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                rigid.gravityScale = 0;
                transform.position = startVector;
                ballFollowCam.SetActive(false);
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
            // Ensure there are no negatives that hurt velocity.
            if (rigid.velocity.x < 0)
                rigid.velocity = new Vector2(0.1f, rigid.velocity.y);
            if (rigid.velocity.y < 0)
                rigid.velocity = new Vector2(rigid.velocity.x, 0.1f);
   
            // Ensure the velocity doesn't get too high.


            rigid.AddForce(force);
        }

        private void Finish()
        {
            SceneManager.LoadScene(0);
        }

        protected override void Upgrade(int Level)
        {
            physicsMat.bounciness = modifiers[Level];
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Project.Scripts
{
    public class PlayerShot : MonoBehaviour
    {
        [SerializeField] private BallBehaviour ball;

        [Header("Player Stats")]
        [SerializeField] private float strength;
        [SerializeField] private float minDistance;

        void Start()
        {
            ball = BallBehaviour.Instance;
        }


        void Update()
        {
            if (Input.touchSupported)
            {
                if (Input.touchCount == 1)
                    ShootBall(Input.touches[0].position);
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                    ShootBall(Input.mousePosition);
            }
        }

        /// <summary>
        /// Shoots the ball based on the input position.
        /// </summary>
        /// <param name="inputPixelPosition"> Position of the input in pixel coordinates. </param>
        private void ShootBall(Vector2 inputPixelPosition)
        {
            // Setup.
            Vector2 inputWorldPosition = Camera.main.ScreenToWorldPoint(inputPixelPosition);
            float ballPosX = ball.transform.position.x;

            if (ballPosX < 0)
                ballPosX *= -1;

            if (ballPosX > minDistance)
                return;

            float ballModifier = 1 / (1 + ballPosX);

            // Calculations.
            Vector2 shootVector = inputWorldPosition.normalized * strength * ballModifier;
            print("try forcing");
            // Action.
            ball.Initialize();
            ball.AddForce(shootVector);
        }
    }
}
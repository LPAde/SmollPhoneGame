using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            return;

        rigid.velocity = startVelocity;

        if (transform.position.x < -1)
            transform.position = startVector;
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
        rigid.AddForce(force);
    }
}

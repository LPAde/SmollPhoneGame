using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    public static BallBehaviour Instance;

    [Header("Properties")]
    [SerializeField] private Rigidbody2D rigid;

    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance);

        Instance = this;
    }

    public void Initialize()
    {
        rigid.gravityScale = 1;
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

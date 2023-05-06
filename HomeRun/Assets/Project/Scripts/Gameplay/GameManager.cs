using System.Collections.Generic;
using UnityEngine;

namespace Assets.Project.Scripts
{
    public class GameManager : MonoBehaviour
    {
        [Header("Spawning")]
        [SerializeField] private List<float> heights;
        [SerializeField] private List<CollisionObject> spawnableObjects;
        [SerializeField] private GameObject spawnPosition;
        [SerializeField] private List<float> threshholds;
        [SerializeField] private float positionMultiplier;
        [SerializeField] private float maxTimer;
        [SerializeField] private float timer;

        [Header("Garbage Collector Optimization")]
        [SerializeField] private int reps;
        [SerializeField] private float startSpawnPosY;
        [SerializeField] private float maxSpawnPosY;
        [SerializeField] private int heightIndex;
        [SerializeField] private float lastXCoord;
        [SerializeField] private List<CollisionObject> objs;
        [SerializeField] private Vector2 spawnPos;
        [SerializeField] private int count;


        private void Update()
        {
            AdjustSpawnPosition(BallBehaviour.Instance.Rigid.velocity);

            if (timer < 0)
            {
                timer = maxTimer;
                var objs = GetPalpableCollisionObjects(GetCurrentHeight());
                SpawnObjects(objs, BallBehaviour.Instance.Rigid.velocity.y);
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }

        private void SpawnObjects(List<CollisionObject> objs, float yVelocity)
        {
            // Decide on Object
            int maxSpawnChance = 0;
            CollisionObject obj = null;

            for (int i = 0; i < objs.Count; i++)
            {
                maxSpawnChance += objs[i].SpawnChance;
            }

            int spawnID = Random.Range(0, maxSpawnChance);

            for (int i = 0; i < objs.Count; i++)
            {
                spawnID -= objs[i].SpawnChance;

                if(spawnID <= 0)
                {
                    obj = objs[i];
                    break;
                }
            }

            // Set Spawn Position of Object
            Vector2 pos = Vector2.zero;
            
            pos = (Vector2)spawnPosition.transform.position + (Random.insideUnitCircle * positionMultiplier);
            

            // Ensuring no negativ spawn position.
            if((pos.y < 0 || obj.SpawnHeight == SpawnHeights.Ground) || (pos.y < 0 && obj.SpawnHeight == SpawnHeights.Ground))
            {
                pos = new Vector2(pos.x, 0);

                // Descrease spawns when at ground.
                timer += .5f;
            }

            if(pos.x < BallBehaviour.Instance.transform.position.x)
            {
                pos = new Vector2(BallBehaviour.Instance.transform.position.x + 15, pos.y);
            }

            // Spawn the Object
            Instantiate(obj, pos, Quaternion.identity, transform);
        }

        /// <summary>
        /// Gets the current height of the ball and converts it to the enum.
        /// </summary>
        /// <returns> The current height as a enum. </returns>
        private SpawnHeights GetCurrentHeight()
        {
            SpawnHeights currentHeight = SpawnHeights.Everywhere;

            for (int i = heights.Count-1; i > 0; i--)
            {
                if (BallBehaviour.Instance.transform.position.y > heights[i])
                    break;

                currentHeight = (SpawnHeights)i;
            }
            
            return currentHeight;
        }

        /// <summary>
        /// Creates a list of spawnable collision objects based on the current height.
        /// </summary>
        /// <param name="currentHeight"> The current height in enum. </param>
        /// <returns> A list of collision objects. </returns>
        private List<CollisionObject> GetPalpableCollisionObjects(SpawnHeights currentHeight)
        {
            objs.Clear();

            for (int i = 0; i < spawnableObjects.Count; i++)
            {
                if (spawnableObjects[i].SpawnHeight == currentHeight || spawnableObjects[i].SpawnHeight == SpawnHeights.Everywhere)
                    objs.Add(spawnableObjects[i]);
            }

            return objs;
        }

        private void AdjustSpawnPosition(Vector2 playerVelo)
        {
            // Y.
            if (playerVelo.y > threshholds[0])
                playerVelo.y = threshholds[0];
            else if (playerVelo.y < threshholds[1])
                playerVelo.y = threshholds[1];

            spawnPosition.transform.position = new Vector3(spawnPosition.transform.position.x, BallBehaviour.Instance.transform.position.y + playerVelo.y, 0);
        }
    }
}
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Project.Scripts
{
    public class GameManager : MonoBehaviour
    {
        [Header("Spawning")]
        [SerializeField] private List<float> heights;
        [SerializeField] private List<int> heightReps;
        [SerializeField] private float minSpawnPos;
        [SerializeField] private float maxSpawnPos;
        [SerializeField] private List<CollisionObject> spawnableObjects;

        [Header("Garbage Collector Optimization")]
        [SerializeField] private int reps;
        [SerializeField] private float startSpawnPosY;
        [SerializeField] private float maxSpawnPosY;
        [SerializeField] private int heightIndex;
        [SerializeField] private float lastXCoord;
        [SerializeField] private CollisionObject obj;
        [SerializeField] private Vector2 spawnPos;
        [SerializeField] private int count;

        private void Start()
        {
            SpawnObjects();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
                SpawnObjects();
        }

        private void SpawnObjects()
        {
            for (int i = 0; i < spawnableObjects.Count; i++)
            {
                if (spawnableObjects[i].MaxDistance == 0)
                    continue;

                lastXCoord = minSpawnPos;
                spawnPos = new Vector2(Random.Range(lastXCoord, lastXCoord + spawnableObjects[i].MaxDistance), 0);

                switch (spawnableObjects[i].SpawnHeight)
                {
                    case SpawnHeights.Everywhere:

                        reps = heightReps[3];
                        startSpawnPosY = heights[0];
                        maxSpawnPosY = heights[3];

                        break;

                    case SpawnHeights.Ground:

                        reps = heightReps[0];
                        startSpawnPosY = heights[0];
                        maxSpawnPosY = heights[0];

                        break;

                    case SpawnHeights.Sky:

                        reps = heightReps[1];
                        startSpawnPosY = heights[1];
                        maxSpawnPosY = heights[2];

                        break;

                    case SpawnHeights.Galaxy:

                        reps = heightReps[2];
                        startSpawnPosY = heights[2];
                        maxSpawnPosY = heights[3];

                        break;
                }

                heightIndex = (int)((maxSpawnPosY - startSpawnPosY) / reps);
                spawnPos.y = startSpawnPosY;

                for (int j = 0; j < reps; j++)
                {
                    lastXCoord = minSpawnPos;
                    spawnPos.x = Random.Range(lastXCoord, lastXCoord + spawnableObjects[i].MaxDistance);
                    spawnPos.y += heightIndex;

                    while (maxSpawnPos - lastXCoord > 5)
                    {
                        obj = Instantiate(spawnableObjects[i], spawnPos, Quaternion.identity, transform);
                        count++;
                        obj.GetComponent<CollisionObject>().Waggle();

                        lastXCoord = Random.Range(lastXCoord, lastXCoord + spawnableObjects[i].MaxDistance);
                        spawnPos.x = lastXCoord + spawnableObjects[i].MinDistance;
                    }
                }
            }
        }
    }
}
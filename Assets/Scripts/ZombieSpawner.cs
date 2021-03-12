using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private int NumberOfZombiesToSpawn;

    [SerializeField] private GameObject[] ZombiePrefab;

    [SerializeField] private Spawner[] SpawnVolumes;

    private GameObject FollowGameObject;
    // Start is called before the first frame update
    void Start()
    {
        FollowGameObject = GameObject.FindGameObjectWithTag("Player");
        for (int i = 0; i < NumberOfZombiesToSpawn; i++)
        {
            SpawnZombie();
        }
    }

   void SpawnZombie()
   {
       GameObject zombieToSpawn = ZombiePrefab[Random.Range(0, ZombiePrefab.Length)];
       Spawner spawn = SpawnVolumes[Random.Range(0, SpawnVolumes.Length)];

        GameObject zombie = Instantiate(zombieToSpawn, spawn.GetPositionInBounds(), spawn.transform.rotation);

        zombie.GetComponent<ZombieComponent>().Initialize(FollowGameObject);
   }
}

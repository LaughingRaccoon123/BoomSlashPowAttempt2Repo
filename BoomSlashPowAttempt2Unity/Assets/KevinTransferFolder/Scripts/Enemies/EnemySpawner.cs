using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemySpawner;
    public List<GameObject> enemyPool = new List<GameObject>();

    private float timeBetweenMonsters = 3f;

    void Start()
    {
        StartCoroutine(SpawnEnemies(timeBetweenMonsters));
    }

    IEnumerator SpawnEnemies(float time)
    {
        while (enemyPool.Count > 0)
        {
            yield return new WaitForSeconds(time);

            Vector3 spawnPosition = enemySpawner.transform.position;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(enemySpawner.transform.position, out hit, 10.0f, NavMesh.AllAreas))
            {
                spawnPosition = hit.position - new Vector3(0, 0.5f, 0);
            }

            GameObject spawnedEnemy = Instantiate(enemyPool[0], spawnPosition, enemySpawner.transform.rotation);
            enemyPool.RemoveAt(0);

            NavMeshAgent agent = spawnedEnemy.GetComponent<NavMeshAgent>();
            if (agent != null)
            {
                agent.Warp(spawnPosition);
            }
        }
    }


}

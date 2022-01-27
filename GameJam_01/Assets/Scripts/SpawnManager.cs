using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct SpawnInfo
{
    public DIFFICULTY difficulty;
    public Transform pos;
    public float delay_min;
    public float delay_max;
}

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private int enemyNum;
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private List<GameObject> enemies = new List<GameObject>();
    [SerializeField] private SpawnInfo[] spawnInfos;

    private void Start()
    {
        StartCoroutine(CreateEnemies());

        SpawnEnemy();
    }

    IEnumerator CreateEnemies()
    {
        for(int i = 0; i < enemyPrefabs.Length; i++)
        {
            for (int j = 0; j < enemyNum; j++)
            {
                GameObject temp = Instantiate(enemyPrefabs[i], this.transform);
                temp.SetActive(false);
                enemies.Add(temp);
            }
        }

        yield return null;
    }

    public void SpawnEnemy()
    {
        for (int i = 0; i < spawnInfos.Length; i++)
        {
            StartCoroutine(SpawnEnemy(spawnInfos[i]));
        }
    }

    IEnumerator SpawnEnemy(SpawnInfo spawner)
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(spawner.delay_min, spawner.delay_max));

            List<GameObject> tempSpawnList = new List<GameObject>();

            foreach (GameObject e in enemies)
            {
                if (!e.activeSelf)
                {
                    if (!e.GetComponent<Enemy>().isDead)
                    {
                        if (e.GetComponent<Enemy>().difficulty == spawner.difficulty)
                        {
                            tempSpawnList.Add(e);
                        }
                    }
                }
            }

            int rnd = Random.Range(0, tempSpawnList.Count);

            tempSpawnList[rnd].transform.position = spawner.pos.position;
            tempSpawnList[rnd].SetActive(true);

        }
    }
}

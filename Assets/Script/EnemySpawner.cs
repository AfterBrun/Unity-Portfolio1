using System.Collections;
//using System.Diagnostics;
using NUnit.Framework;
using NUnit.Framework.Internal.Execution;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _enemyList;

    [SerializeField]
    private GameObject _boss;


    [SerializeField]
    private float[] _spawnXPoints = {-2.3f, -1.2f, 0.0f, 1.2f, 2.3f};
    [SerializeField]
    public float _spawnRate = 1.0f;
    [SerializeField]
    private float _startWait = 3.0f;
    private int[,] stage1 = new int[,] {{1, 0, 0, 0, 0}, 
                                        {0, 0, 1, 0, 0},
                                        {0, 1, 0, 1, 0},
                                        {1, 0, 0, 1, 0},
                                        {1, 0, 1, 0, 1},
                                        {1, 1, 1, 0, 0},
                                        {0, 1, 0, 0, 1},
                                        {1, 1, 1, 1, 1},
                                        {0, 1, 1, 1, 1},
                                        {1, 1, 0, 1, 1}};

    private int[,] stage2 = new int[,] {{0, 1, 0, 2, 0}, 
                                        {1, 1, 0, 0, 1},
                                        {1, 0, 1, 0, 2},
                                        {0, 1, 0, 3, 0},
                                        {2, 0, 1, 0, 2},
                                        {0, 1, 1, 0, 0},
                                        {0, 0, 2, 1, 0},
                                        {0, 1, 1, 0, 3},
                                        {0, 2, 0, 0, 0},
                                        {2, 1, 1, 1, 2}};

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartEnemySpawnRoutine();
    }

    void StartEnemySpawnRoutine() {
        StartCoroutine("FirstPhase");
    }

    IEnumerator FirstPhase() {
        yield return new WaitForSeconds(_startWait);
        for (int i = 0; i < stage1.GetLength(0); i++) {
            for (int j = 0; j < stage1.GetLength(1); j++) {
                if(stage1[i, j] <= 0) continue;
                else {
                    SpawnEnemy(stage1[i, j] - 1, _spawnXPoints[j]);
                }
            }
            yield return new WaitForSeconds(_spawnRate);
        }
        yield return StartCoroutine("SecondPhase");
    }

    IEnumerator SecondPhase() {
        yield return new WaitForSeconds(_startWait);
        for (int i = 0; i < stage2.GetLength(0); i++) {
            for (int j = 0; j < stage2.GetLength(1); j++) {
                if(stage2[i, j] <= 0) continue;
                else {
                    SpawnEnemy(stage2[i, j] - 1, _spawnXPoints[j]);
                }
            }
            yield return new WaitForSeconds(_spawnRate);
        }
        yield return StartCoroutine("BossBattle");
    }

    IEnumerator BossBattle() {
        yield return new WaitForSeconds(_startWait);
        Instantiate(_boss, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemy(int enemyIndex, float x) 
    {
        GameObject enemyObject = Instantiate(_enemyList[enemyIndex], new Vector3(x, transform.position.y, transform.position.z), 
                    Quaternion.identity);
        Fighter fighter;
        UniqueShip uniqueShip;
        if(fighter = enemyObject.GetComponent<Fighter>()) {
            float down = Random.Range(0f, 4f);
            fighter.SetDownPoint(down);
        }
        else if(uniqueShip = enemyObject.GetComponent<UniqueShip>()) {
            float down = Random.Range(0f, 4f);
            uniqueShip.SetDownPoint(down);
        }

    }

    public void StopSpawn() {
        StopAllCoroutines();
    }
}

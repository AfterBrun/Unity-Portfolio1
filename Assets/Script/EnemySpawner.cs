using System.Collections;
using NUnit.Framework;
using NUnit.Framework.Internal.Execution;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _enemyList;
    [SerializeField]
    private float[] _spawnXPoints = {-2.3f, -1.2f, 0.0f, 1.2f, 2.3f};
    [SerializeField]
    public float _spawnRate = 0.3f;
    [SerializeField]
    private float _startWait = 3.0f;
    private float _lastSpawnTime = 0.0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartEnemySpawnRoutine();
    }

    void StartEnemySpawnRoutine() {
        StartCoroutine("EnemySpawnRoutine");
    }

    IEnumerator EnemySpawnRoutine() {
        yield return new WaitForSeconds(_startWait);

        while(true) {
            foreach(float posX in _spawnXPoints) {
                int index = Random.Range(0, _enemyList.Length);
                SpawnEnemy(index, posX);
            }

            yield return new WaitForSeconds(_spawnRate);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemy(int enemyIndex, float x) 
    {
        Instantiate(_enemyList[enemyIndex], new Vector3(x, transform.position.y, transform.position.z), 
                    Quaternion.identity);
    }
}

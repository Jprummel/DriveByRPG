using System.Collections.Generic;
using UnityEngine;

public class RandomEnemySpawner : MonoBehaviour {

    [SerializeField] private List<GameObject> _enemies = new List<GameObject>();
    [SerializeField] private List<Transform> _enemySpawnPoints = new List<Transform>();
    [SerializeField] private Transform _enemyContainer;

	void Awake () {
        SpawnEnemies();
	}

    void SpawnEnemies()
    {
        int enemyAmountToSpawn = Random.Range(1,4);
        
        for (int i = 0; i < enemyAmountToSpawn; i++)
        {
            int enemyToSpawn = Random.Range(0, _enemies.Count);
            GameObject enemy = Instantiate(_enemies[enemyToSpawn]);
            SpriteRenderer sprRenderer = enemy.GetComponent<SpriteRenderer>();
            sprRenderer.sortingOrder = i;
            enemy.transform.position = _enemySpawnPoints[i].position;
            enemy.transform.SetParent(_enemyContainer);
        }
    }
}

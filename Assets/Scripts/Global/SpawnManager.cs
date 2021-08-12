using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer; 
    private bool _isSpawning = true;
    public bool IsSpawning
    {
        get=>_isSpawning;
        set
        {
            _isSpawning = value;
        }
    }
    private GeneralScene generalScene = GeneralScene.Instance;
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }
    IEnumerator SpawnRoutine()
    {
        while (this.IsSpawning)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-1 * (generalScene.XScreenBorder - 2), (generalScene.XScreenBorder - 2)), 7, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(3.0f);
        }
    }
    public void onPlayerDeath()
    {
        this.IsSpawning = false;
    }
}

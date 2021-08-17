using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : GlobalInfo
{
    // GameObjs
    [SerializeField]
    private GameObject _astroidPrefab;
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject[] _powerups;
    [SerializeField]
    private GameObject _powerUpsContainer;
    private bool _isSpawning = true;
    public bool IsSpawning
    {
        get => _isSpawning;
        set
        {
            _isSpawning = value;
        }
    }
    // Core Methods
    private void Start()
    {
        Instantiate(_astroidPrefab);
    }
    public void beginSpawning()
    {
        StartCoroutine(waitBeforeSpawning());
    }
    IEnumerator waitBeforeSpawning()
    {
        yield return new WaitForSeconds(5f);
        StartCoroutine(SpawnRoutineEnemies(_enemyPrefab, _enemyContainer, 3.0f));
        StartCoroutine(SpawnRoutinePowerUps());
    }
    // General Methods
    IEnumerator SpawnRoutineEnemies(GameObject prefab, GameObject container, float timeToWait)
    {
        while (_isSpawning)
        {
            GameObject spawn = instantiateGameObj(prefab);
            spawn.transform.parent = container.transform;
            yield return new WaitForSeconds(timeToWait);
        }
    }
    IEnumerator SpawnRoutinePowerUps()
    {
        while (_isSpawning)
        {
            GameObject spawn = instantiateGameObj(_powerups[generateTandomPowerUpId()]);
            spawn.transform.parent = _powerUpsContainer.transform;
            yield return new WaitForSeconds(Random.Range(8.0f, 15.0f));
        }
    }
    GameObject instantiateGameObj(GameObject prefab)
    {
        return Instantiate(prefab, generateVector3(), Quaternion.identity);
    }
    int generateTandomPowerUpId()
    {
        return Random.Range(0, 3);
    }
    Vector3 generateVector3()
    {
        return new Vector3(Random.Range(-1 * (XScreenBorder - 2), (XScreenBorder - 2)), YScreenBorder, 0);
    }
    // Player Methods
    public void onPlayerDeath()
    {
        this.IsSpawning = false;
    }
}

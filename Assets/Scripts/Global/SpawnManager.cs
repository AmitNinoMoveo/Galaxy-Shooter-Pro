using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // GameObjs
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private GameObject _tripleShotContainer;
    [SerializeField]
    private GameObject _speedUpPrefab;
    [SerializeField]
    private GameObject _speedUpContainer;
    // VARs
    private readonly GlobalInfo globalInfo = GlobalInfo.Instance;
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
    void Start()
    {
        StartCoroutine(SpawnRoutine(_enemyPrefab, _enemyContainer, 3.0f));
        StartCoroutine(SpawnRoutine(_tripleShotPrefab, _tripleShotContainer, Random.Range(8.0f, 15.0f)));
        StartCoroutine(SpawnRoutine(_speedUpPrefab, _speedUpContainer, Random.Range(8.0f, 15.0f)));
    }
    // General Methods
    IEnumerator SpawnRoutine(GameObject prefab, GameObject container, float timeToWait)
    {
        while (this.IsSpawning)
        {
            GameObject spawn = instantiateGameObj(prefab);
            spawn.transform.parent = container.transform;
            yield return new WaitForSeconds(timeToWait);
        }
    }
    GameObject instantiateGameObj(GameObject prefab)
    {
        return Instantiate(prefab, generateVector3(), Quaternion.identity);
    }
    Vector3 generateVector3()
    {
        return new Vector3(Random.Range(-1 * (globalInfo.XScreenBorder - 2), (globalInfo.XScreenBorder - 2)), globalInfo.YScreenBorder, 0);
    }
    // Player Methods
    public void onPlayerDeath()
    {
        this.IsSpawning = false;
    }
}

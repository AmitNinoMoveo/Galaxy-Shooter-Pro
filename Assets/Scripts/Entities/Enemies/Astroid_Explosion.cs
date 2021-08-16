using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astroid_Explosion : MonoBehaviour
{
    private SpawnManager _spawnManager;

    void Start()
    {
        StartCoroutine(handleExplosion());
    }
    IEnumerator handleExplosion()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}

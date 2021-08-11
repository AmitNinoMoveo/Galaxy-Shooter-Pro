using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;
    [SerializeField]
    private GameObject _enemyPrefab;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().decreaseLife();
            DestroyAndInitNewEnemy();
        }
        else if (other.tag == "Laser")
        {
            DestroyAndInitNewEnemy();
            Destroy(other.gameObject);
        };

    }
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        wrapOnBottomScreen();
    }
    void wrapOnBottomScreen()
    {
        if (transform.position.y < -7f)
        {
            transform.position = getRandomVector();
        }
    }
    Vector3 getRandomVector()
    {
        float randomX = Random.Range(-9f, 9f);
        return new Vector3(randomX, 7.5f, 0);
    }
    void DestroyAndInitNewEnemy()
    {
        Instantiate(_enemyPrefab, getRandomVector(), Quaternion.identity);
        Destroy(this.gameObject);
    }
}

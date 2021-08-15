using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    private GlobalInfo globalInfo = GlobalInfo.Instance;
    [SerializeField]
    private float _speed = 4f;
    public float Speed
    {
        get => _speed;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.gameObject.GetComponent<Player>();
            if(player.GetType() != null)
            {
                player.decreaseLife();
            }
            destroyAndInitNewEnemy();
            return;
        }
        else if (other.tag == "Projectiles")
        {
            destroyAndInitNewEnemy();
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
        float randomX = Random.Range(-1 * (globalInfo.XScreenBorder - 1), (globalInfo.XScreenBorder - 1));
        return new Vector3(randomX, 7.5f, 0);
    }
    void destroyAndInitNewEnemy()
    {
        Destroy(this.gameObject);
    }
    public GameObject generateNewEnemy()
    {
        return Instantiate(_enemyPrefab, getRandomVector(), Quaternion.identity);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private Player _player;
    private GlobalInfo globalInfo = GlobalInfo.Instance;
    [SerializeField]
    private float _speed = 4f;
    public float Speed
    {
        get => _speed;
    }
    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player.GetType() == null)
        {
            Debug.Log("Enemy Component: Player is null");
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _player.decreaseLife();
            _player.addScore(Random.Range(2, 5));
            destroyAndInitNewEnemy();
            return;
        }
        else if (other.tag == "Projectiles")
        {
            _player.addScore(Random.Range(8, 15));
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

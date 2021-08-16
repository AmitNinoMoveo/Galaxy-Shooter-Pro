using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astroid : MonoBehaviour
{
    // GameObjs
    [SerializeField]
    private GameObject _astroidAnimation;
    [SerializeField]
    private GameObject _explosionAudio;
    private SpawnManager _spawnManager;
    private CircleCollider2D _boxCollider;
    // VARs
    [SerializeField]
    private float _rotationSpeed = 20f;
    public float RotationSpeed
    {
        get => _rotationSpeed;
    }
    [SerializeField]
    private float _movementSpeed = 1.8f;
    public float MovementSpeed
    {
        get => _movementSpeed;
    }
    void Start()
    {
        transform.position = new Vector3(0, GlobalInfo.Instance.YScreenBorder, 0);
        _boxCollider = GetComponent<CircleCollider2D>();
        if (_boxCollider == null) Debug.Log("Astroid::Circle Collider 2D is null");
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if (_spawnManager == null) Debug.Log("Astroid::SpawnManager is null");
    }
    void Update()
    {
        calculateMovement();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.name == "Player")
        {
            Player player = other.gameObject.GetComponent<Player>();
            if (player.GetType() != null) player.decreaseLife();
            onAstroidDestroy();
        }
        if (other.tag == "Projectiles")
        {
            onAstroidDestroy();
            Destroy(other.gameObject);
        }
    }
    void calculateMovement()
    {
        if (transform.position.y >= -2f)
        {
            transform.Translate(Vector3.down * MovementSpeed * Time.deltaTime, Space.World);
        }
        transform.Rotate(Vector3.forward * RotationSpeed * Time.deltaTime);
    }
    void onAstroidDestroy()
    {
        _boxCollider.enabled = false;
        Instantiate(_astroidAnimation, transform.position, Quaternion.identity);
        _spawnManager.beginSpawning();
        Instantiate(_explosionAudio);
        Destroy(gameObject);
    }
}

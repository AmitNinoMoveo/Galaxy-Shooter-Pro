using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // OTHER COMPONENTS
    private GeneralScene generalScene = GeneralScene.Instance;
    private Life _life = new Life(3);
    public Life Life
    {
        get => _life;
    }
    // GAME OBJs
    [SerializeField]
    private GameObject _laserPrefab;
    private SpawnManager _spawnManager;
    //  VARs
    [SerializeField]
    private float _speed = 8f;
    [SerializeField]
    private float _fireRate = 0.15f;
    private float _cooldownTime = 0.0f;

    void Start()
    {
        transform.position = new Vector3(0f, 0f, 0f);
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if (_spawnManager == null)
        {
            Debug.LogError("SpawnManager is Null");
        }
    }
    void Update()
    {
        CalculateMovement();
        if (CheckIfFiringLaser()) FireLaser();
    }
    void CalculateMovement()
    {
        ifKeyPressedMoveObject();
        wrapIfOutOfScreen();
    }
    void ifKeyPressedMoveObject()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (System.Convert.ToBoolean(horizontalInput) || System.Convert.ToBoolean(verticalInput))
        {
            Vector3 vector = new Vector3(horizontalInput, verticalInput, 0);
            transform.Translate(vector * _speed * Time.deltaTime);
        };
    }
    void wrapIfOutOfScreen()
    {
        float yPosition = transform.position.y;
        float xPosition = transform.position.x;
        // Y Border Block
        transform.position = new Vector3(xPosition, Mathf.Clamp(yPosition, -1 * generalScene.YScreenBorder, generalScene.YScreenBorder), 0);
        // X Border Wrap
        if (xPosition >= (generalScene.XScreenBorder + 2))
        {
            transform.position = new Vector3(-1 * (generalScene.XScreenBorder + 1), yPosition, 0);
        }
        else if (xPosition <= -1 * (generalScene.XScreenBorder + 2))
        {
            transform.position = new Vector3((generalScene.XScreenBorder + 1), yPosition, 0);
        }
    }
    bool CheckIfFiringLaser()
    {
        return Input.GetKeyDown(KeyCode.Space) && Time.time > _cooldownTime;
    }
    void FireLaser()
    {
        _cooldownTime = Time.time + _fireRate;
        Vector3 v = transform.position + new Vector3(0, 1.1f, 0);
        Instantiate(_laserPrefab, v, Quaternion.identity);
    }
    public void decreaseLife(int amount = 1)
    {
        this.Life.decreseCurrentLife(amount);
        if (!this.Life.IsAlive) onDeath();
    }
    private void onDeath()
    {
        _spawnManager.onPlayerDeath();
        Destroy(this.gameObject);
    }
};
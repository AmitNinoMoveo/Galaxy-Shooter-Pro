using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // OTHER COMPONENTS
    private GlobalInfo globalInfo = GlobalInfo.Instance;
    private Life _life;
    // GAME OBJs
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    private SpawnManager _spawnManager;
    //  VARs
    [SerializeField]
    private float _speed = 8f;
    [SerializeField]
    private float _fireRate = 0.15f;
    [SerializeField]
    private bool _isTripleShot = false;
    public bool IsTripleShot
    {
        get => _isTripleShot;
        set
        {
            _isTripleShot = value;
        }
    }
    [SerializeField]
    private bool _isSpeedUp = false;
    public bool IsSpeedUp
    {
        get => _isSpeedUp;
        set
        {
            _isSpeedUp = value;
        }
    }
    private float _cooldownTime;

    // Core Methods
    void Start()
    {
        _life = transform.GetComponent<Life>();
        transform.position = new Vector3(0f, -1 * globalInfo.YScreenBorder + 1, 0f);
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if (_spawnManager == null)
        {
            Debug.LogError("SpawnManager is Null");
        }
    }
    void Update()
    {
        CalculateMovement();
        if (CheckIfFiring()) Fire();
    }
    IEnumerator setPowerUpTimer(System.Action func, float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        func();
    }
    // PowerUp Methods
    public void applyPowerUp(int id)
    {
        if (id == 0)
        {
            setTripleShot();
        }
        else if (id == 1)
        {
            setSpeedUp();
        }
    }
    // Movement Methods
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
            transform.Translate(vector * calculateSpeed() * Time.deltaTime);
        };
    }
    float calculateSpeed()
    {
        return IsSpeedUp ? _speed + 5f : _speed;
    }
    void wrapIfOutOfScreen()
    {
        float yPosition = transform.position.y;
        float xPosition = transform.position.x;
        // Y Border Block
        transform.position = new Vector3(xPosition, Mathf.Clamp(yPosition, -1 * globalInfo.YScreenBorder, globalInfo.YScreenBorder), 0);
        // X Border Wrap
        if (xPosition >= (globalInfo.XScreenBorder + 2))
        {
            transform.position = new Vector3(-1 * (globalInfo.XScreenBorder + 1), yPosition, 0);
        }
        else if (xPosition <= -1 * (globalInfo.XScreenBorder + 2))
        {
            transform.position = new Vector3((globalInfo.XScreenBorder + 1), yPosition, 0);
        }
    }
    void setSpeedUp()
    {
        IsSpeedUp = true;
        StartCoroutine(
            setPowerUpTimer(
            delegate () { IsSpeedUp = false; },
            5f)
        );
    }
    // Deal Damage Methods
    bool CheckIfFiring()
    {
        return Input.GetKeyDown(KeyCode.Space) && Time.time > _cooldownTime;
    }
    void Fire()
    {
        _cooldownTime = Time.time + _fireRate;
        if (IsTripleShot)
        {
            FireTripleShot();
            return;
        }
        else
        {
            FireDefaultProjectile();
        }
    }
    void FireDefaultProjectile()
    {
        Instantiate(_laserPrefab, transform.position, Quaternion.identity);
    }
    void FireTripleShot()
    {
        if (IsTripleShot) Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
    }
    void setTripleShot()
    {
        IsTripleShot = true;
        StartCoroutine(setPowerUpTimer(
            delegate () { IsTripleShot = false; },
            5f));
    }
    // Health Methods
    public void decreaseLife(int amount = 1)
    {
        _life.decreseCurrentLife(amount);
        if (!_life.IsAlive) onDeath();
    }
    private void onDeath()
    {
        _spawnManager.onPlayerDeath();
        Destroy(this.gameObject);
    }
};
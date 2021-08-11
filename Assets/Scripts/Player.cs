using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8f;
    [SerializeField]
    private float _fireRate = 0.15f;
    [SerializeField]
    private GameObject _laserPrefab;
    private float _cooldownTime = 0.0f;
    private float _yScreenLimit = 6f;
    private float _xScreenLimit = 13f;

    private Life life = new Life(3);
    public Life getLife()
    {
        return life;
    }


    void Start()
    {
        transform.position = new Vector3(0f, 0f, 0f);
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

        transform.position = new Vector3(xPosition, Mathf.Clamp(yPosition, -1 * _yScreenLimit, _yScreenLimit), 0);
        if (xPosition >= _xScreenLimit)
        {
            transform.position = new Vector3(-1 * (_xScreenLimit - 1), yPosition, 0);
        }
        else if (xPosition <= -1 * _xScreenLimit)
        {
            transform.position = new Vector3(_xScreenLimit - 1, yPosition, 0);
        }
    }
    bool CheckIfFiringLaser()
    {
        return Input.GetKeyDown(KeyCode.Space) && Time.time > _cooldownTime;
    }
    void FireLaser()
    {
        _cooldownTime = Time.time + _fireRate;
        Vector3 v = transform.position + new Vector3(0, 0.8f, 0);
        Instantiate(_laserPrefab, v, Quaternion.identity);
    }
    public void decreaseLife(int amount = 1)
    {
        life.decreseCurrentLife(amount);
        if (!life.isAlive()) Destroy(this.gameObject);
    }
};
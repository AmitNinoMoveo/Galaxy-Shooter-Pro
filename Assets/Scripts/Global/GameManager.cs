using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isGameOver;
    public bool IsGameOver
    {
        get => _isGameOver;
        set
        {
            _isGameOver = value;
        }
    }
    private void Start()
    {
        IsGameOver = false;
    }
    void Update()
    {
        if (IsGameOver == true && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(1); // Game Scene
        }
        if (IsGameOver == true && Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0); // Main Menu Scene
        }
    }
    public void setGameOver()
    {
        IsGameOver = true;
    }
}

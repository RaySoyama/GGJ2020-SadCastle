using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kernel : MonoBehaviour
{
    public static Kernel instance;

    public UIController uiController;

    void Start()
    {
        if (instance != null) { Destroy(this); Debug.LogWarning("Duplicate kernal detected. Destroying...", gameObject); return; }
        instance = this;
    }

    [SerializeField]
    Castle castle;

    public int knownCastleChunks { get; private set; }
    public int knownHealthyCastleChunks { get; private set; }
    public int knownDamagedCastleChunks { get { return knownCastleChunks - knownHealthyCastleChunks; } }

    bool gameOver;

    void Update()
    {
        if (!gameOver && knownHealthyCastleChunks == 0)
        {
            EndGame();
        }
    }

    void FixedUpdate()
    {
        knownCastleChunks = castle.totalCastleChunks;
        knownHealthyCastleChunks = castle.totalHealthyCastleChunks;
    }

    public void EndGame()
    {
        if(gameOver) { return; }
        gameOver = true;
        if (uiController == null)
        {
            Debug.LogWarning("Failed to locate UI controller! Failsafe: return to 'menutest' scene...");
            UnityEngine.SceneManagement.SceneManager.LoadScene("menutest");
            return;
        }
        uiController.ShowGameOverPanel();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class Kernel : MonoBehaviour
{
    public static Kernel instance;

    public UIController uiController;

    [SerializeField]
    Castle castle;

    public int knownCastleChunks { get; private set; }
    public int knownHealthyCastleChunks { get; private set; }
    public int knownDamagedCastleChunks { get { return knownCastleChunks - knownHealthyCastleChunks; } }

    bool gameOver;

    public UnityEvent OnGameOver;
    public AudioClip gameOverAudio;
    public AudioSource audioSource;

    public float gameOverMusicDelay = 0.5f;

    void Start()
    {
        if (instance != null) { Destroy(this); Debug.LogWarning("Duplicate kernal detected. Destroying...", gameObject); return; }
        instance = this;

        if (audioSource == null)
        {
            Debug.LogWarning("Audio Source not found! Searching for one...");
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                Debug.LogWarning("Audio Source not found! Adding one...");
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }

        OnGameOver?.AddListener(() => DOVirtual.DelayedCall(gameOverMusicDelay, ()=>audioSource.PlayOneShot(gameOverAudio)));
    }

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
        OnGameOver.Invoke();
        if (uiController == null)
        {
            Debug.LogWarning("Failed to locate UI controller! Failsafe: return to 'menutest' scene...");
            UnityEngine.SceneManagement.SceneManager.LoadScene("menutest");
            return;
        }
        uiController.ShowGameOverPanel();
    }
}

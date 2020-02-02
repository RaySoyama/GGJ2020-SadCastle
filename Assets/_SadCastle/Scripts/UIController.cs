using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIController : MonoBehaviour
{
    public Text elapsedDisplay;
    public Text houseDisplay;

    public Color healthyColor = Color.white;
    public Color criticalColor = Color.red;
    public AnimationCurve colorEase;

    public RectTransform GameOverPanel;
    public Text gameOverElapsedDisplay;
    public Text gameOverHouseDisplay;

    public string mainMenuName;
    public bool gameOver = false;

    void Update()
    {
        int minutes = (int)Time.timeSinceLevelLoad / 60;
        int seconds = (int)(Time.timeSinceLevelLoad % 60.0f);

        elapsedDisplay.text = string.Format("{0}:{1}", minutes.ToString("00"), seconds.ToString("00"));

        var healthyChunks = Kernel.instance.knownHealthyCastleChunks;
        var knownChunks = Kernel.instance.knownCastleChunks;
        houseDisplay.text = string.Format("{0}/{1}", healthyChunks.ToString("00"), knownChunks.ToString("00"));
        houseDisplay.color = Color.Lerp(criticalColor, healthyColor, colorEase.Evaluate(healthyChunks / (float)knownChunks));

        if(!gameOver && healthyChunks == 0)
        {
            ShowGameOverPanel();
        }
    }

    [ContextMenu("ShowGameOverPanel")]
    void ShowGameOverPanel()
    {
        GameOverPanel.anchoredPosition = new Vector2(0, 1080);
        GameOverPanel.DOAnchorPos(Vector2.zero, 0.5f, false).SetDelay(0.5f).SetEase(Ease.OutExpo);

        // too tired gg

        // int minutes = (int)Time.timeSinceLevelLoad / 60;
        // int seconds = (int)(Time.timeSinceLevelLoad % 60.0f);

        // gameOverElapsedDisplay.text = string.Format("{0}:{1}", minutes.ToString("00"), seconds.ToString("00"));

        // var healthyChunks = Kernel.instance.knownHealthyCastleChunks;
        // var knownChunks = Kernel.instance.knownCastleChunks;
        // gameOverHouseDisplay.text = string.Format("{0}/{1}", healthyChunks.ToString("00"), knownChunks.ToString("00"));
    }

    public void GoToMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(mainMenuName);
    }
}

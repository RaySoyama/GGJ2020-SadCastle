using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text elapsedDisplay;
    public Text houseDisplay;

    public Color healthyColor = Color.white;
    public Color criticalColor = Color.red;
    public AnimationCurve colorEase;

    void Update()
    {
        int minutes = (int)Time.timeSinceLevelLoad / 60;
        int seconds = (int)(Time.timeSinceLevelLoad % 60.0f);

        elapsedDisplay.text = string.Format("{0}:{1}", minutes.ToString("00"), seconds.ToString("00"));

        var healthyChunks = Kernel.instance.knownHealthyCastleChunks;
        var knownChunks = Kernel.instance.knownCastleChunks;
        houseDisplay.text = string.Format("{0}/{1}", healthyChunks.ToString("00"), knownChunks.ToString("00"));
        houseDisplay.color = Color.Lerp(criticalColor, healthyColor, colorEase.Evaluate(healthyChunks / (float)knownChunks));
    }
}

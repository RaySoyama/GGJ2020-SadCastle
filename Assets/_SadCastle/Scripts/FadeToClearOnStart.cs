using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FadeToClearOnStart : MonoBehaviour
{
    [SerializeField]
    Image target;
    [SerializeField]
    float duration;
    [SerializeField]
    float delay;

    void Start()
    {
        target.DOColor(Color.clear, duration).SetDelay(delay);
    }
}

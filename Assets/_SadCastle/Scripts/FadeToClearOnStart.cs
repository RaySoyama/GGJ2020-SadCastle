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

    [SerializeField]
    bool destroyOnComplete;

    void Start()
    {
        target.color = Color.white;
        target.DOColor(Color.clear, duration).SetDelay(delay).OnComplete(()=>OnFinish());
    }

    void OnFinish()
    {
        if(destroyOnComplete)
        {
            Destroy(gameObject);
        }
    }
}

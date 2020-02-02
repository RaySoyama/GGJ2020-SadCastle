using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TweenFOV : MonoBehaviour
{
    public Camera tweenTarget;

    public float startFOV;
    public float stopFOV;

    public Ease easeFOV;
    public float tweenDuration;

    public float delayFOV = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        tweenTarget.fieldOfView = startFOV;
        tweenTarget.DOFieldOfView(stopFOV, tweenDuration).SetDelay(delayFOV).SetEase(easeFOV);
    }

    void Reset()
    {
        tweenTarget = GetComponent<Camera>();
    }
}

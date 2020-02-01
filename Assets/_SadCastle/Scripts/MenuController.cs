using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class MenuController : MonoBehaviour
{
    public UnityEventBool OnMenuVisibilityChanged;
    public UnityEventGameObject OnMenuStateChange;

    public bool showOnStart = false;

    [System.Serializable]
    public class ElementStartStop
    {
        public RectTransform elementRect;
        public Vector2 startPosition;
        public Vector2 stopPosition;
        public float duration;
    }
    public ElementStartStop[] elements;

    public void Start()
    {
        if(showOnStart)
        {
            ShowMenu();
        }
    }

    [ContextMenu("Show Menu")]
    public void ShowMenu()
    {
        OnMenuVisibilityChanged?.Invoke(true);

        foreach (var elem in elements)
        {
            elem.elementRect.DOAnchorPos(elem.stopPosition, elem.duration, false);
        }
    }

    [ContextMenu("Hide Menu")]
    public void HideMenu()
    {
        OnMenuVisibilityChanged?.Invoke(false);

        foreach (var elem in elements)
        {
            elem.elementRect.DOAnchorPos(elem.startPosition, elem.duration, false).SetEase(Ease.InBounce);
        }
    }
}

[System.Serializable]
public class UnityEventGameObject : UnityEvent<GameObject> { }
[System.Serializable]
public class UnityEventBool : UnityEvent<bool> { }

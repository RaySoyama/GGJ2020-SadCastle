using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class MenuController : MonoBehaviour
{
    public UnityEventBool OnMenuVisibilityChanged;
    public UnityEventGameObject OnMenuStateChanged;

    public bool isMenuVisible;

    public float startDelaySeconds = 0.0f;
    public bool showOnStart = false;

    [System.Serializable]
    public class ElementStartStop
    {
        public RectTransform elementRect;
        public Vector2 startPosition;
        public Vector2 stopPosition;
        public float showDurationMs;
        public float hideDurationMs;

    }
    public ElementStartStop[] elements;

    private void Start()
    {
        if (showOnStart)
        {
            ShowMenu(startDelaySeconds);
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Menu"))
        {
            if (isMenuVisible)
            {
                HideMenu();
            }
            else
            {
                ShowMenu();
            }
        }
    }

    [ContextMenu("Show Menu")]
    public void ShowMenu(float delay = 0.0f)
    {
        OnMenuVisibilityChanged?.Invoke(true);

        foreach (var elem in elements)
        {
            elem.elementRect.DOKill();
            elem.elementRect.DOAnchorPos(elem.stopPosition, elem.showDurationMs / 1000.0f, false).SetDelay(delay);
        }

        isMenuVisible = true;
    }

    [ContextMenu("Hide Menu")]
    public void HideMenu()
    {
        OnMenuVisibilityChanged?.Invoke(false);

        foreach (var elem in elements)
        {
            //elem.elementRect.DORewind(false);
            elem.elementRect.DOKill();
            elem.elementRect.DOAnchorPos(elem.startPosition, elem.hideDurationMs / 1000.0f, false).SetEase(Ease.InBack);
        }

        isMenuVisible = false;
    }

    public void ExitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}

[System.Serializable]
public class UnityEventGameObject : UnityEvent<GameObject> { }
[System.Serializable]
public class UnityEventBool : UnityEvent<bool> { }
